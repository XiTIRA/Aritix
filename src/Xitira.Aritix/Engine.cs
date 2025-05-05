using System;
using MonoGame.Framework.Utilities;
using Xitira.Aritix.Annex;
using Xitira.Aritix.Extensions;
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
    
    public bool IsResizing = false;
    public Rectangle RenderDestination;
    public Rectangle RenderNative;
    public RenderTarget2D RenderTarget;

    public ILogSystem Logger { get; private set; }


    public Engine(GraphicsDeviceManager graphics, Game game, Point nativeSize, int initialScale)
    {
        Gdm = graphics;
        ActiveGame = game;
        
        InputManager = new InputManager();
        SceneManager = new SceneManager(Gdm, ActiveGame.Services);
        Logger = new NullLogSystem();
        
        if (PlatformInfo.MonoGamePlatform == MonoGamePlatform.Android)
        {
            Gdm.IsFullScreen = true;
        } else
        {
            Gdm.PreferredBackBufferWidth = nativeSize.X * initialScale;
            Gdm.PreferredBackBufferHeight = nativeSize.Y * initialScale;
            Gdm.ApplyChanges();
        }
        
        ActiveGame.Window.ClientSizeChanged += WindowRezise; 
        
        RenderNative = new Rectangle(0, 0, nativeSize.X, nativeSize.Y);
        RenderTarget = new RenderTarget2D(Gdm.GraphicsDevice, nativeSize.X, nativeSize.Y);
        
        Gdm.ApplyChanges();
        CalculateRenderDestination();
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

    public Vector2 ScaleWorldPoint(Vector2 input)
    {
        var position = new Vector2();
        position.X = input.X.MapClamp(0, RenderDestination.Width,0,RenderNative.Width);
        position.Y = input.Y.MapClamp(0, RenderDestination.Height,0,RenderNative.Height);
        return position;
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
    
    public void WindowRezise(object? sender, EventArgs args) 
    {
        if (!IsResizing && ActiveGame.Window.ClientBounds is { Width: > 0, Height: > 0 })
        {
            IsResizing = true;
            CalculateRenderDestination();
            IsResizing = false;
        }
    }

    public void AllowWindowResize()
    {
        ActiveGame.Window.AllowUserResizing = true;
    }
    
    private void CalculateRenderDestination()
    {
        Point size = ActiveGame.GraphicsDevice.Viewport.Bounds.Size;
        RenderDestination = new Rectangle(0, 0, size.X, size.Y);
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