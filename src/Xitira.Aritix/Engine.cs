using System;
using Xitira.Aritix.Annex;
using Xitira.Aritix.Input;
using Xitira.Aritix.Scene;
using Xitira.Aritix.Systems.Log;

namespace Xitira.Aritix;

/// <summary>
/// The Engine class serves as the core of the Aritix framework, managing essential components such as input, scenes, and logging while enabling central configuration for the application.
/// </summary>
public class Engine
{
    private Game ActiveGame;
    private GraphicsDeviceManager Gdm;
    
    public InputManager InputManager;
    public SceneManager SceneManager;
    public ContentManager ContentManager;

    public ILogSystem Logger { get; private set; }


    public Engine(GraphicsDeviceManager graphics, Game game)
    {
        this.Gdm = graphics;
        this.ActiveGame = game;
        
        InputManager = new InputManager();
        SceneManager = new SceneManager(Gdm, ActiveGame.Services);
        
        Logger = new NullLogSystem();
    }



    /// <summary>
    /// Gets a Rectangle representing the current viewport dimensions.
    /// </summary>
    /// <returns>A Rectangle with position (0,0) and dimensions matching the current graphics viewport.</returns>
    public Rectangle GetViewport()
    {
        return new Rectangle(0, 0, Gdm.GraphicsDevice.Viewport.Width, Gdm.GraphicsDevice.Viewport.Height);
    }

    
    /// <summary>
    /// Creates a new texture with the specified dimensions filled with a solid color.
    /// </summary>
    /// <param name="width">The width of the texture in pixels.</param>
    /// <param name="height">The height of the texture in pixels.</param>
    /// <param name="color">The color to fill the texture with.</param>
    /// <returns>A new Texture2D instance filled with the specified color.</returns>
    public Texture2D CreateTexture(int width, int height, Color color)
    {
        var texture = new Texture2D(Gdm.GraphicsDevice, width, height);
        var data = new Color[width * height];
        for (int i = 0; i < data.Length; ++i) data[i] = color;
        texture.SetData(data);
        return texture;
    }

    public void SetLogger(ILogSystem logger)
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

    public void Quit()
    {
        this.ActiveGame.Exit();
    }

    public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        this.SceneManager.Draw(gameTime, spriteBatch);
    }
    
}