using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Diagnostics;
using System;
using MonoPongSuper.Script.Controller;
using MonoPongSuper.Script.Game;
using MonoPongSuper.Script.Font;
using System.Net.Http;
using Microsoft.Xna.Framework.Content;
using System.Reflection.Metadata;
using Microsoft.Xna.Framework.Audio;
using System.Collections.Generic;
using MonoPongSuper.Script.Base;
using MonoPongSuper.Script.Global;

namespace MonoPongSuper.Script.Scenes
{
    public static class Play
    {
        private static Texture2D rectDebug;
        private static bool showDebug = true;
        private static bool showScore = true;

        private static Texture2D bg;

        private static Rectangle[] screenBounds = new Rectangle[4]; // Screen boundaries. 0 = Left, 1 = Right, 2 = Upper, 3 = Lower

        public readonly static Paddle[] players = new Paddle[2];
        private static PlayerController[] playersInp = new PlayerController[2];
        private static ComputerController[] computersInp = new ComputerController[2];
        public static bool[] isPlayerCPU = new bool[2];
        private static int[] scores = new int[2];

        public static Ball ball;

        public static int scoreToWin = 1;
        private static KeyboardState prevKBState;

        private static SoundEffect gameEnd;

        private static List<Sprite> gameObjects = new();

        public static void Load() 
        {
            bg = GetContent.GetTexture("image/gamefield");
            rectDebug = GetContent.GetTexture("image/ballSuper");

            Fonts.fonts[0] = GetContent.GetFont("font/debug");
            Fonts.fonts[1] = GetContent.GetFont("font/title");

            Texture2D testImage = GetContent.GetTexture("image/test");
            Texture2D paddleImg = GetContent.GetTexture("image/paddleSuper");
            Texture2D ballImg = GetContent.GetTexture("image/ballSuper");

            gameEnd = GetContent.GetSound("sound/gameEnd");

            float maxSpeed = 3f;

            ball = new Ball(ballImg, new Vector2(160, 90), 100f, gameObjects);
            players[0] = new Paddle(paddleImg, new Vector2(10, 90), maxSpeed, gameObjects);

            computersInp[0] = new ComputerController(players[0], ball);
            playersInp[0] = new PlayerController(1, players[0]);

            
            players[1] = new Paddle(paddleImg, new Vector2(306, 90), maxSpeed, gameObjects);
            computersInp[1] = new ComputerController(players[1], ball);
            playersInp[1] = new PlayerController(2, players[1]);

            gameObjects.Add(ball);
            gameObjects.Add(players[0]);
            gameObjects.Add(players[1]);
            gameObjects.Add(new Goal(new Rectangle(0, 0, 6, 180), players[1]));
            gameObjects.Add(new Goal(new Rectangle(314, 0, 6, 180), players[0]));
            gameObjects.Add(new Boundary(new Rectangle(0, 0, 320, 6)));
            gameObjects.Add(new Boundary(new Rectangle(0, 174, 320, 6)));
        }

        public static void Update(GameTime gt) 
        {
            KeyboardState currentKBState = Keyboard.GetState();

            if (currentKBState.IsKeyDown(Keys.P) && currentKBState != prevKBState)
            {
                CurrentGameState.PauseGame();
            }
            prevKBState = currentKBState;

            foreach (Sprite gameObject in gameObjects)
            {
                if (gameObject is not Boundary)
                    gameObject.Update(gt);

                if (gameObject is Paddle) 
                {
                    if (isPlayerCPU[0])
                        computersInp[0].UpdateControls();
                    else
                        playersInp[0].UpdateControls();

                    if (isPlayerCPU[1])
                        computersInp[1].UpdateControls();
                    else
                        playersInp[1].UpdateControls();
                }
            }

            if (players[0].score == scoreToWin || players[1].score == scoreToWin) 
            {
                gameEnd.Play();
                CurrentGameState.GameOver();
            }
        }
        

        public static void Draw(SpriteBatch _spriteBatch) 
        {
            _spriteBatch.Draw(bg, Vector2.Zero, Color.White);

            foreach (Sprite gameObject in gameObjects) 
            {
                if (gameObject is not Boundary)
                    gameObject.Draw(_spriteBatch);
            }

            if (showScore)
            {
                _spriteBatch.DrawString(Fonts.fonts[1], $"{players[0].score}", new Vector2(100, 9), Color.White);
                _spriteBatch.DrawString(Fonts.fonts[1], $"{players[1].score}", new Vector2(205, 9), Color.White);
            }
        }

        public static void DrawText(SpriteBatch sp, GameTime gt)
        {

            sp.DrawString(Fonts.fonts[0], $"Best Of: {scoreToWin}", new Vector2(40, 40), Color.White);

        }
    }
}
