using FreeSql.DataAnnotations;

namespace Xitira.Aritix.Content;

public class XnaContent
{
    [Column(IsPrimary = true)]
    public string Name {get;set;}
    public byte[] Data {get;set;}
}