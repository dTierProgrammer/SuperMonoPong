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
using MonoPongSuper.Script.Global;
using Microsoft.Xna.Framework.Audio;

namespace MonoPongSuper.Script.Scenes
{
    public static class Title
    {
        public static bool hasBeenPlayed;
        static Texture2D bg;
        static Texture2D title;
        public static int gamesToWin = 5;

        public static SoundEffect selection;
        public static SoundEffect select;


        static KeyboardState prevKBState;

        public static void Load()
        {
            bg = GetContent.GetTexture("image/titleBG");
            title = GetContent.GetTexture("image/titleTex");
            Fonts.fonts[0] = GetContent.GetFont("font/debug");
            Fonts.fonts[1] = GetContent.GetFont("font/title");
            Fonts.fonts[2] = GetContent.GetFont("font/titlesmall");

            selection = GetContent.GetSound("sound/selection");
            select = GetContent.GetSound("sound/select");
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
                selection.Play();
            }
            if (currentKBstate.IsKeyDown(Keys.Left) && currentKBstate != prevKBState && gamesToWin > 1)
            {
                gamesToWin--;
                selection.Play();
            }
            if (currentKBstate.IsKeyDown(Keys.M) && currentKBstate != prevKBState)
            {
                gamesToWin = 25;
                selection.Play();
            }
            if (currentKBstate.IsKeyDown(Keys.D) && currentKBstate != prevKBState)
            {
                gamesToWin = 5;
                selection.Play();
            }
            if (currentKBstate.IsKeyDown(Keys.Enter) && currentKBstate != prevKBState)
            {
                Play.scoreToWin = gamesToWin;
                Play.isPlayerCPU[0] = false;
                Play.isPlayerCPU[1] = false;
                hasBeenPlayed = true;
                select.Play();
                CurrentGameState.GoToGame();
            }
            if (currentKBstate.IsKeyDown(Keys.LeftShift) && currentKBstate != prevKBState)
            {
                Play.scoreToWin = gamesToWin;
                Play.isPlayerCPU[0] = false;
                Play.isPlayerCPU[1] = true;
                hasBeenPlayed = true;
                select.Play();
                CurrentGameState.GoToGame();
            }
            if (currentKBstate.IsKeyDown(Keys.RightShift) && currentKBstate != prevKBState)
            {
                Play.scoreToWin = gamesToWin;
                Play.isPlayerCPU[0] = true;
                Play.isPlayerCPU[1] = false;
                hasBeenPlayed = true;
                select.Play();
                CurrentGameState.GoToGame();
            }
            if ((currentKBstate.IsKeyDown(Keys.LeftAlt) && currentKBstate != prevKBState) || 
                (currentKBstate.IsKeyDown(Keys.RightAlt) && currentKBstate != prevKBState))
            {
                Play.scoreToWin = gamesToWin;
                Play.isPlayerCPU[0] = true;
                Play.isPlayerCPU[1] = true;
                hasBeenPlayed = true;
                select.Play();
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
            string menuElement1 = "Enter - VS Other Player";
            sp.DrawString(Fonts.fonts[2], menuElement1, new Vector2(Fonts.fonts[2].MeasureString(menuElement1).X / 2, 350), Color.White);
            string menuElement2 = "L/R Shift -  VS CPU";
            sp.DrawString(Fonts.fonts[2], menuElement2, new Vector2(Fonts.fonts[2].MeasureString(menuElement2).X / 2, 420), Color.White);
            string menuElement3 = $"Score To Win - <{gamesToWin}>";
            sp.DrawString(Fonts.fonts[2], menuElement3, new Vector2(Fonts.fonts[2].MeasureString(menuElement3).X / 2, 490), Color.White);
            sp.DrawString(Fonts.fonts[1], "By: DTierProgrammer\nVersion: 1.0.4.1", new Vector2(12, 650), Color.White);
        }
    }
}
