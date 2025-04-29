using System;

namespace Xitira.Aritix.Scene;

public interface IScene : IDisposable
{
    public Color BackgroundColor { get; }
    public void Update(GameTime gameTime);
    public void Draw(GameTime gameTime, SpriteBatch spriteBatch);
}