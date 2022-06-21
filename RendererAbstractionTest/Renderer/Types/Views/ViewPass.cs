using System.Drawing;
using System.Numerics;
using Bgfx;
using RendererAbstractionTest.Renderer.Enums;
using RendererAbstractionTest.Renderer.Types.Buffers.Frame;
using RendererAbstractionTest.Renderer.Utils;

namespace RendererAbstractionTest.Renderer.Types.Views;

public unsafe class ViewPass : IDisposable
{
    private string _name;
    private Rectangle _viewRectangle;
    private Rectangle _scissorRectangle;
    private Color _clearColor;
    private float _clearDepth;
    private byte _clearStencil;
    private DrawCallOrder _drawCallOrder;
    private FrameBuffer _viewPassFrameBuffer;
    private Matrix4x4 _viewMatrix;
    private Matrix4x4 _projectionMatrix;

    public ViewPass()
    {
        Name = Id.ToString();
        ClearColor = Color.Purple;
        ClearDepth = 1f;
        ClearStencil = 0;
    }
    
    public ViewPass(string name)
    {
        Name = name;
        ClearColor = Color.Purple;
        ClearDepth = 1f;
        ClearStencil = 0;
    }
    
    public ViewPass(string name, Color clearColor)
    {
        Name = name;
        ClearColor = clearColor;
        ClearDepth = 1f;
        ClearStencil = 0;
    }
    
    public ViewPass(Color clearColor)
    {
        Name = Id.ToString();
        ClearColor = clearColor;
        ClearDepth = 1f;
        ClearStencil = 0;
    }

    public ushort Id { get; init; }

    public string Name
    {
        get => _name;
        set
        {
            bgfx.set_view_name(Id, value);

            _name = value;
        }
    }

    public Matrix4x4 ViewMatrix
    {
        get => _viewMatrix;
        set
        {
            var view = value;
            var projection = _projectionMatrix;
            
            bgfx.set_view_transform(Id, &view, &projection);
            
            _viewMatrix = value;
        }
    }

    public Matrix4x4 ProjectionMatrix
    {
        get => _projectionMatrix;
        set
        {
            var view = _viewMatrix;
            var projection = value;
            
            bgfx.set_view_transform(Id, &view, &projection);
            
            _projectionMatrix = value;
        }
    }

    public Rectangle ViewRectangle
    {
        get => _viewRectangle;
        set
        {
            bgfx.set_view_rect(Id, (ushort)value.X, (ushort)value.Y, (ushort)value.Width, (ushort)value.Height);

            _viewRectangle = value;
        }
    }

    public void SetViewRectangleBackBufferRatio(Point origin, BackBufferRatios bufferRatio)
    {
        throw new NotImplementedException();
        
        // bgfx.set_view_rect_ratio(_id, (ushort)origin.X, (ushort)origin.Y, (bgfx.BackbufferRatio)bufferRatio);
        //
        // _viewRectangle = new Rectangle
        // {
        //     X = origin.X,
        //     Y = origin.Y,
        //     // Height = backbuffer / ratio
        //     // Width = backbuffer / ratio
        // };
    }

    public Rectangle ScissorRectangle
    {
        get => _scissorRectangle;
        set
        {
            bgfx.set_view_scissor(Id, (ushort)value.X, (ushort)value.Y, (ushort)value.Width, (ushort)value.Height);

            _scissorRectangle = value;
        }
    }

    public Color ClearColor
    {
        get => _clearColor;
        set
        {
            bgfx.set_view_clear(Id, (ushort)(bgfx.ClearFlags.Color | bgfx.ClearFlags.Depth), value.ColorToUintRgba() ,_clearDepth, _clearStencil);

            _clearColor = value;
        }
    }

    public void SetFramebufferClearColors()
    {
        //bgfx.set_view_clear_mrt(_id, (ushort)bgfx.ClearFlags.None, _clearDepth, _clearStencil,);

        throw new NotImplementedException();
    }

    public float ClearDepth
    {
        get => _clearDepth;
        set
        {
            bgfx.set_view_clear(Id, (ushort)(bgfx.ClearFlags.Color | bgfx.ClearFlags.Depth), _clearColor.ColorToUintRgba(), value, _clearStencil);
            
            _clearDepth = value;
        }
    }

    public byte ClearStencil
    {
        get => _clearStencil;
        set
        {
            bgfx.set_view_clear(Id, (ushort)(bgfx.ClearFlags.Color | bgfx.ClearFlags.Depth), _clearColor.ColorToUintRgba(), _clearDepth, value);
            
            _clearStencil = value;
        }
    }

    public DrawCallOrder DrawCallOrder
    {
        get => _drawCallOrder;
        set
        {
            bgfx.set_view_mode(Id, (bgfx.ViewMode)value);
            
            _drawCallOrder = value;
        }
    }

    // TODO: implement framebuffer
    public FrameBuffer ViewPassFrameBuffer
    {
        get => _viewPassFrameBuffer;
        set => _viewPassFrameBuffer = value;
    }

    public void Reset()
    {
        bgfx.reset_view(Id);
    }

    private void ReleaseUnmanagedResources()
    {
        Reset();
    }

    public void Dispose()
    {
        ReleaseUnmanagedResources();
        GC.SuppressFinalize(this);
    }

    ~ViewPass()
    {
        ReleaseUnmanagedResources();
    }
}