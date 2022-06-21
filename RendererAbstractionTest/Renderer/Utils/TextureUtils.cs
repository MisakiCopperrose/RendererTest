using System.Diagnostics;
using Bgfx;
using StbImageSharp;

namespace RendererAbstractionTest.Renderer.Utils;

public static unsafe class TextureUtils
{
    private const int Texture2DDepth = 1;

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

    public static void CreateStbTexture(string filepath, bool hasMips, ushort layerCount, bool cubeMap,
        out bgfx.TextureHandle textureHandle, out bgfx.TextureInfo textureInfo)
    {
        textureHandle = default;

        ReadImageStb(filepath, out var imageResult, out var imageInfo);

        var format = GetStbFormat(imageResult, imageInfo);
        var handle = MemoryUtils.Create(imageResult.Data);
        var width = (ushort)imageResult.Width;
        var height = (ushort)imageResult.Height;

        Debug.Assert(
            bgfx.is_texture_valid(
                Texture2DDepth,
                cubeMap,
                layerCount,
                format,
                (ulong)bgfx.TextureFlags.None
            ),
            $"Texture: {Path.GetFileName(filepath)} cannot be valid!"
        );

        textureHandle = cubeMap switch
        {
            false => bgfx.create_texture_2d(
                width,
                height,
                hasMips,
                layerCount,
                format,
                (ulong)bgfx.TextureFlags.None,
                handle
            ),
            true => bgfx.create_texture_cube(
                width,
                hasMips,
                layerCount,
                format,
                (ulong)bgfx.TextureFlags.None,
                handle
            )
        };

        fixed (bgfx.TextureInfo* textureInfoP = &textureInfo)
        {
            bgfx.calc_texture_size(
                    textureInfoP,
                    width,
                    height,
                    Texture2DDepth,
                    cubeMap,
                    hasMips,
                    layerCount,
                    format)
                ;
        }

        Debug.Assert(textureHandle.Valid, $"File: {Path.GetFileName(filepath)} is corrupted or not supported!");
    }

    private static bgfx.TextureFormat GetStbFormat(ImageResult imageResult, ImageInfo imageInfo)
    {
        return imageResult.Comp switch
        {
            ColorComponents.RedGreenBlueAlpha when imageInfo.BitsPerChannel == 4 => bgfx.TextureFormat.RGBA4,
            ColorComponents.RedGreenBlueAlpha when imageInfo.BitsPerChannel == 8 => bgfx.TextureFormat.RGBA8,
            ColorComponents.RedGreenBlueAlpha when imageInfo.BitsPerChannel == 16 => bgfx.TextureFormat.RGBA16,
            ColorComponents.RedGreenBlue when imageInfo.BitsPerChannel == 8 => bgfx.TextureFormat.RGB8,
            ColorComponents.Grey when imageInfo.BitsPerChannel == 1 => bgfx.TextureFormat.R1,
            ColorComponents.Grey when imageInfo.BitsPerChannel == 8 => bgfx.TextureFormat.R8,
            ColorComponents.Grey when imageInfo.BitsPerChannel == 16 => bgfx.TextureFormat.R16,
            // Doesn't actually work, will turn into a red green image...
            ColorComponents.GreyAlpha when imageInfo.BitsPerChannel == 8 => bgfx.TextureFormat.RG8,
            // Doesn't actually work, will turn into a red green image...
            ColorComponents.GreyAlpha when imageInfo.BitsPerChannel == 16 => bgfx.TextureFormat.RG16,
            _ => bgfx.TextureFormat.Unknown
        };
    }

    private static void ReadImageStb(string filepath, out ImageResult imageResult, out ImageInfo imageInfo)
    {
        try
        {
            var stream = File.OpenRead(filepath);
            var imageInfoStream = ImageInfo.FromStream(stream);

            Debug.Assert(imageInfoStream is not null,
                $"File: {Path.GetFileName(filepath)} is corrupted or not supported!");

            imageResult = ImageResult.FromStream(stream);
            imageInfo = imageInfoStream.Value;
        }
        catch (Exception e)
        {
            imageResult = default!;
            imageInfo = default;

            Debug.WriteLine(e);
        }
    }
}