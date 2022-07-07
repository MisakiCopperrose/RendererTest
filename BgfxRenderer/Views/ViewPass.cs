using Bgfx;

namespace BgfxRenderer.Views;

public struct ViewPass
{
    private string _name = string.Empty;

    public ViewPass()
    {
        
    }
    
    public ushort Id { get; internal set; }

    public string Name
    {
        get => _name;
        set
        {
            bgfx.set_view_name(Id, value);
            
            _name = value;
        }
    }
}