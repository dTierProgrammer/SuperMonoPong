using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Graphics.PackedVector;
using Microsoft.Xna.Framework.Input;
using MonoPongSuper.Script.Font;
using MonoPongSuper.Script.Sound;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonoPongSuper.Script.Scenes
{
    public static class Pause
    {
        static Texture2D gamePause;
        static Texture2D bg;
        static KeyboardState prevKBState;
        public static void Initialize(ContentManager cn)
        {
            LoadMenuSounds.LoadMenuSFX(cn);
        }

        public static void Load(ContentManager cn)
        {
            bg = cn.Load<Texture2D>("image/titleBG");
            gamePause = cn.Load<Texture2D>("image/paused");
        }

        public static void Update()
        {
            KeyboardState currentKBState = Keyboard.GetState();
            if (currentKBState.IsKeyDown(Keys.Enter) && currentKBState != prevKBState) 
            {
                LoadMenuSounds.select.Play();
                CurrentGameState.GoToGame();
            }
            if (currentKBState.IsKeyDown(Keys.Back) && currentKBState != prevKBState)
            {
                LoadMenuSounds.select.Play();
                CurrentGameState.GoToTitle();
            }
            prevKBState = currentKBState;
        }

        public static void Draw(SpriteBatch sp)
        {
            sp.Draw(bg, Vector2.Zero, Color.White);
            sp.Draw(gamePause, new Vector2(160, 40), null, Color.White, 0, new Vector2(gamePause.Width / 2, gamePause.Height / 2), 1f, SpriteEffects.None, 0);
        }
        public static void DrawText(SpriteBatch sp)
        {
            sp.DrawString(Fonts.fonts[2], "ENTER - RESUME", new Vector2(332, 350), Color.White);
            sp.DrawString(Fonts.fonts[2], "BACKSPACE - TITLE", new Vector2(275, 420), Color.White);
            sp.DrawString(Fonts.fonts[2], $"SCORE: {Play.players[0].score} - {Play.players[1].score}", new Vector2(405, 490), Color.White);
        }
    }
}
