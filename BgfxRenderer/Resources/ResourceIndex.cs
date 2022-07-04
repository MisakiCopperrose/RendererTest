namespace BgfxRenderer.Resources;

public static class ResourceIndex
{
    private static ResourceIndexData _indexData = new();

    public static void UpdateResources()
    {
        // TODO: index textures
        
        // TODO: index meshes
        
        // TODO: index shaders, keep current backend in mind
    }

    public static bool TryGetTexturePath(string filename, out string path)
    {
        return _indexData.Textures.TryGetValue(filename, out path!);
    }
    
    public static bool TryGetMeshPath(string filename, out string path)
    {
        return _indexData.Textures.TryGetValue(filename, out path!);
    }
    
    public static bool TryGetShaderPath(string filename, out string path)
    {
        return _indexData.Textures.TryGetValue(filename, out path!);
    }
}