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

        float spdR;
        public Ball(Texture2D img, Vector2 pos, float speed, List<Sprite> collisionGroup): base(img, pos) // 100 overload challenge !!!
        {
            respawnPos = pos;
            velocity = new Vector2(speed, speed);
            spdR = speed;

            this.cn = cn;

            this.collisionGroup = collisionGroup;
        }

        protected void ResetPos() 
        {
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

        public void ResetVelocity() 
        {
            velocity.X = spdR;
            velocity.Y = spdR;
        }

        private static void GoalHelper(Goal goal) 
        {
            goal.Score();
        }

        protected void CheckCollisionsX() 
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
        }

        protected void CheckCollisionsY() 
        {
            foreach (Sprite gameObject in collisionGroup)
            {
                if (collideBox.Intersects(gameObject.collideBox))
                {
                    if (gameObject == this)
                        continue;
                    if (prevPosition.Y > gameObject.collideBox.Y)
                    {
                        pos.Y = gameObject.collideBox.Bottom;
                        BounceDown();
                        ballBounce.Play();
                    }
                    else
                    {
                        pos.Y = gameObject.collideBox.Top - collideBox.Height;
                        BounceUp();
                        ballBounce.Play();
                    }
                    if (gameObject is Paddle) 
                    {
                        velocity.X = (collideBox.Center.X - gameObject.collideBox.Center.X) / 6f;
                        Math.Clamp(velocity.X, -spdR, spdR);
                    }
                    break;
                }
            }
            
        }

        public override void Update(GameTime gt)
        {
            this.pos.X += velocity.X;
            CheckCollisionsX();
            prevPosition.X = pos.X;

            this.pos.Y += velocity.Y;
            CheckCollisionsY();
            prevPosition.Y = pos.Y;
        }
    }
}
