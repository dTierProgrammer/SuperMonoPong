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
using MonoPongSuper.Script.Scenes;
using MonoPongSuper.Script.Global;

namespace MonoPongSuper;

public class Game1 : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    RenderTarget2D scaledDisp;
    //private GameState gameState;

    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
        Window.Title = "Super Mono Pong 1.0.4.1";

        _graphics.PreferredBackBufferWidth = 1280;
        _graphics.PreferredBackBufferHeight = 720;
        _graphics.ApplyChanges();

        scaledDisp = new RenderTarget2D(GraphicsDevice, GraphicsDevice.DisplayMode.Width / 4, GraphicsDevice.DisplayMode.Height / 4);
        // true res: 320, 180
    }

    protected override void Initialize()
    {
        // TODO: Add your initialization logic here

        GetContent.Initialize(this);
        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);

        // TODO: use this.Content to load your game content here
        
        Title.Load();
        Play.Load();
        Pause.Load();
        GameOver.Load();
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        switch (CurrentGameState.currentState)
        {
            case GameState.Title:
                Title.Update(gameTime);
                break;
            case GameState.Game:
                Play.Update(gameTime);
                break;
            case GameState.Pause:
                Pause.Update();
                break;
            case GameState.GameOver:
                GameOver.Update(gameTime);
                break;
        }
        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        
        GraphicsDevice.SetRenderTarget(scaledDisp);
        GraphicsDevice.Clear(Color.Black);
        
        // TODO: Add your drawing code here
        _spriteBatch.Begin(samplerState: SamplerState.PointClamp); // main rendering surface
        switch (CurrentGameState.currentState)
        {
            case GameState.Title:
                Title.Draw(_spriteBatch);
                break;
            case GameState.Game:
                Play.Draw(_spriteBatch);
                break;
            case GameState.Pause:
                Pause.Draw(_spriteBatch);
                break;
            case GameState.GameOver:
                GameOver.Draw(_spriteBatch);
                break;
        }
        _spriteBatch.End();

        GraphicsDevice.SetRenderTarget(null);

        _spriteBatch.Begin(samplerState: SamplerState.PointClamp); // scale main surface to window size
        _spriteBatch.Draw(scaledDisp, new Rectangle(0, 0, GraphicsDevice.DisplayMode.Width, GraphicsDevice.DisplayMode.Height), Color.White);
       
        _spriteBatch.End();

        _spriteBatch.Begin(samplerState: SamplerState.LinearWrap);
        switch (CurrentGameState.currentState)
        {
            case GameState.Title:
                Title.DrawText(_spriteBatch);
                break;
            case GameState.Game:
                Play.DrawText(_spriteBatch, gameTime);
                break;
            case GameState.Pause:
                Pause.DrawText(_spriteBatch);
                break;
            case GameState.GameOver:
                GameOver.DrawText(_spriteBatch);
                break;
        }
        _spriteBatch.End();

        base.Draw(gameTime);
    }
}
