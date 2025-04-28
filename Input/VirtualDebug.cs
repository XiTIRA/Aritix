namespace Aritix.Input;

public class VirtualDebug
{
    private GraphicsDeviceManager _graphicsDevice;
    private SpriteFont _font;
    
    private double _frames = 0;
    private double _ticks = 0;
    
    private double _elapsed = 0;
    private double _frequency = .5;
    
    private double _framesPerSecond = 0;
    private double _ticksPerSecond = 0;
    private long _lastDrawCount = 0;
    private long _lastSpriteCount = 0;

    private Texture2D _texture;

    private string _fps = "FPS: 0";

    public VirtualDebug(GraphicsDeviceManager graphicsDevice, SpriteFont sf, Texture2D texture)
    {
        _texture = texture;
        _graphicsDevice = graphicsDevice;
        _font = sf;
    }

    public void Update(GameTime gameTime)
    {
        _ticks++;
        _elapsed += gameTime.ElapsedGameTime.TotalSeconds;
        if (_elapsed > _frequency)
        {
            _framesPerSecond = _frames / _elapsed;
            _ticksPerSecond = _ticks / _elapsed;

            //var gamePad = inputManager.GetPadType();

            _fps = $"FPS: {_framesPerSecond:F2} | TPS: {_ticksPerSecond:F2} | Draw: {_lastDrawCount} | Sprites: {_lastSpriteCount} ";
            
            _frames = 0;
            _ticks = 0;
            _elapsed = 0;
        }
    }

    public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        _frames++;

        _lastDrawCount = _graphicsDevice.GraphicsDevice.Metrics.DrawCount;
        _lastSpriteCount = _graphicsDevice.GraphicsDevice.Metrics.SpriteCount;
        
        spriteBatch.Begin();
        spriteBatch.Draw(_texture, new Vector2(0,0), Color.White);
        spriteBatch.DrawString(_font, _fps, new Vector2(0, 0), Color.White);
        spriteBatch.End();
    }
}