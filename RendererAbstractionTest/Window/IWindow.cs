namespace RendererAbstractionTest.Window;

public unsafe interface IWindow
{
    int Width { get; }

    int Height { get; }

    bool WindowShouldClose { get; }

    void PollEvents();

    void* NativeWindowHandle(out void* display);
}