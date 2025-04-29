using System.Collections.Generic;

namespace Xitira.Aritix.Scene;

public class SceneManager
{
    private Stack<IScene> _scenes = new();
    private GraphicsDeviceManager _graphicsDevice;
    private GameServiceContainer _services;
    private ContentManager _content;
    
    public SceneManager(GraphicsDeviceManager gd, GameServiceContainer services)
    {
        _graphicsDevice = gd;
        _services = services;
        _content = NewContentManager();
      }
    
    public ContentManager NewContentManager(string rootDirectory = "Content")
    {
        return new ContentManager(_services, rootDirectory);
    }
    
    public void Push(IScene scene)
    {
        _scenes.Push(scene);
    }

    public void Clear()
    {
        _scenes.Clear();
    }

    public void Replace(IScene scene)
    {
        var sc = _scenes.Pop();
        sc.Dispose();
        _scenes.Push(scene);
    }

    public void Pop()
    {
        _scenes.Pop();
    }

    public IScene Peek()
    {
        return _scenes.Peek();
    }
    
    public void Update(GameTime gameTime)
    {
        _scenes.Peek().Update(gameTime);
    }

    public void Draw(GameTime gt, SpriteBatch sb)
    {
        _graphicsDevice.GraphicsDevice.Clear(_scenes.Peek().BackgroundColor);
        _scenes.Peek().Draw(gt, sb);
    }
}