using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MonoPongSuper.Script.Sound;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using MonoPongSuper.Script.Base;
using MonoPongSuper.Script.Global;

namespace MonoPongSuper.Script.Game
{
    public class Ball : Sprite
    {
        public Vector2 respawnPos; // ball position after respawn (goal hit)
        public Vector2 velocity; // ball speed, x and y

        List<Sprite> collisionGroup;
        private ContentManager cn;

        private static SoundEffect ballBounce = GetContent.GetSound("sound/bounce");
        private static SoundEffect ballLoss = GetContent.GetSound("sound/ballLoss");
        private static SoundEffect score = GetContent.GetSound("sound/score");
        private static SoundEffect paddleBounce = GetContent.GetSound("sound/paddleBounce");

        private Vector2 prevPosition;
        public Vector2 projPos;

        float spdR;
        float Reset;
        public Ball(Texture2D img, Vector2 pos, float speed, List<Sprite> collisionGroup) : base(img, pos) // 100 overload challenge !!!
        {
            respawnPos = pos;
            velocity = new Vector2(speed, speed);
            spdR = speed;
            Reset = speed;
            this.collisionGroup = collisionGroup;
        }

        protected void ResetPos(GameTime gt)
        {
            velocity = new Vector2(spdR * (float)gt.ElapsedGameTime.TotalSeconds, 0);
            velocity *= new Vector2(-1, -1);
            this.pos = respawnPos;
        }

        protected void BounceLeft()
        {
            velocity.X = -Math.Abs(velocity.X);
        }

        protected void BounceRight()
        {
            velocity.X = Math.Abs(velocity.X);
        }

        protected void BounceUp()
        {
            velocity.Y = -Math.Abs(velocity.Y);
        }

        public void BounceDown()
        {
            velocity.Y = Math.Abs(velocity.Y);
        }

        public void ResetVelocity(GameTime gt)
        {
            spdR = Reset;
            velocity.X = spdR * (float)gt.ElapsedGameTime.TotalSeconds;
            velocity.Y = 0;
        }

        private static void GoalHelper(Goal goal)
        {
            goal.Score();
        }

        protected float ProjectPositionX(GameTime gt) 
        {
            float projectedX = pos.X + velocity.X * (float)gt.ElapsedGameTime.TotalSeconds;
            return projectedX;
        }

        protected void CheckCollisionsX(GameTime gt) 
        {
            foreach (Sprite gameObject in collisionGroup) 
            {
                
                if (gameObject != this && collideBox.Intersects(gameObject.collideBox)) 
                {
                    if (gameObject is Paddle) 
                    {
                        if (prevPosition.X > gameObject.collideBox.X)
                        {
                            pos.X = gameObject.collideBox.Right;
                            BounceRight();
                            paddleBounce.Play();
                        }
                        else
                        {
                            pos.X = gameObject.collideBox.Left - collideBox.Width;
                            BounceLeft();
                            paddleBounce.Play();
                        }
                        velocity.Y = (collideBox.Center.Y - gameObject.collideBox.Center.Y) / 6f;
                        Math.Clamp(velocity.Y, -spdR, spdR);
                        break;
                    }

                    if (gameObject is Goal) 
                    {
                        GoalHelper((Goal)gameObject);
                        ResetPos();
                        ballLoss.Play();
                        break;
                    }
                }
            }
            prevPosition.X = pos.X;
        }

        protected float ProjectPositionY(GameTime gt)
        {
            float projectedY = pos.Y + velocity.Y * (float)gt.ElapsedGameTime.TotalSeconds;
            return projectedY;
        }

        protected void CheckCollisionsY(GameTime gt) 
        {

            foreach (Sprite gameObject in collisionGroup)
            {

                if (collideBox.Intersects(gameObject.collideBox))
                {
                    Vector2 intersection = GetIntersections.LineRectReturnIntersection((float)collideBox.Center.X, (float)collideBox.Center.Y, projPos.X, projPos.Y, gameObject.collideBox);
                    if (intersection != new Vector2(-1000, -1000))
                    {
                        intersections.Add(intersection);
                    }
                    else
                    {
                        pos.Y = gameObject.collideBox.Top - collideBox.Height;
                        BounceUp();
                        ballBounce.Play();
                    }
                    break;
                }
                
            }
            prevPosition.Y = pos.Y;
        }

        protected Vector2 ProjectPosition(GameTime gt) 
        {
            Vector2 projectedPosition = new Vector2(ProjectPositionX(gt), ProjectPositionY(gt));
            return projectedPosition;
        }

        protected bool CheckCollisionsLineSegment(GameTime gt) 
        {
            foreach (Sprite gameObject in collisionGroup) 
            {
                // left
                if (gameObject != this && GetIntersections.DoesLineIntersectLine(
                    pos, 
                    ProjectPosition(gt), 
                    new Vector2(gameObject.collideBox.X, gameObject.collideBox.Y), 
                    new Vector2(gameObject.collideBox.X, gameObject.collideBox.Y + gameObject.collideBox.Height))) 
                {
                    
                    pos.X = gameObject.collideBox.Left - collideBox.Width;

                    if (gameObject is Goal) 
                    {
                        GoalHelper((Goal)gameObject);
                        ResetPos();
                        ballLoss.Play();
                    }
                    else if (gameObject is Paddle) 
                    {
                        BounceLeft();
                        velocity.Y = (collideBox.Center.Y - gameObject.collideBox.Center.Y) / 6f;
                        Math.Clamp(velocity.Y, -spdR, spdR);
                    }
                    else if (gameObject is Boundary) 
                    {
                        BounceLeft();
                    }

                    return true;
                }

                // right
                if (gameObject != this && GetIntersections.DoesLineIntersectLine(
                    pos,
                    ProjectPosition(gt),
                    new Vector2(gameObject.collideBox.X + gameObject.collideBox.Width, gameObject.collideBox.Y),
                    new Vector2(gameObject.collideBox.X + gameObject.collideBox.Width, gameObject.collideBox.Y + gameObject.collideBox.Height))) 
                {
                    pos.X = gameObject.collideBox.Left + 1;

                    if (gameObject is Goal)
                    {
                        GoalHelper((Goal)gameObject);
                        ResetPos();
                        ballLoss.Play();
                    }
                    else if (gameObject is Paddle)
                    {
                        BounceRight();
                        velocity.Y = (collideBox.Center.Y - gameObject.collideBox.Center.Y) / 6f;
                        Math.Clamp(velocity.Y, -spdR, spdR);
                    }
                    else if (gameObject is Boundary)
                    {
                        BounceRight();
                    }

                    return true;
                }
                
                // up
                if (gameObject != this && GetIntersections.DoesLineIntersectLine(
                    pos,
                    ProjectPosition(gt),
                    new Vector2(gameObject.collideBox.X, gameObject.collideBox.Y),
                    new Vector2(gameObject.collideBox.X + gameObject.collideBox.Width, gameObject.collideBox.Y + gameObject.collideBox.Height))) 
                {
                    pos.Y = gameObject.collideBox.Top - collideBox.Height;
                    BounceUp();
                    return true;
                }

                // down
                if (gameObject != this && GetIntersections.DoesLineIntersectLine(
                    pos,
                    ProjectPosition(gt),
                    new Vector2(gameObject.collideBox.X, gameObject.collideBox.Y + gameObject.collideBox.Height),
                    new Vector2(gameObject.collideBox.X + gameObject.collideBox.Width, gameObject.collideBox.Y + gameObject.collideBox.Height))) 
                {
                    pos.Y = gameObject.collideBox.Bottom;
                    BounceDown();
                    return true;
                }
            }
            return false;
        }

        public override void Update(GameTime gt)
        {
            CheckCollisionsLineSegment(gt);
            this.pos += velocity;
            ProjectPosition(gt);
            
        }
    }
}
