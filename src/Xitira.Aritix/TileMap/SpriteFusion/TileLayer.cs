using System.Collections.Generic;

namespace Xitira.Aritix.TileMap.SpriteFusion;

public class TileLayer
{
    public TileLayer()
    {
        Tiles = new List<Tile>();
    }
    public List<Tile> Tiles { get; set; }

    public void AddTile(int tileId, Vector2 gridPosition, bool isCollidable)
    {
        Tiles.Add(new Tile { TileId = tileId, GridPosition = gridPosition, IsCollidable = isCollidable });
    }
}