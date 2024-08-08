using Teko.Core;
using Teko.Graphics;
using Teko.Input;
using Teko.Resources;

namespace AIWorld.World;

public class WorldScene : Scene
{
    private GraphicsService? _graphics;
    private Input? _input;
    private readonly Camera _camera = new();
    private Surface? _surface;
    
    public override void Ready()
    {
        _input = Game.GetService<Input>();
        _input.SetKeyboardEvent("camera_up", Key.W);
        _input.SetKeyboardEvent("camera_down", Key.S);
        _input.SetKeyboardEvent("camera_left", Key.A);
        _input.SetKeyboardEvent("camera_right", Key.D);
        
        _input.SetKeyboardEvent("camera_zoom", Key.O);
        _input.SetKeyboardEvent("camera_unzoom", Key.P);
        
        _graphics = Game.GetService<GraphicsService>();
        _graphics.FillColor = new Color(0x1c1c1fFF);
        _graphics.SetLayersCount(2);

        _surface = new Surface(Game.GetService<ResourcesLoader>().LoadResource<TileSet>("World/tileset.json")!);
        new StdSurfaceGenerator().Generate(_surface, 0);
    }

    public override void Update(float delta)
    {
        _camera.Update(_graphics!, _input!, delta);
    }

    public override void Draw(float delta)
    {
        _surface!.Draw(_graphics!, _camera);
    }

    public override void OnClose()
        => Game.Exit();
}