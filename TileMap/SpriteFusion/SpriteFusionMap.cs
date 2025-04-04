using System.Collections.Generic;

namespace Aritix.TileMap.SpriteFusion;

public class SpriteFusionMap
{
    public int tileSize { get; set; }
    public int mapWidth { get; set; }
    public int mapHeight { get; set; }
    public List<SpriteFusionLayer> layers { get; set; }
}
