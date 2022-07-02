using System.Drawing;
using Bgfx;
using Renderer.Enums;

namespace Renderer.Types.Buffers.Frame;

public class FrameBuffer
{
    private readonly bgfx.FrameBufferHandle _frameBufferHandle;

    private string _name = string.Empty;

    public FrameBuffer(Size size)
    {
        _frameBufferHandle = bgfx.create_frame_buffer(
            (ushort)size.Width,
            (ushort)size.Height,
            bgfx.TextureFormat.RGBA8,
            (ulong)(bgfx.SamplerFlags.UClamp | bgfx.SamplerFlags.UClamp)
        );
    }

    public FrameBuffer(BackBufferRatios backBufferRatio)
    {
        _frameBufferHandle = bgfx.create_frame_buffer_scaled(
            (bgfx.BackbufferRatio)backBufferRatio,
            bgfx.TextureFormat.RGBA8,
            (ulong)(bgfx.SamplerFlags.UClamp | bgfx.SamplerFlags.UClamp)
        );
    }

    public string Name
    {
        get => _name;
        set
        {
            bgfx.set_frame_buffer_name(_frameBufferHandle, value, value.Length);

            _name = value;
        }
    }

    public ushort Handle => _frameBufferHandle.idx;

    private void ReleaseUnmanagedResources()
    {
        bgfx.destroy_frame_buffer(_frameBufferHandle);
    }

    public void Dispose()
    {
        ReleaseUnmanagedResources();
        GC.SuppressFinalize(this);
    }

    ~FrameBuffer()
    {
        ReleaseUnmanagedResources();
    }
}