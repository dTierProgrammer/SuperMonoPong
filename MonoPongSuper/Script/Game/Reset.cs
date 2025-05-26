using Microsoft.Xna.Framework;
using MonoPongSuper.Script.Scenes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoPongSuper.Script.Game
{
    public static class Reset
    {
        public static void ResetGame(GameTime gt) 
        {
            Play.players[0].score = 0; 
            Play.players[1].score = 0;
            Play.players[0].velocity.X = 0; Play.players[1].velocity.X = 0;
            Play.players[0].pos = new Vector2(10, 90); Play.players[1].pos = new Vector2(306, 90);
            Play.ball.pos = new Vector2(160, 90);
            Play.ball.ResetVelocity(gt);
        }
    }
}
