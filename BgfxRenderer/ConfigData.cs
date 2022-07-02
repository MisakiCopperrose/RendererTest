namespace BgfxRenderer;

public record ConfigData
{
    public string ShaderFolderPath { get; } = "Shaders";

    public string TextureFolderPath { get; } = "Textures";

    public string MeshFolderPath { get; } = "Meshes";
}