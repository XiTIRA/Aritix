using System.IO;
using LiteDB;

namespace Xitira.Aritix.Content;

public class LiteDbContentManager : ContentManager
{
    public string ContentPath { get; set; }

    private LiteDatabase _litedb;
    private ILiteStorage<string> _storage;
    private MemoryStream _ms;

    public LiteDbContentManager(GameServiceContainer gsc, string contentPath) : base(gsc)
    {
        ContentPath = contentPath;

        _ms = new MemoryStream();
        TitleContainer.OpenStream(Path.Combine(contentPath, "assets.ldb")).CopyTo(_ms);
        _litedb = new LiteDatabase(_ms);

        _storage = _litedb.GetStorage<string>();
    }

    protected override Stream OpenStream(string assetName)
    {
        return _storage.OpenRead(assetName);
    }
}