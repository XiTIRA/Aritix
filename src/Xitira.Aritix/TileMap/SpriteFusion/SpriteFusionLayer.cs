using System.Collections.Generic;

namespace Xitira.Aritix.TileMap.SpriteFusion;

public class SpriteFusionLayer
{
    public string name { get; set; }
    public List<SpriteFusionTile> tiles { get; set; }
    public bool collider { get; set; }
}