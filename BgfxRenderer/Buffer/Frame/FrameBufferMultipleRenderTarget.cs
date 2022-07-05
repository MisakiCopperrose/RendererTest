using Bgfx;
using BgfxRenderer.Resources.Textures;

namespace BgfxRenderer.Buffer.Frame;

public unsafe class FrameBufferMultipleRenderTarget : IFrameBuffer
{
    private readonly TextureAttachment[] _attachments = Array.Empty<TextureAttachment>();
    private readonly ITexture[] _textures = Array.Empty<ITexture>();

    private readonly bool _destroyTextures;

    private string _name = string.Empty;

    public FrameBufferMultipleRenderTarget(TextureAttachment[] attachments, bool destroyTextures)
    {
        var length = attachments.Length;
        var attachmentsHandles = new bgfx.Attachment[length];

        for (var i = 0; i < length; i++)
        {
            attachmentsHandles[i] = attachments[i].Handle;
        }

        var firstAttachmentHandle = attachmentsHandles.First();

        Handle = bgfx.create_frame_buffer_from_attachment(
            (byte)length,
            &firstAttachmentHandle,
            false
        );

        _attachments = attachments;
        _destroyTextures = destroyTextures;
    }

    public FrameBufferMultipleRenderTarget(ITexture[] textures, bool destroyTextures)
    {
        var length = textures.Length;
        var textureHandles = new bgfx.TextureHandle[textures.Length];

        for (var i = 0; i < length; i++)
        {
            textureHandles[i] = textures[i].Handle;
        }

        var firstTextureHandle = textureHandles.First();

        Handle = bgfx.create_frame_buffer_from_handles(
            (byte)length,
            &firstTextureHandle,
            false
        );

        _textures = textures;
        _destroyTextures = destroyTextures;
    }

    public bgfx.FrameBufferHandle Handle { get; }

    public IReadOnlyList<TextureAttachment> Attachments => _attachments;

    public IReadOnlyList<ITexture> Textures => _textures;

    public string Name
    {
        get => _name;
        set
        {
            bgfx.set_frame_buffer_name(Handle, value, value.Length);

            _name = value;
        }
    }

    private void ReleaseUnmanagedResources()
    {
        bgfx.destroy_frame_buffer(Handle);

        if (!_destroyTextures)
            return;

        foreach (var texture in _textures)
        {
            texture.Dispose();
        }
    }

    public void Dispose()
    {
        ReleaseUnmanagedResources();
        GC.SuppressFinalize(this);
    }

    ~FrameBufferMultipleRenderTarget()
    {
        ReleaseUnmanagedResources();
    }
}