using FreeSql.DataAnnotations;

namespace Aritix.Content;

public class XnaContent
{
    [Column(IsPrimary = true)]
    public string Name {get;set;}
    public byte[] Data {get;set;}
}