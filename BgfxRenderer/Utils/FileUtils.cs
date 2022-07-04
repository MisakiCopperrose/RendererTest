namespace BgfxRenderer.Utils;

public static class FileUtils
{
    public static bool TryReadFile(string filepath, out byte[] fileData)
    {
        fileData = Array.Empty<byte>();

        try
        {
            fileData = File.ReadAllBytes(filepath);

            return true;
        }
        catch (Exception e)
        {
            // TODO: write to logger
            Console.WriteLine(e);

            return false;
        }
    }
    
    public static bool TryOpenReadFile(string filepath, out FileStream fileStream)
    {
        fileStream = default!;

        try
        {
            fileStream = File.OpenRead(filepath);

            return true;
        }
        catch (Exception e)
        {
            // TODO: write to logger
            Console.WriteLine(e);

            return false;
        }
    }
}