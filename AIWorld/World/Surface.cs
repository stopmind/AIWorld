using Teko.Core;
using Teko.Graphics;

namespace AIWorld.World;

public class Surface(TileSet tileSet)
{
    private readonly Dictionary<Vector2i, Chunk> _chunks = new();
    
    private Chunk GetChunk(Vector2i pos)
    {
        if (_chunks.TryGetValue(pos, out var chunk))
            return chunk;

        chunk = new Chunk((pos * Chunk.Size * tileSet.TileSize).ToFloat(), tileSet);
        _chunks[pos] = chunk;
        return chunk;
    }

    private Vector2i GlobalToLocal(Vector2i pos)
    {
        var result = new Vector2i(
            pos.X% Chunk.Size.X,
            pos.Y% Chunk.Size.Y
        );

        if (result.X < 0) result.X += Chunk.Size.X;
        if (result.Y < 0) result.Y += Chunk.Size.Y;

        return result;
    }

    private Vector2i GlobalToChunk(Vector2i pos)
        => pos / Chunk.Size;

    public int GetTile(Vector2i pos)
        => GetChunk(GlobalToChunk(pos)).GetTile(GlobalToLocal(pos));

    public void SetTile(Vector2i pos, int tile)
        => GetChunk(GlobalToChunk(pos)).SetTile(GlobalToLocal(pos), tile);
    
    public void Draw(GraphicsService graphics, Camera camera)
    {
        foreach (var (_, chunk) in _chunks)
            chunk.Draw(graphics, camera);
    }
}