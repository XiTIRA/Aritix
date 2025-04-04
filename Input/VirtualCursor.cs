namespace Aritix.Input;

public class VirtualCursor
{
    private Texture2D _mouse;
    
    private Vector2 _position;
    private Vector2 _origin;

    
    public VirtualCursor(Texture2D mouse, Vector2 origin)
    {
        _mouse = mouse;
        _origin = origin;
    }
    
    public void Update(GameTime gameTime, InputManager inputManager)
    {
        _position = inputManager.Axes(AxisTypes.Mouse);
    }

    public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(_mouse, _position, null,Color.White, 0f, _origin, 1f, SpriteEffects.None, 0f);
    }
}