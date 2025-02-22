using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Graphics.PackedVector;
using MonoPongSuper.Script.Font;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Vector2 = Microsoft.Xna.Framework.Vector2;
using MonoPongSuper.Script.Font;
using Microsoft.Xna.Framework.Input;

namespace MonoPongSuper.Script.Scenes
{
    public static class Title
    {
        static Texture2D bg;
        static Texture2D title;
        public static int gamesToWin = 5;

        static KeyboardState prevKBState;

        public static void Initialize(ContentManager cn)
        {
            
        }

        public static void Load(ContentManager cn)
        {
            bg = cn.Load<Texture2D>("image/titleBG");
            title = cn.Load<Texture2D>("image/titleTex");
            Fonts.fonts[0] = cn.Load<SpriteFont>("font/debug");
            Fonts.fonts[1] = cn.Load<SpriteFont>("font/title");
            Fonts.fonts[2] = cn.Load<SpriteFont>("font/titlesmall");
        }

        public static void Update()
        {
            KeyboardState currentKBstate = Keyboard.GetState();
            if (currentKBstate.IsKeyDown(Keys.Right) && currentKBstate != prevKBState  && gamesToWin < 25) 
            {
                gamesToWin++;
            }
            if (currentKBstate.IsKeyDown(Keys.Left) && currentKBstate != prevKBState && gamesToWin > 1)
            {
                gamesToWin--;
            }
            if (currentKBstate.IsKeyDown(Keys.M))
            {
                gamesToWin = 25;
            }
            if (currentKBstate.IsKeyDown(Keys.D))
            {
                gamesToWin = 5;
            }
            if (currentKBstate.IsKeyDown(Keys.Enter))
            {
                Play.scoreToWin = gamesToWin;
                CurrentGameState.GoToGame();
            }
            prevKBState = currentKBstate;
        }

        public static void Draw(SpriteBatch sp)
        {
            sp.Draw(bg, Vector2.Zero, Color.White);
            sp.Draw(title, new Vector2(160, 40), null, Color.White, 0, new Vector2(title.Width / 2, title.Height / 2), 1f, SpriteEffects.None, 0);
        }

        public static void DrawText(SpriteBatch sp)
        {
            
            sp.DrawString(Fonts.fonts[2], "ENTER - VS HUMAN", new Vector2(340, 350), Color.White);
            sp.DrawString(Fonts.fonts[2], "L/R SHIFT - VS CPU", new Vector2(310, 420), Color.White);
            sp.DrawString(Fonts.fonts[2], $"SCORE TO WIN - <{gamesToWin}>", new Vector2(305, 490), Color.White);

            sp.DrawString(Fonts.fonts[1], "By: D TIER PROGRAMMER\nVersion: 1.0 [Unfinished]", new Vector2(12, 650), Color.White);
        }
    }
}
