using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoPongSuper.Script.Font;
using MonoPongSuper.Script.Game;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoPongSuper.Script.Scenes
{
    public static class GameOver
    {
        static Texture2D bg;
        static Texture2D p1Win;
        static Texture2D p2Win;
        static Texture2D userWin;
        static Texture2D userLoss;
        static Texture2D cpuWin;
        static SoundEffect gameEnd;

        static KeyboardState prevKBstate;
        public static void Initialize(ContentManager cn)
        {

        }

        public static void Load(ContentManager cn)
        {
            bg = cn.Load<Texture2D>("image/titleBG");
            p1Win = cn.Load<Texture2D>("image/user_p1Win");
            p2Win = cn.Load<Texture2D>("image/user_p2Win");
            userWin = cn.Load<Texture2D>("image/user_Win");
            userLoss = cn.Load<Texture2D>("image/user_loss");
            cpuWin = cn.Load<Texture2D>("image/user_game_over");
            gameEnd = cn.Load<SoundEffect>("sound/gameEnd");
        }

        public static void Update()
        {
            KeyboardState currentKBstate = Keyboard.GetState();
            if (currentKBstate.IsKeyDown(Keys.Enter) && currentKBstate != prevKBstate) 
            {
                Play.players[0].score = 0;
                Play.players[1].score = 0;
                Reset.ResetGame();
                
                CurrentGameState.GoToGame();
            }
            if (currentKBstate.IsKeyDown(Keys.Back) && currentKBstate != prevKBstate)
            {
                Play.players[0].score = 0;
                Play.players[1].score = 0;
                Reset.ResetGame();
                
                CurrentGameState.GoToTitle();
            }
            prevKBstate = currentKBstate;
        }

        public static void Draw(SpriteBatch sp)
        {
            sp.Draw(bg, Vector2.Zero, Color.White);
            if ((Play.isPlayerCPU[1] && Play.players[0].score == Play.scoreToWin) && !Play.isPlayerCPU[1] ||
                (Play.isPlayerCPU[0] && Play.players[1].score == Play.scoreToWin) && !Play.isPlayerCPU[0])
                sp.Draw(userWin, new Vector2(160, 40), null, Color.White, 0, new Vector2(userWin.Width / 2, userWin.Height / 2), 1f, SpriteEffects.None, 0);
            else if (Play.players[0].score == Play.scoreToWin && !Play.isPlayerCPU[0])
                sp.Draw(p1Win, new Vector2(160, 40), null, Color.White, 0, new Vector2(p1Win.Width / 2, p1Win.Height / 2), 1f, SpriteEffects.None, 0);
            else if (Play.players[1].score == Play.scoreToWin && !Play.isPlayerCPU[1])
                sp.Draw(p2Win, new Vector2(160, 40), null, Color.White, 0, new Vector2(p2Win.Width / 2, p2Win.Height / 2), 1f, SpriteEffects.None, 0);
            else if (Play.isPlayerCPU[0] && Play.players[0].score == Play.scoreToWin || Play.isPlayerCPU[1] && Play.players[1].score == Play.scoreToWin) 
                sp.Draw(userLoss, new Vector2(160, 40), null, Color.White, 0, new Vector2(userLoss.Width / 2, userLoss.Height / 2), 1f, SpriteEffects.None, 0);
            else
                sp.Draw(cpuWin, new Vector2(160, 40), null, Color.White, 0, new Vector2(userLoss.Width / 2 + 15, userLoss.Height / 2), 1f, SpriteEffects.None, 0);
        }

        public static void DrawText(SpriteBatch sp) 
        {
            sp.DrawString(Fonts.fonts[2], "ENTER - REMATCH", new Vector2(320, 350), Color.White);
            sp.DrawString(Fonts.fonts[2], "BACKSPACE - TITLE", new Vector2(275, 420), Color.White);
            sp.DrawString(Fonts.fonts[2], $"SCORE: {Play.players[0].score} - {Play.players[1].score}", new Vector2(405, 490), Color.White);
        }
    }
}
