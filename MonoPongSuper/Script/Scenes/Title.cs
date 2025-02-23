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
using Microsoft.Xna.Framework.Input;
using MonoPongSuper.Script.Game;
using MonoPongSuper.Script.Sound;
using System.ComponentModel.Design;

namespace MonoPongSuper.Script.Scenes
{
    public static class Title
    {
        public static bool hasBeenPlayed;
        static Texture2D bg;
        static Texture2D title;
        public static int gamesToWin = 5;


        static KeyboardState prevKBState;

        public static void Initialize(ContentManager cn)
        {

            LoadMenuSounds.LoadMenuSFX(cn);
            
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
            Play.players[0].score = 0;
            Play.players[1].score = 0;
            Reset.ResetGame();
            KeyboardState currentKBstate = Keyboard.GetState();
            if (currentKBstate.IsKeyDown(Keys.Right) && currentKBstate != prevKBState  && gamesToWin < 25) 
            {
                gamesToWin++;
                LoadMenuSounds.selection.Play();
            }
            if (currentKBstate.IsKeyDown(Keys.Left) && currentKBstate != prevKBState && gamesToWin > 1)
            {
                gamesToWin--;
                LoadMenuSounds.selection.Play();
            }
            if (currentKBstate.IsKeyDown(Keys.M) && currentKBstate != prevKBState)
            {
                gamesToWin = 25;
                LoadMenuSounds.selection.Play();
            }
            if (currentKBstate.IsKeyDown(Keys.D) && currentKBstate != prevKBState)
            {
                gamesToWin = 5;
                LoadMenuSounds.selection.Play();
            }
            if (currentKBstate.IsKeyDown(Keys.Enter) && currentKBstate != prevKBState)
            {
                Play.scoreToWin = gamesToWin;
                Play.isPlayerCPU[0] = false;
                Play.isPlayerCPU[1] = false;
                hasBeenPlayed = true;
                LoadMenuSounds.select.Play();
                CurrentGameState.GoToGame();
            }
            if (currentKBstate.IsKeyDown(Keys.LeftShift) && currentKBstate != prevKBState)
            {
                Play.scoreToWin = gamesToWin;
                Play.isPlayerCPU[0] = false;
                Play.isPlayerCPU[1] = true;
                hasBeenPlayed = true;
                LoadMenuSounds.select.Play();
                CurrentGameState.GoToGame();
            }
            if (currentKBstate.IsKeyDown(Keys.RightShift) && currentKBstate != prevKBState)
            {
                Play.scoreToWin = gamesToWin;
                Play.isPlayerCPU[0] = true;
                Play.isPlayerCPU[1] = false;
                hasBeenPlayed = true;
                LoadMenuSounds.select.Play();
                CurrentGameState.GoToGame();
            }
            if ((currentKBstate.IsKeyDown(Keys.LeftAlt) && currentKBstate != prevKBState) || 
                (currentKBstate.IsKeyDown(Keys.RightAlt) && currentKBstate != prevKBState))
            {
                Play.scoreToWin = gamesToWin;
                Play.isPlayerCPU[0] = true;
                Play.isPlayerCPU[1] = true;
                hasBeenPlayed = true;
                LoadMenuSounds.select.Play();
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
            sp.DrawString(Fonts.fonts[1], "By: D TIER PROGRAMMER\nVersion: 1.0", new Vector2(12, 650), Color.White);
        }
    }
}
