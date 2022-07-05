using System.Drawing;
using Bgfx;
using BgfxRenderer.Enums;
using BgfxRenderer.Utils;
using StbImageSharp;

namespace BgfxRenderer.Resources.Textures;

// TODO: maybe add image-sharp as first choice and stb as fallback?
public static unsafe class TextureUtils
{
    private static readonly string[] StbSupportedFileTypes =
    {
        ".png", ".jpg", ".bmp", ".tga", ".psd", ".gif", ".hdr"
    };
 
    private static readonly string[] NativelySupportedFileTypes =
    {
        ".dds", ".ktx", ".pvr"
    };

    public static Texture2D GetTexture2DFromFile(string filename, TextureFlags textureFlags)
    {
        if (!ResourceIndex.TryGetTexturePath(filename, out var path))
            return GetDefaultTexture2D();

        var extension = Path.GetExtension(path);

        if (NativelySupportedFileTypes.Contains(extension) && 
            TryGetNativeTextureFile(path, textureFlags, out var textureHandle, out var textureInfo))
            return new Texture2D(textureHandle, textureInfo, textureFlags);

        if (!StbSupportedFileTypes.Contains(extension)) 
            // TODO: log extension not supported
            return GetDefaultTexture2D();
        
        if (!TryGetStbTextureFile(path, out var imageResult, out var bitsPerChannel))
            return GetDefaultTexture2D();

        var textureFormat = GetStbTextureFormat(imageResult.Comp, bitsPerChannel);

        return new Texture2D(imageResult.Data, new Size(imageResult.Width, imageResult.Height), 1,
            false, textureFormat, textureFlags);
    }

    public static Texture3D GetTexture3DFromFile(string filename, TextureFlags textureFlags)
    {
        if (!ResourceIndex.TryGetTexturePath(filename, out var path))
            return GetDefaultTexture3D();

        var extension = Path.GetExtension(path);

        if (NativelySupportedFileTypes.Contains(extension) && 
            TryGetNativeTextureFile(path, textureFlags, out var textureHandle, out var textureInfo))
            return new Texture3D(textureHandle, textureInfo, textureFlags);

        return GetDefaultTexture3D();
    }

    public static TextureCube GetTextureCubeFromFile(string filename, TextureFlags textureFlags)
    {
        if (!ResourceIndex.TryGetTexturePath(filename, out var path))
            return GetDefaultTextureCube();

        var extension = Path.GetExtension(path);

        if (NativelySupportedFileTypes.Contains(extension) && 
            TryGetNativeTextureFile(path, textureFlags, out var textureHandle, out var textureInfo))
            return new TextureCube(textureHandle, textureInfo, textureFlags);

        if (!StbSupportedFileTypes.Contains(extension)) 
            return GetDefaultTextureCube();
        
        if (!TryGetStbTextureFile(path, out var imageResult, out var bitsPerChannel))
            return GetDefaultTextureCube();

        var textureFormat = GetStbTextureFormat(imageResult.Comp, bitsPerChannel);

        return new TextureCube(imageResult.Data, (ushort)imageResult.Height, 1,
            false, textureFormat, textureFlags);
    }

    private static bool TryGetNativeTextureFile(string filepath, TextureFlags textureFlags,
        out bgfx.TextureHandle textureHandle, out bgfx.TextureInfo textureInfo)
    {
        textureHandle = default;
        textureInfo = default;

        if (!FileUtils.TryReadFile(filepath, out var data))
            return false;

        var memoryPointer = MemoryUtils.GetMemoryPointer(data);
        var textureInfoBuffer = new bgfx.TextureInfo();

        textureHandle = bgfx.create_texture(memoryPointer, (ulong)textureFlags, 0, &textureInfoBuffer);
        textureInfo = textureInfoBuffer;

        return true;
    }

    private static bool TryGetStbTextureFile(string filepath, out ImageResult imageResult, out int bitsPerChannel)
    {
        imageResult = default!;
        bitsPerChannel = default;

        if (!FileUtils.TryReadFile(filepath, out var data))
            return false;

        imageResult = ImageResult.FromMemory(data);

        if (!FileUtils.TryOpenReadFile(filepath, out var stream))
            return false;
        
        var imageInfoBool = ImageInfo.FromStream(stream);

        if (!imageInfoBool.HasValue)
        {
            // TODO: assertion of value
            return false;
        }

        bitsPerChannel = imageInfoBool.Value.BitsPerChannel;

        return true;
    }

    private static TextureFormat GetStbTextureFormat(ColorComponents colorComponents, int bitsPerChannel)
    {
        return colorComponents switch
        {
            ColorComponents.Grey when bitsPerChannel == 1 => TextureFormat.R1,
            ColorComponents.Grey when bitsPerChannel == 8 => TextureFormat.R8,
            ColorComponents.Grey when bitsPerChannel == 16 => TextureFormat.R16,
            ColorComponents.GreyAlpha when bitsPerChannel == 8 => TextureFormat.RG8,
            ColorComponents.GreyAlpha when bitsPerChannel == 16 => TextureFormat.RG16,
            ColorComponents.RedGreenBlue when bitsPerChannel == 8 => TextureFormat.RGB8,
            ColorComponents.RedGreenBlueAlpha when bitsPerChannel == 4 => TextureFormat.RGBA4,
            ColorComponents.RedGreenBlueAlpha when bitsPerChannel == 8 => TextureFormat.RGBA8,
            ColorComponents.RedGreenBlueAlpha when bitsPerChannel == 16 => TextureFormat.RGBA16,
            _ => TextureFormat.Unknown
        };
    }
    
    public static Texture2D GetDefaultTexture2D()
    {
        throw new NotImplementedException();
    }

    public static Texture3D GetDefaultTexture3D()
    {
        throw new NotImplementedException();
    }

    public static TextureCube GetDefaultTextureCube()
    {
        throw new NotImplementedException();
    }

    public static bool TryReadBackTexture(ITexture texture, out uint framesTillAvailable, out void* data,
        byte mipLevel = 0)
    {
        data = default;
        framesTillAvailable = default;

        if (texture.TextureFlags != TextureFlags.ReadBack)
            return false;

        // TODO: check capabilities for availability

        framesTillAvailable = bgfx.read_texture(texture.Handle, data, mipLevel);

        return true;
    }

    public static bool TryDirectTextureDataAccess(ITexture texture, out void* data)
    {
        data = default;

        // TODO: check capabilities for availability

        return false;

        data = bgfx.get_direct_access_ptr(texture.Handle);

        return true;
    }
}