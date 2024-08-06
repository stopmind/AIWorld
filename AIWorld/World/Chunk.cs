using Teko.Core;
using Teko.Graphics;

namespace AIWorld.World;

public class Chunk(Vector2f position, TileSet tileSet)
{
    private int[] _tiles = new int[Size.X * Size.Y];
    public static readonly Vector2i Size = new(16, 16);

    public readonly Vector2f Position = position;

    public void Draw(GraphicsService graphics)
    {
        graphics.SetContext(new DrawContext(0, -1));
        for (var x = 0; x < Size.X; x++)
        {
            for (var y = 0; y < Size.Y; y++)
            {
                var pos = tileSet.TileSize * new Vector2i(x, y);
                graphics.DrawRect(
                    new RectF(pos.X + Position.X, pos.Y + Position.Y, tileSet.TileSize.X, tileSet.TileSize.Y),
                    Color.White, tileSet.Tiles[_tiles[x + y * tileSet.TileSize.X]]
                );
            }
        }
        graphics.SetContext(null);
    }

    public int GetTile(Vector2i pos)
        => _tiles[pos.X + pos.Y * tileSet.TileSize.X];
    
    public void SetTile(Vector2i pos, int tile)
        => _tiles[pos.X + pos.Y * tileSet.TileSize.X] = tile;
}