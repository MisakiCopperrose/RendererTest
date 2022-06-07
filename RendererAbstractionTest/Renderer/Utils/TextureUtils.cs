using StbImageSharp;

namespace RendererAbstractionTest.Renderer.Utils;

public static class TextureUtils
{
    private static readonly string[] StbSupportedFileTypes =
    {
        ".png", ".jpg", ".bmp", ".tga", ".psd", ".gif", ".hdr"
    };

    private static readonly string[] NativelySupportedFileTypes =
    {
        ".dds", ".ktx", ".pvr"
    };

    public static string CheckExtension(string filepath)
    {
        var extension = Path.GetExtension(filepath);

        return extension switch
        {
            null => throw new ArgumentNullException($"Filepath: {filepath} cannot be null!"),
            "" => throw new Exception($"Filepath: {filepath} cannot be empty!"),
            _ => extension
        };
    }

    public static bool CheckForNativeSupport(string extension)
    {
        if (NativelySupportedFileTypes.Contains(extension))
        {
            return true;
        }

        if (StbSupportedFileTypes.Contains(extension))
        {
            return false;
        }

        throw new Exception($"File extension: {extension} is not supported!");
    }

    public static ImageResult ReadFileStb(string filepath)
    {
        var data = File.ReadAllBytes(filepath);
        
        return ImageResult.FromMemory(data);
    }
}