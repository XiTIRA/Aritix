namespace Xitira.Aritix.Particle;

public class MouseEmitter : IEmitter
{
    public Vector2 EmitPosition => Mouse.GetState().Position.ToVector2();
}