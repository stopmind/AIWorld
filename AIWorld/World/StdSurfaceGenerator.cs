using Teko.Core;

namespace AIWorld.World;

public class StdSurfaceGenerator : ISurfaceGenerator
{
    private static readonly Vector2i Size = Chunk.Size * new Vector2i(20, 20);
    
    public void Generate(Surface surface, int seed)
    {
        var random = new Random(seed);
        
        GenerateIsland(surface, random.Next());
        GenerateLakes(surface, random.Next());
    }

    private void GenerateIsland(Surface surface, int seed)
    {
        var toEdge = Size.X / 2;
        var center = Size / 2;
        
        var noise = new FastNoiseLite(seed);
        noise.SetNoiseType(FastNoiseLite.NoiseType.OpenSimplex2);
        noise.SetFrequency(0.01f);
        
        for (var y = 0; y < Size.Y; y++)
        {
            for (var x = 0; x < Size.X; x++)
            {
                var height = MathF.Sqrt(MathF.Pow(Math.Abs(center.X - x), 2) + MathF.Pow(Math.Abs(center.Y - y), 2)) + noise.GetNoise(x, y) * toEdge / 4;
                
                var ratio = height / toEdge;
                if (ratio <= 0.8)
                    surface.SetTile(new Vector2i(x, y), 1);
                else if (ratio <= 0.85)
                    surface.SetTile(new Vector2i(x, y), 2);
                else
                    surface.SetTile(new Vector2i(x, y), 3);
            }
        }
    }

    private void GenerateLakes(Surface surface, int seed)
    {
        var noise = new FastNoiseLite(seed);
        noise.SetNoiseType(FastNoiseLite.NoiseType.OpenSimplex2);
        noise.SetFrequency(0.0058f);
        
        noise.SetFractalType(FastNoiseLite.FractalType.Ridged);
        noise.SetFractalOctaves(1);
        
        for (var y = 0; y < Size.Y; y++)
        {
            for (var x = 0; x < Size.X; x++)
            {
                if (surface.GetTile(new Vector2i(x, y)) == 1 && noise.GetNoise(x, y) >= 0.8)
                    surface.SetTile(new Vector2i(x, y), 2);
            }
        }
    }
}