using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoPongSuper.Script.Base
{
    public class Sprite
    {
        protected Texture2D img;
        public Vector2 pos;
        protected Color color;

        public Sprite(Texture2D img, Vector2 pos, Color color)
        {
            this.img = img;
            this.pos = pos;
            this.color = color;
        }
        public Sprite(Texture2D img, Vector2 pos)
        {
            this.img = img;
            this.pos = pos;
            this.color = Color.White;
        }

        public Sprite(Vector2 pos, Color color)
        {
            this.pos = pos;
            this.color = color;
        }

        public Sprite(Vector2 pos)
        {
            this.pos = pos;
            this.color = Color.White;
        }

        public Sprite() 
        {

        }

        public virtual Rectangle collideBox
        {
            get { return new Rectangle((int)this.pos.X, (int)this.pos.Y, this.img.Width, this.img.Height); }
        }

        public virtual void Update(GameTime gt)
        {
            // override method
        }

        public virtual void Draw(SpriteBatch sb) 
        {
            sb.Draw(this.img, this.collideBox, color);
        }
    }
}
