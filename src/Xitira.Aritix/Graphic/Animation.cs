namespace Xitira.Aritix.Graphic;

public class Animation
{
    private Atlas _atlas;
    private int _currentFrame;
    private double _secondsPerFrame;
    private bool _loop;
    private int _totalFrames;
    private double _countDown;

    public Animation(Atlas atlas, bool loop, double secondsPerFrame)
    {
        _atlas = atlas;
        _loop = loop;
        _secondsPerFrame = secondsPerFrame;
        _totalFrames = _atlas.GetFrameCount();

        _countDown = _secondsPerFrame;
    }

    public Vector2 GetFrameSize()
    {
        return _atlas.GetFrameSize().ToVector2();
    }
    
    public Rectangle GetFrameCollision(Vector2 position)
    {
        return _atlas.GetFrameCollision(position);
    }

    public void Draw(SpriteBatch spriteBatch, Vector2 position, bool flipped)
    {
        _atlas.Draw(spriteBatch, _currentFrame, position, flipped);
    }

    public void Update(GameTime gameTime)
    {
        _countDown -= gameTime.ElapsedGameTime.TotalSeconds;

        if (_countDown <= 0 && _loop)
        {
            _currentFrame++;

            if (_currentFrame >= _totalFrames)
            {
                _currentFrame = 0;
            }

            _countDown = _secondsPerFrame;
        }
    }
}