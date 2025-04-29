using System.Collections.Generic;
using System.Threading.Tasks;

namespace Xitira.Aritix.Particle;

public class ParticleManager
{
    private readonly List<global::Xitira.Aritix.Particle.Particle> _particles = new List<global::Xitira.Aritix.Particle.Particle>();
    private readonly List<ParticleEmitter> _particleEmitters = new List<ParticleEmitter>();

    public void AddParticle(global::Xitira.Aritix.Particle.Particle particle)
    {
        _particles.Add(particle);
    }

    public void Update(GameTime gameTime)
    {

        Parallel.ForEach(_particles, p =>
        {
            p.Update(gameTime);
        });

        // foreach (var particle in _particles)
        // {
        //     particle.Update(gameTime);
        // }

        Parallel.ForEach(_particleEmitters, e =>
        {
            e.Update(gameTime);
        });

        // foreach (var emitter in _particleEmitters)
        // {
        //     emitter.Update(gameTime);
        // }

        _particles.RemoveAll(x => x.IsFinished);

    }

    public void AddParticleEmitter(ParticleEmitter e)
    {
        _particleEmitters.Add(e);
    }

    public void Draw(SpriteBatch spriteBatch, GameTime gameTime)
    {
        foreach (var particle in _particles)
        {
            particle.Draw(spriteBatch, gameTime);
        }
    }
}