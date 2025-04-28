using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Aritix.Particle;

public class MouseEmitter : IEmitter
{
    public Vector2 EmitPosition => Mouse.GetState().Position.ToVector2();
}