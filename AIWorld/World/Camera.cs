using Teko.Core;
using Teko.Graphics;
using Teko.Input;

namespace AIWorld.World;

public class Camera
{
    private const float MoveSpeed = 600f;
    private const float ZoomSpeed = 0.8f;
    private const float ZoomMin = 0.4f;
    private const float ZoomMax = 4f;
    
    private Vector2f _position = Vector2f.Zero;
    private float _zoom = 0.4f;

    public RectF VisibleArea = new();
    
    public void Update(GraphicsService graphics, Input input, float delta)
    {
        var move = Vector2f.Zero;
        if (input.IsDown("camera_up")) move += Vector2f.Up;
        if (input.IsDown("camera_down")) move += Vector2f.Down;
        if (input.IsDown("camera_left")) move += Vector2f.Left;
        if (input.IsDown("camera_right")) move += Vector2f.Right;
        _position += move.Normalize() * MoveSpeed / _zoom * delta;

        var zoomChange = 0f;
        if (input.IsDown("camera_zoom"))   zoomChange++;
        if (input.IsDown("camera_unzoom")) zoomChange--;
        _zoom = MathF.Min(ZoomMax, MathF.Max(ZoomMin, _zoom + zoomChange * delta * ZoomSpeed));

        var size = graphics.GetSize().ToFloat() / _zoom;

        VisibleArea.Size = size;
        VisibleArea.Position = _position - size / 2;
        
        graphics.SetView(0, new View(_position, graphics.GetSize().ToFloat() / _zoom));
    }
}