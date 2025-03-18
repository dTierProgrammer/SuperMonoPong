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

namespace MonoPongSuper.Script.Game
{
    public class Ball
    {
        public Texture2D img; // visual representation of ball
        public Vector2 pos; // initial ball position
        public Vector2 respawnPos; // ball position after respawn (goal hit)
        public Vector2 velocity; // ball speed, x and y

        public Paddle[] paddles = new Paddle[2]; // references of game objects
        public Rectangle[] screenBounds = new Rectangle[4]; // Screen boundaries. 0 = Left, 1 = Right, 2 = Upper, 3 = Lower
        private ContentManager cn;

        private static SoundEffect ballBounce;
        private static SoundEffect ballLoss;
        private static SoundEffect score;
        private static SoundEffect paddleBounce;
        private Paddle _paddle;

        private Vector2 prevPosition;
        private bool hasCollided;

        Random rng = new Random();
        float spdR;
        public Ball(Texture2D img, Vector2 pos, float speed, Paddle[] paddles, Rectangle[] screenBounds, ContentManager cn) // 100 overload challenge !!!
        {
            this.img = img;
            this.pos = pos;
            respawnPos = pos;
            velocity.X = speed;
            velocity.Y = speed;
            spdR = speed;
            

            this.screenBounds = screenBounds;
            this.paddles = paddles;

            this.cn = cn;
        }

        public Rectangle collideBox // physical representation of ball
        {
            get 
            {
                return new Rectangle
                {
                    Width = img.Width,
                    Height = img.Height,
                    X = (int)this.pos.X,
                    Y = (int)this.pos.Y
                };
            }
        }

        public static void LoadSounds(ContentManager cn) 
        {
            ballBounce = cn.Load<SoundEffect>("sound/bounce");
            ballLoss = cn.Load<SoundEffect>("sound/ballLoss");
            score = cn.Load<SoundEffect>("sound/score");
            paddleBounce = cn.Load<SoundEffect>("sound/paddleBounce");
        }

        public void ResetPos() 
        {
            velocity *= new Vector2(-1, -1);
            this.pos = respawnPos;
        }

        public void ReverseVelocityX()
        {
            velocity.X *= -1;
        }

        public void ReverseVelocityY()
        {
            velocity.Y *= -1;
        }

        public void BounceLeft()
        {
            velocity.X = -Math.Abs(velocity.X);
        }

        public void BounceRight()
        {
            velocity.X = Math.Abs(velocity.X);
        }

        public void BounceUp()
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

        public void Update() 
        {
            // X Start
            this.pos.X += velocity.X;
            if (collideBox.Intersects(screenBounds[0])) 
            {
                paddles[1].score++;
                ResetPos();
                ballLoss.Play();
            }
            if (collideBox.Intersects(screenBounds[1]))
            {
                paddles[0].score++;
                ResetPos();
                ballLoss.Play();
            }
            foreach (Paddle paddle in paddles) 
            {
                if (collideBox.Intersects(paddle.collideBox)) 
                {
                    _paddle = paddle;
                    hasCollided = true;
                    break;
                }
            }
            if(hasCollided && collideBox.Intersects(_paddle.collideBox)) 
            {
                if((collideBox.Right >= _paddle.collideBox.Left) && (prevPosition.X >= _paddle.collideBox.Right))
                { // r
                    paddleBounce.Play();
                    BounceRight();
                    pos.X = prevPosition.X;
                    velocity.Y = (collideBox.Center.Y - _paddle.collideBox.Center.Y) / 6f;
                    Math.Clamp(velocity.Y, -spdR, spdR);
                    hasCollided = false;
                }
                if((collideBox.Left <= _paddle.collideBox.Right) && (prevPosition.X <= _paddle.collideBox.Left)) 
                { // l
                    paddleBounce.Play();
                    BounceLeft();
                    pos.X = prevPosition.X;
                    velocity.Y = (collideBox.Center.Y - _paddle.collideBox.Center.Y) / 6f;
                    Math.Clamp(velocity.Y, -spdR, spdR);
                    hasCollided = false;
                }
            }

            // X End

            // Y Start
            this.pos.Y += velocity.Y;
            if (collideBox.Intersects(screenBounds[2])) //up
            {
                if(prevPosition.Y > screenBounds[2].Bottom) 
                {
                    pos.Y = screenBounds[2].Bottom;
                }
                BounceDown();
                ballBounce.Play(); 
            }

            if (collideBox.Intersects(screenBounds[3])) //down
            {
                if(prevPosition.Y < screenBounds[3].Top - collideBox.Height) 
                {
                    pos.Y = screenBounds[3].Top - collideBox.Height;
                }
                BounceUp();
                ballBounce.Play();
            }

            foreach (Paddle paddle in paddles)
            {
                if (collideBox.Intersects(paddle.collideBox))
                {
                    _paddle = paddle;
                    hasCollided = true;
                    break;
                }
            }

            if (hasCollided && collideBox.Intersects(_paddle.collideBox))
            {
                if ((collideBox.Top <= _paddle.collideBox.Bottom) && (prevPosition.Y <= _paddle.collideBox.Bottom))
                {
                    paddleBounce.Play();
                    BounceUp();
                    pos.Y = _paddle.collideBox.Top - collideBox.Height;
                }

                if((collideBox.Bottom >= _paddle.collideBox.Top) && (prevPosition.Y >= _paddle.collideBox.Top)) 
                {
                    paddleBounce.Play();
                    BounceDown();
                    pos.Y = _paddle.collideBox.Bottom;
                }
            }
            // Y End
            prevPosition = pos;
        }

        public void Draw(SpriteBatch sp) 
        {
            sp.Draw(img, collideBox, Color.White);
        }
    }
}
