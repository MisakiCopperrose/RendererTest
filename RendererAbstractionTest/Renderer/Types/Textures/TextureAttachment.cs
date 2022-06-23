using Bgfx;
using RendererAbstractionTest.Renderer.Enums;

namespace RendererAbstractionTest.Renderer.Types.Textures;

public unsafe class TextureAttachment : IDisposable
{
    private readonly bgfx.Attachment _attachment;
    private readonly bool _destroyParent;

    public TextureAttachment(ITexture renderTarget, Access access, ushort layerOrSide, ushort layerCount,
        ushort mipLevel, ResolveFlags resolveFlags, bool destroyParent = false)
    {
        var attachment = new bgfx.Attachment();

        bgfx.attachment_init(
            &attachment,
            new bgfx.TextureHandle { idx = renderTarget.Handle },
            (bgfx.Access)access,
            layerOrSide,
            layerCount,
            mipLevel,
            (byte)resolveFlags
        );

        RenderTarget = renderTarget;
        _destroyParent = destroyParent;

        _attachment = attachment;
    }

    public ushort TextureHandle => _attachment.handle.idx;

    public bgfx.Attachment AttachmentHandle => _attachment;
    
    public ITexture RenderTarget { get; init; }

    public Access Access
    {
        get
        {
            var access = _attachment.access;

            return access switch
            {
                bgfx.Access.Read => Access.Read,
                bgfx.Access.Write => Access.Write,
                bgfx.Access.ReadWrite => Access.ReadWrite,
                bgfx.Access.Count => throw new ArgumentException(),
                _ => throw new ArgumentOutOfRangeException()
            };
        }
    }

    public ushort MipLevel => _attachment.mip;

    public ushort LayerCount => _attachment.numLayers;

    public ushort LayerOrSide => _attachment.layer;

    public ResolveFlags ResolveFlags
    {
        get
        {
            var resolveFlag = _attachment.resolve;

            return resolveFlag switch
            {
                (byte)bgfx.ResolveFlags.None => ResolveFlags.None,
                (byte)bgfx.ResolveFlags.AutoGenMips => ResolveFlags.AutoMipMap,
                _ => throw new ArgumentOutOfRangeException()
            };
        }
    }

    private void Dispose(bool disposing)
    {
        if (disposing && _destroyParent)
        {
            RenderTarget.Dispose();
        }
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    ~TextureAttachment()
    {
        Dispose(false);
    }
}