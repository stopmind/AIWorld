using Teko.Core;
using Teko.Graphics;
using Teko.Resources;

namespace AIWorld.World;

public class WorldScene : Scene
{
    private GraphicsService? _graphics;
    private Chunk? _chunk;
    
    public override void Ready()
    {
        _graphics = Game.GetService<GraphicsService>();
        _graphics.FillColor = new Color(0x1c1c1fFF);
        _chunk = new Chunk(Vector2f.Zero,
            Game.GetService<ResourcesLoader>().LoadResource<TileSet>("World/tileset.json")!);
    }

    public override void Update(float delta)
    {
        
    }

    public override void Draw(float delta)
    {
        _chunk?.Draw(_graphics!);
    }

    public override void OnClose()
        => Game.Exit();
}