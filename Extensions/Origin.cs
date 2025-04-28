namespace Aritix.Extensions;

public static class Origin
{
    public static Vector2 GetVector(Vector2 sourceFrameSize, Origins originPoint) => originPoint switch
    {
        Origins.Center => new Vector2((int)(sourceFrameSize.X / 2), (int)(sourceFrameSize.Y / 2)),
        Origins.TopLeft => Vector2.Zero,
        Origins.TopRight => new Vector2(sourceFrameSize.X, 0),
        Origins.BottomLeft => new Vector2(0, sourceFrameSize.Y),
        Origins.BottomRight => new Vector2(sourceFrameSize.X, sourceFrameSize.Y),
        Origins.CenterLeft => new Vector2(0, (int)(sourceFrameSize.Y / 2)),
        Origins.CenterRight => new Vector2(sourceFrameSize.X, (int)(sourceFrameSize.Y / 2)),
        Origins.CenterTop => new Vector2((int)(sourceFrameSize.X / 2), 0),
        Origins.CenterBottom => new Vector2((int)(sourceFrameSize.X / 2), sourceFrameSize.Y),
        _ => Vector2.Zero
    };
}