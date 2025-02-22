using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Diagnostics;
using System;
using MonoPongSuper.Script.Controller;
using MonoPongSuper.Script.Game;
using System.Net.Http;
using Microsoft.Xna.Framework.Content;

namespace MonoPongSuper;

public class Game1 : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    RenderTarget2D scaledDisp;

    SpriteFont[] fonts = new SpriteFont[10];

    private Texture2D rectDebug;
    private bool showDebug = false;
    private bool showScore = true;

    private Texture2D bg;

    private Rectangle[] screenBounds = new Rectangle[4]; // Screen boundaries. 0 = Left, 1 = Right, 2 = Upper, 3 = Lower

    private Paddle[] players = new Paddle[2];
    private PlayerController[] playersInp = new PlayerController[2];
    private int[] scores = new int[2];

    private Ball ball;

    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;

        _graphics.PreferredBackBufferWidth = 1280;
        _graphics.PreferredBackBufferHeight = 720;
        _graphics.ApplyChanges();

        scaledDisp = new RenderTarget2D(GraphicsDevice, GraphicsDevice.DisplayMode.Width / 4, GraphicsDevice.DisplayMode.Height / 4);
        // true res: 320, 180

        bg = Content.Load<Texture2D>("image/gamefield");
        rectDebug = Content.Load<Texture2D>("image/ballSuper");
        
    }

    protected override void Initialize()
    {
        // TODO: Add your initialization logic here
        screenBounds[0] = new Rectangle(0, 0, 6, 180); // left of screen boundary ( goal )
        screenBounds[1] = new Rectangle(314, 0, 6, 180); // right of screen boundary ( goal )
        screenBounds[2] = new Rectangle(0, 0, 320, 6); // top of screen boundary
        screenBounds[3] = new Rectangle(0, 174, 320, 6); // bottom of screen boundary

        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);

        // TODO: use this.Content to load your game content here
        fonts[0] = Content.Load<SpriteFont>("font/debug");
        fonts[1] = Content.Load<SpriteFont>("font/title");

        Texture2D testImage = Content.Load<Texture2D>("image/test");
        Texture2D paddleImg = Content.Load<Texture2D>("image/paddleSuper");
        Texture2D ballImg = Content.Load<Texture2D>("image/ballSuper");

        float maxSpeed = 3f;

        players[0] = new Paddle(paddleImg, new Vector2(10, 90), maxSpeed, screenBounds);
        playersInp[0] = new PlayerController(1, players[0]);

        players[1] = new Paddle(paddleImg, new Vector2(306, 90), maxSpeed, screenBounds);
        playersInp[1] = new PlayerController(2, players[1]);

        ball = new Ball(ballImg, new Vector2(160, 90), 2f, players, screenBounds);
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        //scores[0] = players[0].Score; scores[1] = players[1].Score;
        scores[0] = 59;

        foreach (Paddle player in players) 
        {
            player.Update(gameTime);
            foreach(PlayerController controller in playersInp) 
            {
                controller.UpdateControls();
            }
        }

        
        ball.Update();
        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        

        GraphicsDevice.SetRenderTarget(scaledDisp);
        GraphicsDevice.Clear(Color.Black);
        

        // TODO: Add your drawing code here
        _spriteBatch.Begin(); // main rendering surface
        _spriteBatch.Draw(bg, Vector2.Zero, Color.White);

        foreach (Paddle player in players)
        {
            player.Draw(_spriteBatch);
        }
        ball.Draw(_spriteBatch);

        
        if (showScore) 
        {
            _spriteBatch.DrawString(fonts[1], $"{players[0].score}", new Vector2(100, 9), Color.White);
            _spriteBatch.DrawString(fonts[1], $"{players[1].score}", new Vector2(205, 9), Color.White);
        }
        

        // debug info
        if (showDebug) 
        {
            _spriteBatch.Draw(rectDebug, screenBounds[2], Color.Red);
            _spriteBatch.Draw(rectDebug, screenBounds[3], Color.Red);
            _spriteBatch.Draw(rectDebug, screenBounds[0], Color.Green);
            _spriteBatch.Draw(rectDebug, screenBounds[1], Color.Green);
        }
        _spriteBatch.End();

        GraphicsDevice.SetRenderTarget(null);

        _spriteBatch.Begin(samplerState: SamplerState.PointClamp); // scale main surface to window size
        _spriteBatch.Draw(scaledDisp, new Rectangle(0, 0, GraphicsDevice.DisplayMode.Width, GraphicsDevice.DisplayMode.Height), Color.White);

        // debug info
        if (showDebug)
            _spriteBatch.DrawString(fonts[0], $"BallXY: [{ball.pos.X}, {ball.pos.Y}]\n" +
                $"BallVel: [{ball.velocity.X}, {ball.velocity.Y}]\n" +
                $"P1XY: [{players[0].pos.X}, {players[0].pos.Y}]\n" +
                $"P1Vel: [{players[0].velocity.X}, {players[1].velocity.Y}]\n" +
                $"P2XY: [{players[1].pos.X}, {players[1].pos.Y}]\n" +
                $"P2Vel: [{players[1].velocity.X}, {players[1].velocity.Y}]", Vector2.Zero, Color.Yellow);
        
        _spriteBatch.End();


        base.Draw(gameTime);
    }
}
