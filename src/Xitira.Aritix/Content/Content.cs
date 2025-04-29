using FreeSql.DataAnnotations;

namespace Xitira.Aritix.Content;

public class Content
{
    [Column(IsPrimary = true)]
    public string Name { get; set; }
    public string Extension { get; set; }
    public byte[] Data { get; set; }
}