using Bgfx;

namespace BgfxRenderer.Buffers.Frame;

public static class FrameBufferUtils
{
    internal static bool TryGetTexture(IFrameBuffer framebuffer, byte index, out bgfx.TextureHandle textureHandle)
    {
        textureHandle = default;

        if (framebuffer is FrameBufferWindowed)
        {
            // TODO: log error
            return false;
        }

        textureHandle = bgfx.get_texture(framebuffer.Handle, index);

        return true;
    }
}