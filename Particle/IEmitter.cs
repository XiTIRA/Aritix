using Microsoft.Xna.Framework;

namespace Aritix.Particle;

public interface IEmitter
{
    Vector2 EmitPosition { get; }
}