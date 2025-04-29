using System.Collections.Generic;
using Xitira.Aritix.Extensions;

namespace Xitira.Aritix.Graphic;

public class Atlas
{
    private Texture2D _texture;
    private Point _tileSize = Point.Zero;
    private Point _gridSize = Point.Zero;

    private Vector2 _origin;
    private float _scale;
    private float _rotation;
    private bool _flippedX;
    private Color _color;
    private float _opacity;

    private Dictionary<int, Rectangle> _frames;
    

    public Atlas(Texture2D texture, Point splitSize, bool isTileSize = true)
    {
        _texture = texture;
        _frames = new Dictionary<int, Rectangle>();

        if (isTileSize)
        {
            _tileSize = splitSize;
            _gridSize.Y = _texture.Width / _tileSize.X; // Divide width by the columns
            _gridSize.X = _texture.Height / _tileSize.Y; // Divide height by the rows
        }
        else
        {
            _gridSize = splitSize; 
            _tileSize.X = _texture.Width / _gridSize.Y; // Divide width by the columns
            _tileSize.Y = _texture.Height / _gridSize.X; // Divide height by the rows
        }

        int frame = 0;
        for (int x = 0; x < _gridSize.X; x++)
        {
            for (int y = 0; y < _gridSize.Y; y++)
            {

                _frames.Add(frame, new Rectangle(y * _tileSize.X, x * _tileSize.Y, _tileSize.X, _tileSize.Y));
                frame++;
            }
        }

        _origin = Vector2.Zero;
        _flippedX = false;
        _rotation = 0.0f;
        _scale = 1.0f;
        _opacity = 1.0f;
        _color = Color.White;
    }
    
    public void SetOrigin(Origins origin)
    {
        _origin = Origin.GetVector(_gridSize.ToVector2(), origin);
    }

    public void SetScale(float scale)
    {
        _scale = scale;
    }

    public void SetColor(Color color)
    {
        _color = color;
    }

    public void SetRotation(float rotation)
    {
        _rotation = rotation;
    }
    
    public void SetOpacity(float opacity)
    {
        _opacity = opacity;
    }
    
    public void FlipX()
    {
        _flippedX = !_flippedX;
    }
    
    public void SetFlipX(bool flipX)
    {
        _flippedX = flipX;
    }

    public int GetFrameCount()
    {
        return _frames.Count;
    }

    public Point GetFrameSize()
    {
        return _tileSize;
    }
    
    public Rectangle GetFrameCollision( Vector2 position)
    {
        return new Rectangle((int)_origin.X+(int)position.X,(int)_origin.Y+(int)position.Y,_tileSize.X,_tileSize.Y);
    }

    public void Draw(
        SpriteBatch sb,
        int frame,
        Vector2 position,
        bool flipped = false
    )
    {
        sb.Draw(
            _texture,
            position, // Vector2
            _frames[frame],
            _color * _opacity,
            _rotation,
            _origin,
            _scale, // 1.5f = 1.5x larger
            flipped ? SpriteEffects.FlipHorizontally : SpriteEffects.None,
            0.0f);
    }
}