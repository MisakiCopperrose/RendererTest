using System.Drawing;
using Bgfx;
using BgfxRenderer.Enums;
using BgfxRenderer.Utils;

namespace BgfxRenderer.Resources.Textures;

public unsafe class TextureCube : ITexture
{
    public TextureCube(ushort textureSize, ushort layerCount, bool hasMips, TextureFormat textureFormat,
        TextureFlags textureFlags)
    {
        Handle = bgfx.create_texture_cube(
            textureSize,
            hasMips,
            layerCount,
            (bgfx.TextureFormat)textureFormat,
            (ulong)textureFlags,
            null
        );

        TextureSize = new Size(textureSize, textureSize);
        LayerCount = layerCount;
        MipMaps = hasMips;
        TextureFormat = textureFormat;
        TextureFlags = textureFlags;
        ReadOnly = false;
    }

    public TextureCube(byte[] data, ushort textureSize, ushort layerCount, bool hasMips, TextureFormat textureFormat,
        TextureFlags textureFlags)
    {
        // TODO: log assertion of bgfx.is_texture_valid();

        var memoryPointer = MemoryUtils.GetMemoryPointer(data);

        Handle = bgfx.create_texture_cube(
            textureSize,
            hasMips,
            layerCount,
            (bgfx.TextureFormat)textureFormat,
            (ulong)textureFlags,
            memoryPointer
        );

        // TODO: log valid assertion

        TextureSize = new Size(textureSize, textureSize);
        LayerCount = layerCount;
        MipMaps = hasMips;
        TextureFormat = textureFormat;
        TextureFlags = textureFlags;
        ReadOnly = true;
    }
    
    public TextureCube(void* data, uint dataSize, ushort textureSize, ushort layerCount, bool hasMips,
        TextureFormat textureFormat, TextureFlags textureFlags)
    {
        // TODO: log assertion of bgfx.is_texture_valid();

        var memoryPointer = bgfx.make_ref(data, dataSize);

        Handle = bgfx.create_texture_cube(
            textureSize,
            hasMips,
            layerCount,
            (bgfx.TextureFormat)textureFormat,
            (ulong)textureFlags,
            memoryPointer
        );

        // TODO: log valid assertion

        TextureSize = new Size(textureSize, textureSize);
        LayerCount = layerCount;
        MipMaps = hasMips;
        TextureFormat = textureFormat;
        TextureFlags = textureFlags;
        ReadOnly = true;
    }
    
    internal TextureCube(bgfx.TextureHandle handle, bgfx.TextureInfo textureInfo, TextureFlags textureFlags)
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

    ~TextureCube()
    {
        ReleaseUnmanagedResources();
    }
}