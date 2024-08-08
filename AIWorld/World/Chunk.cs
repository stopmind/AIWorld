using Teko.Core;
using Teko.Graphics;

namespace AIWorld.World;

public class Chunk(Vector2f position, TileSet tileSet)
{
    private int[] _tiles = new int[Size.X * Size.Y];
    public static readonly Vector2i Size = new(16, 16);

    public readonly Vector2f Position = position;

    public void Draw(GraphicsService graphics, Camera camera)
    {
        bool SubCheck(RectF a, RectF b) => 
            ((a.Top < b.Top && b.Top < a.Bottom) || (a.Top < b.Bottom && b.Bottom < a.Bottom)) &&
            ((a.Left < b.Left && b.Left < a.Right) || (a.Left < b.Right && b.Right < a.Right));

        if (!SubCheck(camera.VisibleArea, new RectF(Position, (Size * tileSet.TileSize).ToFloat()))) 
            return;
        
        graphics.SetContext(new DrawContext(0, -1));
        for (var x = 0; x < Size.X; x++)
        {
            for (var y = 0; y < Size.Y; y++)
            {
                var pos = tileSet.TileSize * new Vector2i(x, y);
                graphics.DrawSprite(
                    new Vector2f(pos.X + Position.X, pos.Y + Position.Y),
                    Color.White, tileSet.Tiles[_tiles[x + y * Size.X]]
                );
            }
        }
        graphics.SetContext(null);
    }

    public int GetTile(Vector2i pos)
        => _tiles[pos.X + pos.Y * Size.X];
    
    public void SetTile(Vector2i pos, int tile)
        => _tiles[pos.X + pos.Y * Size.X] = tile;
}