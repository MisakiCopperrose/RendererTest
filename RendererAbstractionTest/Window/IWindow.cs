namespace RendererAbstractionTest.Window;

public unsafe interface IWindow
{
    public void* WindowHandle { get; }

    public void* DisplayHandle { get; }

    public int Width { get; }
    
    public int Height { get; }
    
    public Action RenderFrame { get; set; }

    public void Start();
}