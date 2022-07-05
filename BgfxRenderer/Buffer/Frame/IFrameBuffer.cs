using Bgfx;

namespace BgfxRenderer.Buffer.Frame;

public interface IFrameBuffer : IDisposable
{
    public bgfx.FrameBufferHandle Handle { get; }

    public string Name { get; set; }
}