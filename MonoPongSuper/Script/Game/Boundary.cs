using Microsoft.Xna.Framework;
using MonoPongSuper.Script.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoPongSuper.Script.Game
{
    public class Boundary : Sprite
    {
        Rectangle rect;
        public Boundary(Rectangle boundaryDimsPos)
        {
            rect = boundaryDimsPos;
        }

        public override Rectangle collideBox
        {
            get { return rect; }
        }
    }
}
