using Bgfx;

namespace BgfxRenderer.Buffer.Frame;

public static class FrameBufferUtils
{
    internal static bool TryGetTexture(IFrameBuffer framebuffer, out bgfx.TextureHandle textureHandle)
    {
        textureHandle = default;

        if (framebuffer is FrameBufferWindowed)
        {
            // TODO: log error
            return false;
        }
        
        
    }
}