using System.Drawing;
using Bgfx;
using BgfxRenderer.Enums;

namespace BgfxRenderer.Buffer.Frame;

public class FrameBuffer : IFrameBuffer
{
    private string _name = string.Empty;

    public FrameBuffer(Size textureSize, TextureFormat textureFormat, TextureFlags textureFlags)
    {
        Handle = bgfx.create_frame_buffer(
            (ushort)textureSize.Width,
            (ushort)textureSize.Height,
            (bgfx.TextureFormat)textureFormat,
            (ulong)textureFlags
        );

        TextureSize = textureSize;
        TextureFormat = textureFormat;
        TextureFlags = textureFlags;
    }

    public FrameBuffer(BackbufferRatio backbufferRatio, TextureFormat textureFormat, TextureFlags textureFlags)
    {
        Handle = bgfx.create_frame_buffer_scaled(
            (bgfx.BackbufferRatio)backbufferRatio,
            (bgfx.TextureFormat)textureFormat,
            (ulong)textureFlags
        );

        TextureFormat = textureFormat;
        TextureFlags = textureFlags;
    }

    public bgfx.FrameBufferHandle Handle { get; }

    public string Name
    {
        get => _name;
        set
        {
            bgfx.set_frame_buffer_name(Handle, value, value.Length);

            _name = value;
        }
    }

    public Size TextureSize { get; } = Size.Empty;

    private TextureFormat TextureFormat { get; }

    private TextureFlags TextureFlags { get; }

    private void ReleaseUnmanagedResources()
    {
        bgfx.destroy_frame_buffer(Handle);
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