namespace Aritix.Collision;

public class Rect : ICollidable
{
    public float Width;
    public float Height;
    public Vector2 Origin;
    public Vector2 Position;

    public Rect(float x, float y, float width, float height)
    {
        Origin = new Vector2(x, y);
        Width = width;
        Height = height;
        Position = new Vector2(x, y);
    }

    public double GetArea()
    {
        return Width * Height;
    }
}