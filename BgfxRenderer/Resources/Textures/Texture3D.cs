using System.Drawing;
using Bgfx;
using BgfxRenderer.Enums;
using BgfxRenderer.Utils;

namespace BgfxRenderer.Resources.Textures;

public unsafe class Texture3D : ITexture
{
    public Texture3D(Size textureSize, ushort textureDepth, bool hasMips, TextureFormat textureFormat,
        TextureFlags textureFlags)
    {
        // TODO: log assertion of bgfx.is_texture_valid();

        Handle = bgfx.create_texture_3d(
            (ushort)textureSize.Width,
            (ushort)textureSize.Height,
            textureDepth,
            hasMips,
            (bgfx.TextureFormat)textureFormat,
            (ulong)textureFlags,
            null
        );

        // TODO: log valid assertion

        TextureSize = textureSize;
        TextureDepth = textureDepth;
        MipMaps = hasMips;
        TextureFormat = textureFormat;
        TextureFlags = textureFlags;
        ReadOnly = false;
    }

    public Texture3D(byte[] data, Size textureSize, ushort textureDepth, bool hasMips, TextureFormat textureFormat,
        TextureFlags textureFlags)
    {
        // TODO: log assertion of bgfx.is_texture_valid();

        var memoryPointer = MemoryUtils.GetMemoryPointer(data);

        Handle = bgfx.create_texture_3d(
            (ushort)textureSize.Width,
            (ushort)textureSize.Height,
            textureDepth,
            hasMips,
            (bgfx.TextureFormat)textureFormat,
            (ulong)textureFlags,
            memoryPointer
        );

        // TODO: log valid assertion

        TextureSize = textureSize;
        TextureDepth = textureDepth;
        MipMaps = hasMips;
        TextureFormat = textureFormat;
        TextureFlags = textureFlags;
        ReadOnly = true;
    }

    public Texture3D(void* data, uint dataSize, Size textureSize, ushort textureDepth, bool hasMips,
        TextureFormat textureFormat, TextureFlags textureFlags)
    {
        // TODO: log assertion of bgfx.is_texture_valid();

        var memoryPointer = bgfx.make_ref(data, dataSize);

        Handle = bgfx.create_texture_3d(
            (ushort)textureSize.Width,
            (ushort)textureSize.Height,
            textureDepth,
            hasMips,
            (bgfx.TextureFormat)textureFormat,
            (ulong)textureFlags,
            memoryPointer
        );

        // TODO: log valid assertion

        TextureSize = textureSize;
        TextureDepth = textureDepth;
        MipMaps = hasMips;
        TextureFormat = textureFormat;
        TextureFlags = textureFlags;
        ReadOnly = true;
    }

    internal Texture3D(bgfx.TextureHandle handle, bgfx.TextureInfo textureInfo, TextureFlags textureFlags)
    {
        Handle = handle;
        TextureSize = new Size(textureInfo.width, textureInfo.height);
        TextureDepth = textureInfo.depth;
        MipMaps = textureInfo.numMips > 0;
        TextureFormat = (TextureFormat)textureInfo.format;
        TextureFlags = textureFlags;
        ReadOnly = true;
    }

    public bgfx.TextureHandle Handle { get; }

    public string Name { get; set; } = string.Empty;

    public Size TextureSize { get; private set; }

    public ushort TextureDepth { get; private set; }

    public Point TextureOffset { get; private set; } = Point.Empty;

    public ushort TextureDepthOffset { get; private set; }

    public bool MipMaps { get; }

    public TextureFormat TextureFormat { get; }

    public TextureFlags TextureFlags { get; }

    public bool ReadOnly { get; }

    public void Update(byte[] data, byte mipLevel, Point offset, ushort depthOffset, Size size,
        ushort depth)
    {
        if (ReadOnly)
        {
            // TODO: log error
            return;
        }

        var memoryPointer = MemoryUtils.GetMemoryPointer(data);

        bgfx.update_texture_3d(
            Handle,
            mipLevel,
            (ushort)offset.X,
            (ushort)offset.Y,
            depthOffset,
            (ushort)size.Width,
            (ushort)size.Height,
            depth,
            memoryPointer
        );

        // TODO: log valid assertion

        TextureOffset = offset;
        TextureDepthOffset = depthOffset;
        TextureSize = size;
        TextureDepth = depth;
    }

    public void Update(void* data, uint dataSize, byte mipLevel, Point offset, ushort depthOffset, Size size,
        ushort depth)
    {
        if (ReadOnly)
        {
            // TODO: log error
            return;
        }

        var memoryPointer = bgfx.make_ref(data, dataSize);

        bgfx.update_texture_3d(
            Handle,
            mipLevel,
            (ushort)offset.X,
            (ushort)offset.Y,
            depthOffset,
            (ushort)size.Width,
            (ushort)size.Height,
            depth,
            memoryPointer
        );

        // TODO: log valid assertion

        TextureOffset = offset;
        TextureDepthOffset = depthOffset;
        TextureSize = size;
        TextureDepth = depth;
    }

    private void ReleaseUnmanagedResources()
    {
        bgfx.destroy_texture(Handle);
    }

    public void Dispose()
    {
        ReleaseUnmanagedResources();
        GC.SuppressFinalize(this);
    }

    ~Texture3D()
    {
        ReleaseUnmanagedResources();
    }
}