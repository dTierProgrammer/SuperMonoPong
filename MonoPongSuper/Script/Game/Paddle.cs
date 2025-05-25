using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Vector2 = Microsoft.Xna.Framework.Vector2;
using MonoPongSuper.Script.Base;

namespace MonoPongSuper.Script.Game
{
    public class Paddle : Sprite
    {

        // movement
        public bool[] isMoving = new bool[4]; // check for movement with 4 booleans, each corresponds to a cardinal direction

        public Vector2 velocity = Vector2.Zero; // movement speed of player
        public float maxVelocity; // max speed of player
        private const float acceleration = .4f; // rate of change for velocity
        private const float friction = acceleration * 0.5f; // opposes acceleration
        private const float tolerance = friction * 0.9f; // dictates when velocity is low enough to sit Player still
        private Vector2 previousPosition;

        public int score;

        List<Sprite> collisionGroup;

        public Paddle(Texture2D img, Vector2 pos, float speed, List<Sprite> collisionGroup): base (img, pos) // contructor
        {
            maxVelocity = speed;
            this.collisionGroup = collisionGroup;
            previousPosition = pos;
        }

        public void MovePaddleY() 
        {
            if (isMoving[0]) // up
            {
                velocity.Y = (velocity.Y - acceleration);
                velocity.Y = MathHelper.Clamp(velocity.Y, -maxVelocity, maxVelocity);
            }
            else if (isMoving[1]) // down
            {
                velocity.Y = (velocity.Y + acceleration);
                velocity.Y = MathHelper.Clamp(velocity.Y, -maxVelocity, maxVelocity);
            }
            else
            {
                velocity.Y += -Math.Sign(velocity.Y) * friction;
                if (Math.Abs(velocity.Y) <= tolerance)
                {
                    velocity.Y = 0;
                }
            }
            this.pos.Y += velocity.Y;
        }

        public void CheckCollisionsY() 
        {
            foreach (Sprite gameObject in collisionGroup) 
            {
                if (gameObject is Boundary) 
                {
                    if (collideBox.Intersects(gameObject.collideBox)) 
                    {
                        velocity.X = 0;
                        if (previousPosition.Y > gameObject.collideBox.Y)
                            pos.Y = gameObject.collideBox.Bottom;
                        else
                            pos.Y = gameObject.collideBox.Top - collideBox.Height;
                        break;
                    }
                }
            }
        }

        public override void Update(GameTime gt) // update
        {
            MovePaddleY();
            CheckCollisionsY();
            previousPosition.Y = pos.Y;
        }
    }
}
