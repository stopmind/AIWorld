using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Teko.Core;
using Teko.Graphics;
using Teko.Resources;

namespace AIWorld.World;

public class TileSet : IResource
{
    public readonly Vector2i TileSize;
    public readonly Texture[] Tiles;
    
    public static dynamic Load(ResourcesLoader loader, Stream stream)
    {
        var reader = new StreamReader(stream);
        var data = JsonConvert.DeserializeObject<JObject>(reader.ReadToEnd())!;
        reader.Close();

        var atlas = loader.LoadResource<Texture>(data["atlas"]!.Value<string>()!)!;
        var size = new Vector2i(data["tileSize"]![0]!.Value<int>(), data["tileSize"]![1]!.Value<int>());
        var tiles = data["tiles"]!.Value<JArray>()!.Select(o =>
        {
            var pos = size * new Vector2i(o[0]!.Value<int>(), o[1]!.Value<int>());
            return atlas.SubTexture(new RectI(pos, pos + size));
        }).ToArray();

        return new TileSet(size, tiles);
    }

    private TileSet(Vector2i size, Texture[] tiles)
    {
        TileSize = size;
        Tiles = tiles;
    }
}