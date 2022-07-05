using System.Drawing;
using Bgfx;
using BgfxRenderer.Enums;
using BgfxRenderer.Utils;

namespace BgfxRenderer.Resources.Textures;

public unsafe class Texture2D : ITexture
{
    public Texture2D(Size textureSize, ushort layerCount, bool hasMips, TextureFormat textureFormat,
        TextureFlags textureFlags)
    {
        // TODO: log assertion of bgfx.is_texture_valid();

        Handle = bgfx.create_texture_2d(
            (ushort)textureSize.Width,
            (ushort)textureSize.Height,
            hasMips,
            layerCount,
            (bgfx.TextureFormat)textureFormat,
            (ulong)textureFlags,
            null
        );

        // TODO: log valid assertion

        TextureSize = textureSize;
        LayerCount = layerCount;
        MipMaps = hasMips;
        TextureFormat = textureFormat;
        TextureFlags = textureFlags;
        ReadOnly = false;
    }

    public Texture2D(BackbufferRatio backbufferRatio, ushort layerCount, bool hasMips, TextureFormat textureFormat,
        TextureFlags textureFlags)
    {
        Handle = bgfx.create_texture_2d_scaled(
            (bgfx.BackbufferRatio)backbufferRatio,
            hasMips,
            layerCount,
            (bgfx.TextureFormat)textureFormat,
            (ulong)textureFlags
        );
        
        LayerCount = layerCount;
        MipMaps = hasMips;
        TextureFormat = textureFormat;
        TextureFlags = textureFlags;
    }

    public Texture2D(byte[] data, Size textureSize, ushort layerCount, bool hasMips, TextureFormat textureFormat,
        TextureFlags textureFlags)
    {
        // TODO: log assertion of bgfx.is_texture_valid();

        var memoryPointer = MemoryUtils.GetMemoryPointer(data);

        Handle = bgfx.create_texture_2d(
            (ushort)textureSize.Width,
            (ushort)textureSize.Height,
            hasMips,
            layerCount,
            (bgfx.TextureFormat)textureFormat,
            (ulong)textureFlags,
            memoryPointer
        );

        // TODO: log valid assertion

        TextureSize = textureSize;
        LayerCount = layerCount;
        MipMaps = hasMips;
        TextureFormat = textureFormat;
        TextureFlags = textureFlags;
        ReadOnly = true;
    }

    public Texture2D(void* data, uint dataSize, Size textureSize, ushort layerCount, bool hasMips,
        TextureFormat textureFormat, TextureFlags textureFlags)
    {
        // TODO: log assertion of bgfx.is_texture_valid();

        var memoryPointer = bgfx.make_ref(data, dataSize);

        Handle = bgfx.create_texture_2d(
            (ushort)textureSize.Width,
            (ushort)textureSize.Height,
            hasMips,
            layerCount,
            (bgfx.TextureFormat)textureFormat,
            (ulong)textureFlags,
            memoryPointer
        );

        // TODO: log valid assertion

        TextureSize = textureSize;
        LayerCount = layerCount;
        MipMaps = hasMips;
        TextureFormat = textureFormat;
        TextureFlags = textureFlags;
        ReadOnly = true;
    }

    internal Texture2D(bgfx.TextureHandle handle, bgfx.TextureInfo textureInfo, TextureFlags textureFlags)
    {
        Handle = handle;
        TextureSize = new Size(textureInfo.width, textureInfo.height);
        LayerCount = textureInfo.numLayers;
        MipMaps = textureInfo.numMips > 0;
        TextureFormat = (TextureFormat)textureInfo.format;
        TextureFlags = textureFlags;
        ReadOnly = true;
    }

    public bgfx.TextureHandle Handle { get; }

    public string Name { get; set; } = string.Empty;

    public Size TextureSize { get; private set; }

    public Point TextureOffset { get; private set; } = Point.Empty;

    public ushort LayerCount { get; }

    public bool MipMaps { get; }

    public TextureFormat TextureFormat { get; }

    public TextureFlags TextureFlags { get; }

    public bool ReadOnly { get; }

    public void Update(byte[] data, ushort layer, byte mipLevel, Point offset, Size size)
    {
        if (ReadOnly)
        {
            // TODO: log error
            return;
        }

        var memoryPointer = MemoryUtils.GetMemoryPointer(data);

        bgfx.update_texture_2d(
            Handle,
            layer,
            mipLevel,
            (ushort)offset.X,
            (ushort)offset.Y,
            (ushort)size.Width,
            (ushort)size.Height,
            memoryPointer,
            ushort.MaxValue
        );

        // TODO: log valid assertion

        TextureOffset = offset;
        TextureSize = size;
    }

    public void Update(void* data, uint dataSize, ushort layer, byte mipLevel, Point offset, Size size)
    {
        if (ReadOnly)
        {
            // TODO: log error
            return;
        }

        var memoryPointer = bgfx.make_ref(data, dataSize);

        bgfx.update_texture_2d(
            Handle,
            layer,
            mipLevel,
            (ushort)offset.X,
            (ushort)offset.Y,
            (ushort)size.Width,
            (ushort)size.Height,
            memoryPointer,
            ushort.MaxValue
        );

        // TODO: log valid assertion

        TextureOffset = offset;
        TextureSize = size;
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

    ~Texture2D()
    {
        ReleaseUnmanagedResources();
    }
}