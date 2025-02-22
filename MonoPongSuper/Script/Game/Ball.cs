using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
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
        public Ball(Texture2D img, Vector2 pos, float speed, Paddle[] paddles, Rectangle[] screenBounds) // 100 overload challenge !!!
        {
            this.img = img;
            this.pos = pos;
            respawnPos = pos;
            velocity.X = speed;
            velocity.Y = speed;

            this.screenBounds = screenBounds;
            this.paddles = paddles;
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

        public void ResetPos() 
        {
            velocity *= new Vector2(-1, -1);
            this.pos = respawnPos;
        }

        public void Update() 
        {
            this.pos += velocity;
            if (collideBox.Intersects(screenBounds[2]) || collideBox.Intersects(screenBounds[3])) 
            {
                velocity.Y *= -1;
            }
            if (collideBox.Intersects(screenBounds[0]))
            {
                paddles[0].score++;
                
            }
            else if (collideBox.Intersects(screenBounds[1]))
            {
                paddles[1].score++;
                
            }
            if (collideBox.Intersects(screenBounds[0]) || collideBox.Intersects(screenBounds[1]))
            {
                ResetPos();
                
            }
            foreach (Paddle paddle in paddles) 
            {
                if (collideBox.Intersects(paddle.collideBox))
                {
                    if (collideBox.Top <= paddle.collideBox.Bottom || collideBox.Bottom >= paddle.collideBox.Top) 
                    {
                        velocity.Y = paddle.velocity.X;
                        velocity.Y *= -1;
                    }
                    if (collideBox.Left <= paddle.collideBox.Right || collideBox.Right >= paddle.collideBox.Left) // what happens if the ball gets behind the player?
                    {
                        velocity.X *= -1;
                        velocity.Y = paddle.velocity.X;
                    }
                }
            }
        }

        public void Draw(SpriteBatch sp) 
        {
            sp.Draw(img, collideBox, Color.White);
        }
    }
}
