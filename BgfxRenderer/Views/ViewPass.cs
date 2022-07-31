using System.Drawing;
using Bgfx;

namespace BgfxRenderer.Views;

public struct ViewPass
{
    private string _name = string.Empty;
    private Rectangle _rectangle = Rectangle.Empty;

    public ViewPass()
    {
        Id = 0;
    }

    public ushort Id { get; set; }

    public string Name
    {
        get => _name;
        set
        {
            bgfx.set_view_name(Id, value);

            _name = value;
        }
    }

    public Rectangle Rectangle
    {
        get => _rectangle;
        set
        {
            bgfx.set_view_rect(Id, (ushort)value.X, (ushort)value.Y, (ushort)value.Width, (ushort)value.Height);

            _rectangle = value;
        }
    }
}