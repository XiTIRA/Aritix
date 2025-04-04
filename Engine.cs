using System;
using Aritix.Input;
using Aritix.Log;
using Aritix.Scene;

namespace Aritix;

public class Engine
{
    private Game ActiveGame;
    private GraphicsDeviceManager Gdm;
    
    public InputManager InputManager;
    public SceneManager SceneManager;
    public ContentManager ContentManager;

    public ILogger Logger { get; private set; }
    
    public Engine(GraphicsDeviceManager graphics, Game game)
    {
        this.Gdm = graphics;
        this.ActiveGame = game;
        
        InputManager = new InputManager();
        SceneManager = new SceneManager(Gdm, ActiveGame.Services);
        
        Logger = new NullLogger();
    }

    public Rectangle GetViewport()
    {
        return new Rectangle(0, 0, Gdm.GraphicsDevice.Viewport.Width, Gdm.GraphicsDevice.Viewport.Height);
    }

    public Texture2D CreateTexture(int width, int height, Color color)
    {
        var texture = new Texture2D(Gdm.GraphicsDevice, width, height);
        var data = new Color[width * height];
        for (int i = 0; i < data.Length; ++i) data[i] = color;
        texture.SetData(data);
        return texture;
    }

    public void SetLogger(ILogger logger)
    {
        Logger = logger;
    }

    public void SetFps(int fps)
    {
        ActiveGame.TargetElapsedTime = TimeSpan.FromSeconds(1.0 / fps);
    }

    public void SetResolution( int width, int height)
    {
        Gdm.PreferredBackBufferHeight = height;
        Gdm.PreferredBackBufferWidth = width;

        Gdm.ApplyChanges();
    }

    public void ToggleFullScreen()
    {
        Gdm.ToggleFullScreen();
        Gdm.ApplyChanges();
    }

    public void UncapFps()
    {
        ActiveGame.IsFixedTimeStep = false;
    }

    public void CapFps()
    {
        ActiveGame.IsFixedTimeStep = true;
    }


    public void Update(GameTime gameTime)
    {
        this.InputManager.Update(gameTime);
        this.SceneManager.Update(gameTime);

    }

    public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        this.SceneManager.Draw(gameTime, spriteBatch);
    }
    
}