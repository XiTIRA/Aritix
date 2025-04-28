using System;
using System.IO;
using Microsoft.VisualBasic.FileIO;

namespace Aritix.Content;

public class SqLiteContentManager : ContentManager
{
    public string ContentPath { get; set; }

    private IFreeSql _fsql;

    public SqLiteContentManager(GameServiceContainer gsc, string contentPath) : base(gsc)
    {
        SQLitePCL.Batteries_V2.Init();
        string dbPath = Path.Combine(contentPath, "assets.db");

        _fsql = new FreeSql.FreeSqlBuilder()
            .UseConnectionString(FreeSql.DataType.Sqlite, $"Data Source={dbPath}")
            .UseAutoSyncStructure(false)
            .Build();
    }

    protected override Stream OpenStream(string assetName)
    {
        var content = _fsql.Select<XnaContent>().Where(x => x.Name == assetName).First();
        return new MemoryStream(content.Data);
    }
}