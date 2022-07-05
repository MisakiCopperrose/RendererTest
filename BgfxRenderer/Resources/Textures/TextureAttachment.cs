using Bgfx;
using BgfxRenderer.Enums;

namespace BgfxRenderer.Resources.Textures;

public unsafe struct TextureAttachment
{
    public TextureAttachment(ITexture texture, Access access, ushort layer, ushort layerCount, ushort mipLevel,
        bool autoGenMips)
    {
        var attachment = new bgfx.Attachment();
        var resolveFlags = autoGenMips ? bgfx.ResolveFlags.AutoGenMips : bgfx.ResolveFlags.None;

        bgfx.attachment_init(
            &attachment,
            texture.Handle,
            (bgfx.Access)access,
            layer,
            layerCount,
            mipLevel,
            (byte)resolveFlags
        );

        Handle = attachment;
        Access = access;
    }

    public bgfx.Attachment Handle { get; }

    public Access Access { get; }

    public string Name { get; set; } = string.Empty;
}