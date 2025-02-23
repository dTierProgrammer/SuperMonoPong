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

namespace MonoPongSuper.Script.Scenes
{
    public static class Play
    {
        private static Texture2D rectDebug;
        private static bool showDebug = false;
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
        public static void Initialize(ContentManager cn) 
        {
            screenBounds[0] = new Rectangle(0, 0, 6, 180); // left of screen boundary ( goal )
            screenBounds[1] = new Rectangle(314, 0, 6, 180); // right of screen boundary ( goal )
            screenBounds[2] = new Rectangle(0, 0, 320, 6); // top of screen boundary
            screenBounds[3] = new Rectangle(0, 174, 320, 6); // bottom of screen boundary

            Ball.LoadSounds(cn);
            Paddle.LoadSounds(cn);
        }

        public static void Load(ContentManager cn) 
        {
            bg = cn.Load<Texture2D>("image/gamefield");
            rectDebug = cn.Load<Texture2D>("image/ballSuper");

            Fonts.fonts[0] = cn.Load<SpriteFont>("font/debug");
            Fonts.fonts[1] = cn.Load<SpriteFont>("font/title");

            Texture2D testImage = cn.Load<Texture2D>("image/test");
            Texture2D paddleImg = cn.Load<Texture2D>("image/paddleSuper");
            Texture2D ballImg = cn.Load<Texture2D>("image/ballSuper");

            gameEnd = cn.Load<SoundEffect>("sound/gameEnd");

            float maxSpeed = 3f;

            ball = new Ball(ballImg, new Vector2(160, 90), 2f, players, screenBounds, cn);
            players[0] = new Paddle(paddleImg, new Vector2(10, 90), maxSpeed, screenBounds, cn);

            computersInp[0] = new ComputerController(players[0], ball);
            playersInp[0] = new PlayerController(1, players[0]);

            
            players[1] = new Paddle(paddleImg, new Vector2(306, 90), maxSpeed, screenBounds, cn);
            computersInp[1] = new ComputerController(players[1], ball);
            playersInp[1] = new PlayerController(2, players[1]);
        }

        public static void Update(GameTime gt) 
        {
            KeyboardState currentKBState = Keyboard.GetState();

            if (currentKBState.IsKeyDown(Keys.P) && currentKBState != prevKBState)
            {
                CurrentGameState.PauseGame();
            }
            prevKBState = currentKBState;

            ball.Update();
            foreach (Paddle player in players)
            {
                player.Update(gt);

                if (isPlayerCPU[0])
                    computersInp[0].UpdateControls();
                else
                    playersInp[0].UpdateControls();

                if (isPlayerCPU[1])
                    computersInp[1].UpdateControls();
                else
                    playersInp[1].UpdateControls();
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

            foreach (Paddle player in players)
            {
                player.Draw(_spriteBatch);
            }
            ball.Draw(_spriteBatch);


            if (showScore)
            {
                _spriteBatch.DrawString(Fonts.fonts[1], $"{players[0].score}", new Vector2(100, 9), Color.White);
                _spriteBatch.DrawString(Fonts.fonts[1], $"{players[1].score}", new Vector2(205, 9), Color.White);
            }


            // debug info
            if (showDebug)
            {
                _spriteBatch.Draw(rectDebug, screenBounds[2], Color.Red);
                _spriteBatch.Draw(rectDebug, screenBounds[3], Color.Red);
                _spriteBatch.Draw(rectDebug, screenBounds[0], Color.Green);
                _spriteBatch.Draw(rectDebug, screenBounds[1], Color.Green);
            }
        }

        public static void DrawText(SpriteBatch sp)
        {

            sp.DrawString(Fonts.fonts[0], $"BEST OF: {scoreToWin}", new Vector2(40, 40), Color.White);
            
        }
    }
}
