using System.Collections.Generic;
using System.Text.Json;
using Aritix.Graphic;

namespace Aritix.TileMap.SpriteFusion;

public class Tilemap
{
    private Vector2 _startPosition;
    private Atlas _atlas;
    private Vector2 _tileSize;
    private List<TileLayer> _tileLayers;
    private bool _showColliders;
    
    public Tilemap(Atlas atlas, Vector2 startPosition, bool showColliders)
    {
        _atlas = atlas;
        _startPosition = startPosition;
        _tileSize = atlas.GetFrameSize().ToVector2();
        _tileLayers = new List<TileLayer>();
        _showColliders = showColliders;
    }

    public List<Rectangle> GetCollisionRectangles()
    {
        List<Rectangle> rectangles = new List<Rectangle>();

        foreach (var layer in _tileLayers)
        {
            foreach (var tile in layer.Tiles)
            {
                if (tile.IsCollidable)
                {
                    rectangles.Add(new Rectangle(
                        (int)((tile.GridPosition.X * _tileSize.X) + _startPosition.X),
                        (int)((tile.GridPosition.Y * _tileSize.Y) + _startPosition.Y),
                        (int)_tileSize.X,
                        (int)_tileSize.Y));
                }
            }
        }

        return rectangles;
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        foreach (var layer in _tileLayers)
        {
            foreach (var tile in layer.Tiles)
            {
                if (tile.IsCollidable && !_showColliders)
                    continue;
                _atlas.Draw(spriteBatch, tile.TileId, (tile.GridPosition * _tileSize) + _startPosition);
            }
        }
    }


    public void AddSpriteFusionTiles(string json)
    {
        SpriteFusionMap map = JsonSerializer.Deserialize<SpriteFusionMap>(json);
        foreach (var layer in map.layers)
        {
            TileLayer tileLayer = new TileLayer();

            foreach (var tile in layer.tiles)
            {
                int tileId = int.Parse(tile.id);
                tileLayer.AddTile(tileId, new Vector2(tile.x, tile.y), layer.collider);
            }

            _tileLayers.Add(tileLayer);
        }

        _tileLayers.Reverse();
    }





}