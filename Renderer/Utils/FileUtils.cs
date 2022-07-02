namespace Renderer.Utils;

public static class FileUtils
{
    public static string GetExtension(string filepath)
    {
        var extension = Path.GetExtension(filepath);

        return extension switch
        {
            null => throw new ArgumentNullException($"Filepath: {filepath} cannot be null!"),
            "" => throw new Exception($"Filepath: {filepath} cannot be empty!"),
            _ => extension
        };
    }
}