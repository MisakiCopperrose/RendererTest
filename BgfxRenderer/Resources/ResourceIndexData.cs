namespace BgfxRenderer.Resources;

public record ResourceIndexData
{
    public Dictionary<string, string> Textures = new();

    public Dictionary<string, string> Shaders = new();

    public Dictionary<string, string> Meshes = new();
}