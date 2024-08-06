using AIWorld.World;
using Teko.Core;
using Teko.Graphics;
using Teko.Resources;

namespace AIWorld.Scenes;

public class BootstrapScene : Scene
{
    private GraphicsService? _graphics;
    private Texture? _background;
    
    public override void Ready()
    {
        _background = Game.GetService<ResourcesLoader>().LoadResource<Texture>("Bootstrap/Bg.png");
        _graphics = Game.GetService<GraphicsService>();
        _graphics.SetLayersCount(1);

        Game.Scene = new WorldScene();
    }

    public override void Update(float delta)
    {
        
    }

    public override void Draw(float delta)
    {
        _graphics!.DrawSprite(Vector2f.Zero, Color.White, _background!);
    }

    public override void OnClose()
        => Game.Exit();
}