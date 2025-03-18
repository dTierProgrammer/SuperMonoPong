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

namespace MonoPongSuper.Script.Game
{
    public class Paddle
    {
        public Texture2D img; // visual representation for player
        public Vector2 pos; // player position
        private Vector2 origin;

        // movement
        public bool[] isMoving = new bool[4]; // check for movement with 4 booleans, each corresponds to a cardinal direction
        //public int hasCollided = -1;
        //ublic int hasPrevCollided;

        public Vector2 velocity = Vector2.Zero; // movement speed of player
        public float maxVelocity; // max speed of player
        private const float acceleration = .4f; // rate of change for velocity
        private const float friction = acceleration * 0.5f; // opposes acceleration
        private const float tolerance = friction * 0.9f; // dictates when velocity is low enough to sit Player still

        private Rectangle[] bounds = new Rectangle[2];
        public int score;

        private ContentManager cn;

        private static SoundEffect paddleCollide;
        
        

        public Paddle(Texture2D img, Vector2 pos, float speed, Rectangle[] screenBounds, ContentManager cn) // contructor
        {
            this.img = img;
            this.pos = pos;
            maxVelocity = speed;
            origin = new Vector2(this.img.Width / 2, this.img.Height / 2);
            this.bounds[0] = screenBounds[2];
            this.bounds[1] = screenBounds[3];
            this.cn = cn;
        }

        public Rectangle collideBox // physical representation of player, creates rect bound to player's image based on image position and size
        {
            get { return new Rectangle((int)this.pos.X, (int)this.pos.Y, img.Width, img.Height); }
        }

        public static void LoadSounds(ContentManager cn) 
        {
            paddleCollide = cn.Load<SoundEffect>("sound/paddleCollide");
        }

        public void Update(GameTime gt) // update
        {
            
            if (isMoving[0]) // up
            {
                velocity.X = (velocity.X - acceleration);
                velocity.X = MathHelper.Clamp(velocity.X, -maxVelocity, maxVelocity);
            }
            if (isMoving[1]) // down
            {
                velocity.X = (velocity.X + acceleration);
                velocity.X = MathHelper.Clamp(velocity.X, -maxVelocity, maxVelocity);
            }

            else
            {
                velocity.X += -Math.Sign(velocity.X) * friction;
                if (Math.Abs(velocity.X) <= tolerance)
                {
                    velocity.X = 0;
                }
            }

            this.pos.Y += velocity.X;

            if (collideBox.Intersects(bounds[0])) 
            {
                
                velocity.X = 0;
                if (collideBox.Top <= bounds[0].Bottom)
                {
                    pos.Y = bounds[0].Bottom;
                    
                    
                }
                
            }

            if (collideBox.Intersects(bounds[1])) 
            {
                
                velocity.X = 0;
                if (collideBox.Bottom > bounds[1].Top)
                {
                    pos.Y = bounds[1].Top - img.Height;
                    
                   
                }
                

            }
            

        }

        public void Draw(SpriteBatch sp)
        {
            //sp.Draw(img, collideBox, null,  Color.White, 0, origin, SpriteEffects.None, 0);
            sp.Draw(img, collideBox, Color.White);
        }
    }
}
