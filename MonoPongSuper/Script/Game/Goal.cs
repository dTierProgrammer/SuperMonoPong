using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoPongSuper.Script.Game
{
    public class Goal : Boundary
    {
        private Paddle paddle;
        public Goal(Rectangle boundaryDimsPos, Paddle paddle): base (boundaryDimsPos) { this.paddle = paddle; }
        public void Score() { paddle.score++; }
    }
}
