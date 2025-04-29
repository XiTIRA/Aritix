namespace Xitira.Aritix.Extensions;

public static class Graphics
{
    private static readonly Vector2 LineOrigin = new Vector2(0.0f, 0.5f);
    
    public static void DrawLine(
        this SpriteBatch spriteBatch,
        Vector2 start, 
        Vector2 end, 
        Color colour, 
        Texture2D pixel,
        float thickness )
    {
        Vector2 delta = end - start;

        spriteBatch.Draw(
            pixel,
            start,
            null,
            colour,
            delta.ToAngle(),
            LineOrigin,
            new Vector2(delta.Length(),thickness),
            SpriteEffects.None,
            0f
        );
    }
}