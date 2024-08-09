using AIWorld.Registry;
using AIWorld.World;
using Teko.Core;
using Teko.Graphics;
using Teko.Resources;

namespace AIWorld.Scenes;

public class BootstrapScene : Scene
{
    private GraphicsService? _graphics;
    private Texture? _background;
    private bool _drawed;
    private bool _initStarted;
    
    public override void Ready()
    {
        _background = Game.GetService<ResourcesLoader>().LoadResource<Texture>("Bootstrap/Bg.png");
        _graphics = Game.GetService<GraphicsService>();
        Game.AddService(new RegistryService());
        _graphics.SetLayersCount(1);

        Game.Scene = new WorldScene();
    }

    public override void Update(float delta)
    {
        if (_drawed && !_initStarted)
        {
            _initStarted = true;
            var registry = Game.GetService<RegistryService>();
            registry.Load(Game.GetService<ResourcesLoader>().LoadResource<LoadConfig>("Bootstrap/LoadConfig.json")!);
        }
    }

    public override void Draw(float delta)
    {
        _graphics!.DrawSprite(Vector2f.Zero, Color.White, _background!);
        _drawed = true;
    }

    public override void OnClose()
        => Game.Exit();
}