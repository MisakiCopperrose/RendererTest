using System.Diagnostics;
using Bgfx;
using StbImageSharp;

namespace RendererAbstractionTest.Renderer.Utils;

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

    public enum ExtensionSupport
    {
        Native,
        Stb,
        Unsupported
    }

    public static ExtensionSupport CheckForNativeSupport(string filePath)
    {
        var extension = FileUtils.GetExtension(filePath);
        
        if (NativelySupportedFileTypes.Contains(extension))
        {
            return ExtensionSupport.Native;
        }

        if (StbSupportedFileTypes.Contains(extension))
        {
            return ExtensionSupport.Stb;
        }

        Debug.WriteLine($"File extension {extension} of {Path.GetFileName(filePath)} is not supported!");

        return ExtensionSupport.Unsupported;
    }

    public static void CreateNativeTexture(string filepath, out bgfx.TextureHandle textureHandle,
        out bgfx.TextureInfo textureInfo)
    {
        textureHandle = default;
        textureInfo = new bgfx.TextureInfo();

        var data = File.ReadAllBytes(filepath);
        var handle = MemoryUtils.Create(data);

        fixed (bgfx.TextureInfo* textureInfoP = &textureInfo)
        {
            textureHandle = bgfx.create_texture(handle, (ulong)bgfx.TextureFlags.None, 0, textureInfoP);
        }

        Debug.Assert(textureHandle.Valid, $"{Path.GetFileName(filepath)} is corrupted or not supported!");
    }

    public static void CreateStbTexture(string filepath, bool hasMips, ushort layerCount,
        out bgfx.TextureHandle textureHandle, out bgfx.TextureInfo textureInfo)
    {
        textureHandle = default;
        textureInfo = new bgfx.TextureInfo();
        
        ReadFileStb(filepath, out var imageResult, out var imageInfo);

        if (imageResult is not null && imageInfo.HasValue)
        {
        }
        
        Debug.Assert(textureHandle.Valid, $"{Path.GetFileName(filepath)} is corrupted or not supported!");
    }

    public static void ReadFileStb(string filepath, out ImageResult? imageResult, out ImageInfo? imageInfo)
    {
        try
        {
            var stream = File.OpenRead(filepath);

            imageResult = ImageResult.FromStream(stream);
            imageInfo = ImageInfo.FromStream(stream);
        }
        catch (Exception e)
        {
            imageResult = default;
            imageInfo = default;
            
            Debug.WriteLine(e);
        }
    }
}