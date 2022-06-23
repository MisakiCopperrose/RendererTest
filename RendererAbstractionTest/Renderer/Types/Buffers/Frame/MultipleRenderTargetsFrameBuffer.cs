using Bgfx;
using RendererAbstractionTest.Renderer.Types.Textures;

namespace RendererAbstractionTest.Renderer.Types.Buffers.Frame;

public unsafe class MultipleRenderTargetsFrameBuffer : IDisposable
{
    private readonly bgfx.FrameBufferHandle _frameBufferHandle;
    private readonly TextureAttachment[] _textureAttachments;
    private readonly bool _destroyWithTextures;

    public MultipleRenderTargetsFrameBuffer(TextureAttachment[] textureAttachments, bool destroyWithTextures)
    {
        var attachmentArray = new bgfx.Attachment[textureAttachments.Length];

        for (var i = 0; i < textureAttachments.Length; i++)
        {
            attachmentArray[i] = textureAttachments[i].AttachmentHandle;
        }

        fixed (bgfx.Attachment* arrayP = attachmentArray)
        {
            _frameBufferHandle = bgfx.create_frame_buffer_from_attachment(
                (byte)textureAttachments.Length,
                arrayP,
                false
            );
        }

        _textureAttachments = textureAttachments;
        _destroyWithTextures = destroyWithTextures;
    }

    private void ReleaseUnmanagedResources()
    {
        bgfx.destroy_frame_buffer(_frameBufferHandle);

        if (!_destroyWithTextures) 
            return;
        
        foreach (var textureAttachment in _textureAttachments)
        {
            textureAttachment.Dispose();
        }
    }

    public void Dispose()
    {
        ReleaseUnmanagedResources();
        GC.SuppressFinalize(this);
    }

    ~MultipleRenderTargetsFrameBuffer()
    {
        ReleaseUnmanagedResources();
    }
}