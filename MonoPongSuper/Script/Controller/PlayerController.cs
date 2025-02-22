using Microsoft.Xna.Framework.Input;
using MonoPongSuper.Script.Game;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoPongSuper.Script.Controller
{
    public class PlayerController
    {
        int playerIndex;
        Paddle paddle;

        public PlayerController(int playerIndex, Paddle paddle) 
        {
            this.playerIndex = playerIndex;
            this.paddle = paddle;
        }

        public void UpdateControls() 
        {
            switch (playerIndex) 
            {
                case 1:
                    if (Keyboard.GetState().IsKeyDown(Keys.W))
                    {
                        paddle.isMoving[0] = true;
                    }
                    else 
                    {
                        paddle.isMoving[0] = false;
                    }
                    if (Keyboard.GetState().IsKeyDown(Keys.S))
                    {
                        paddle.isMoving[1] = true;
                    }
                    else 
                    {
                        paddle.isMoving[1] = false;
                    }
                        break;
                case 2:
                    if (Keyboard.GetState().IsKeyDown(Keys.Up))
                    {
                        paddle.isMoving[0] = true;
                    }
                    else
                    {
                        paddle.isMoving[0] = false;
                    }
                    if (Keyboard.GetState().IsKeyDown(Keys.Down))
                    {
                        paddle.isMoving[1] = true;
                    }
                    else
                    {
                        paddle.isMoving[1] = false;
                    }
                    break;
            }
        }
    }
}
