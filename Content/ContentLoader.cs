using System.IO;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;

namespace Aritix.Content;

public class ContentLoader
{
    private IFreeSql _fsql;
    private GraphicsDevice _graphicsDevice;

    public ContentLoader(GraphicsDevice graphicsDevice, string dbPath)
    {
        _fsql = new FreeSql.FreeSqlBuilder()
            .UseConnectionString(FreeSql.DataType.Sqlite, $"Data Source={dbPath}")
            .UseAutoSyncStructure(false) 
            .Build();
        
        _graphicsDevice = graphicsDevice;
    }

    public ContentBox CreateBox() =>  new ContentBox(this);

    public Texture2D GetSprite(string path)
    {
        var sprite = _fsql.Select<Sprite>().Where(x => x.Path == path).First();
        var stream = new MemoryStream(sprite.Data);
        return Texture2D.FromStream(_graphicsDevice, stream, DefaultColorProcessors.PremultiplyAlpha);
    }

    public SoundEffect GetSound(string path)
    {
        var sound = _fsql.Select<Sound>().Where(x => x.Path == path).First();
        var stream = new MemoryStream(sound.Data);
        return SoundEffect.FromStream(stream);
    }
}