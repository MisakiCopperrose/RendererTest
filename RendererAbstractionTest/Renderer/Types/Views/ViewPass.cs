using System.Drawing;
using System.Numerics;
using Bgfx;
using RendererAbstractionTest.Renderer.Types.Buffers.Frame;

namespace RendererAbstractionTest.Renderer.Types.Views;

public class ViewPass : IDisposable
{
    private readonly ushort _id;
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

    public ushort Id
    {
        get
        {
            return _id;
        }
        init { _id = value; }
    }

    public string Name
    {
        get { return _name; }
        set { _name = value; }
    }

    public Matrix4x4 ViewMatrix
    {
        get => _viewMatrix;
        set => _viewMatrix = value;
    }

    public Matrix4x4 ProjectionMatrix
    {
        get => _projectionMatrix;
        set => _projectionMatrix = value;
    }

    public Rectangle ViewRectangle
    {
        get { return _viewRectangle; }
        set { _viewRectangle = value; }
    }

    public Rectangle ScissorRectangle
    {
        get { return _scissorRectangle; }
        set { _scissorRectangle = value; }
    }

    public Color ClearColor
    {
        get { return _clearColor; }
        set { _clearColor = value; }
    }

    public float ClearDepth
    {
        get { return _clearDepth; }
        set { _clearDepth = value; }
    }

    public byte ClearStencil
    {
        get { return _clearStencil; }
        set { _clearStencil = value; }
    }

    public DrawCallOrder DrawCallOrder
    {
        get => _drawCallOrder;
        set => _drawCallOrder = value;
    }

    public FrameBuffer ViewPassFrameBuffer
    {
        get => _viewPassFrameBuffer;
        set => _viewPassFrameBuffer = value;
    }

    public void Reset()
    {
        bgfx.reset_view(_id);
    }

    private void ReleaseUnmanagedResources()
    {
        bgfx.reset_view(_id);
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