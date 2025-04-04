using System.Collections.Generic;

namespace Aritix.Graphic;

public class Sprite
{
    public Vector2 Position;
    private float _rotation;
    private bool _flippedX;
    
    private  List<Rectangle> _staticCollisions = new();


    private Dictionary<string, Animation> _animations = new();
    private Animation _currentAnimation;
    // private Dictionary<string, IBehaviour> _behaviours = new();
    
    public Sprite(string animationKey, Animation texture, Vector2 position, float rotation)
    {
        _currentAnimation = texture;
        _animations.Add(animationKey,texture);
        Position = position;
        _rotation = rotation;
    }


    
    public List<Rectangle> GetStaticCollisions()
    {
        return _staticCollisions;
    }
    
    public void AddStaticCollision(Rectangle rectangle)
    {
        _staticCollisions.Add(rectangle);
    }
    
    public void AddStaticCollisions(List<Rectangle> rectangles)
    {
        _staticCollisions.AddRange(rectangles);
    }
    
    public Rectangle GetCollisionRectangle()
    {
       return  _currentAnimation.GetFrameCollision(Position);
    }
    
    public void FlipX()
    {
        _flippedX = !_flippedX;
    }
    
    public void SetFlipX(bool flipX)
    {
        _flippedX = flipX;
    }
    
    public void AddAnimation(string name, Animation animation)
    {
        _animations.Add(name,animation);
    }
    
    public void SetAnimation(string name)
    {
        _currentAnimation = _animations[name];
    }
    
    // public  void AddBehaviour(string name, IBehaviour behaviour)
    // {
    //     _behaviours.Add(name, behaviour);
    // }

    public void Update(GameTime gameTime)
    {
        // foreach (var behaviour in _behaviours.Values)
        // {
        //     behaviour.Update(gameTime,  this);
        // }       
        _currentAnimation.Update(gameTime);

    }
    
    public void Move(Vector2 position)
    {
        Position += position;
    }
    
    public void MoveTo(Vector2 position)
    {
        Position = position;
    }
    
    public void Draw(SpriteBatch spriteBatch)
    {
        _currentAnimation.Draw(spriteBatch,Position, _flippedX);
    }
}