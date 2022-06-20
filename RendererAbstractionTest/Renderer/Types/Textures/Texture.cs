using RendererAbstractionTest.Renderer.Utils;

namespace RendererAbstractionTest.Renderer.Types.Textures;

public static class Texture
{
    public static Texture2D CreateTexture2DFromFile(string filePath, bool hasMips = false, ushort layerCount = 1)
    {
        var nativeExtension = TextureUtils.CheckForNativeSupport(filePath);

        if (nativeExtension is TextureUtils.ExtensionSupport.Native)
        {
            TextureUtils.CreateNativeTexture(filePath, out var textureHandle, out var textureInfo);

            return new Texture2D
            {
                Handle = textureHandle.idx,
                Format = textureInfo.format,
                Size = textureInfo.storageSize,
                Width = textureInfo.width,
                Depth = textureInfo.depth,
                Height = textureInfo.height,
                LayerCount = textureInfo.numLayers,
                MipCount = textureInfo.numMips,
                BitsPerPixel = textureInfo.bitsPerPixel,
                ReadOnly = true
            };
        }

        if (nativeExtension is TextureUtils.ExtensionSupport.Stb)
        {
            TextureUtils.CreateStbTexture(filePath, hasMips, layerCount, false, out var textureHandle, out var textureInfo);
            
            return new Texture2D
            {
                Handle = textureHandle.idx,
                Format = textureInfo.format,
                Size = textureInfo.storageSize,
                Width = textureInfo.width,
                Depth = textureInfo.depth,
                Height = textureInfo.height,
                LayerCount = textureInfo.numLayers,
                MipCount = textureInfo.numMips,
                BitsPerPixel = textureInfo.bitsPerPixel,
                ReadOnly = true
            };
        }

        // TODO: default fallback texture

        return null;
    }

    public static Texture3D CreateTexture3DFromFile(string filePath)
    {
        var extension = FileUtils.GetExtension(filePath);
    
        TextureUtils.CheckForNativeSupport(extension);
        TextureUtils.CreateNativeTexture(filePath, out var textureHandle, out var textureInfo);
    
        return new Texture3D
        {
            Handle = textureHandle.idx,
            Format = textureInfo.format,
            Size = textureInfo.storageSize,
            Width = textureInfo.width,
            Depth = textureInfo.depth,
            Height = textureInfo.height,
            LayerCount = textureInfo.numLayers,
            MipCount = textureInfo.numMips,
            BitsPerPixel = textureInfo.bitsPerPixel,
            ReadOnly = true
        };
    }
    //
    // public static TextureCube CreateTextureCubeFromFIle()
    // {
    // }
}