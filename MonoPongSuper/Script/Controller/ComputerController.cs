using MonoPongSuper.Script.Game;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoPongSuper.Script.Controller
{
    public class ComputerController
    {
        private Paddle paddle;
        private Ball ball;

        public ComputerController(Paddle player, Ball ball) 
        {
            paddle = player;
            this.ball = ball;
        }

        public void UpdateControls() 
        {
            if (ball.pos.Y < paddle.pos.Y + (paddle.collideBox.Height / 2))
                paddle.isMoving[0] = true;
            else
                paddle.isMoving[0] = false;

            if(ball.pos.Y > paddle.pos.Y + (paddle.collideBox.Height / 2))
                paddle.isMoving[1] = true;
            else
                paddle.isMoving[1] = false;
        }
    }
}
