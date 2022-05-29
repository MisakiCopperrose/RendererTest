using System.Numerics;
using Bgfx;
using RendererAbstractionTest.Renderer.Structs;
using RendererAbstractionTest.Renderer.Types.Buffers;

namespace RendererAbstractionTest.Renderer.Types;

public unsafe class RenderPass
{
    private ViewRectangle<ushort> _viewRectangle;
    private ViewRectangle<ushort> _scissorRectangle;
    private Framebuffer _framebuffer;
    private Matrix4x4 _viewMatrix;
    private Matrix4x4 _projectionMatrix;
    
    private string _name = string.Empty;

    public RenderPass(ushort id, string name, ViewRectangle<ushort> viewRectangle, ViewRectangle<ushort> scissorRectangle)
    {
        Id = id;
        Name = name;
        ViewRectangle = viewRectangle;
        ScissorRectangle = scissorRectangle;
    }

    public ushort Id { get; }

    public Framebuffer Framebuffer
    {
        get => _framebuffer;
        set
        {
            var framebuffer = new bgfx.FrameBufferHandle
            {
                idx = value.Handle
            };

            bgfx.set_view_frame_buffer(Id, framebuffer);
            
            _framebuffer = value;
        }
    }

    public Matrix4x4 ViewMatrix
    {
        get => _viewMatrix;
        set
        {
            var projectionMatrix = _projectionMatrix;
            
            bgfx.set_view_transform(Id, &value, &projectionMatrix);
            
            _viewMatrix = value;
        }
    }
    
    public Matrix4x4 ProjectionMatrix
    {
        get => _projectionMatrix;
        set
        {
            var viewMatrix = _viewMatrix;
            
            bgfx.set_view_transform(Id, &viewMatrix, &value);
            
            _projectionMatrix = value;
        }
    }

    public string Name
    {
        get => _name;
        set
        {
            bgfx.set_view_name(Id, value);

            _name = value;
        }
    }

    public ViewRectangle<ushort> ViewRectangle
    {
        get => _viewRectangle;
        set
        {
            bgfx.set_view_rect(
                Id,
                value.X,
                value.Y,
                value.Width,
                value.Height
            );

            _viewRectangle = value;
        }
    }

    public ViewRectangle<ushort> ScissorRectangle
    {
        get => _scissorRectangle;
        set
        {
            bgfx.set_view_scissor(
                Id,
                value.X,
                value.Y,
                value.Width,
                value.Height
            );

            _scissorRectangle = value;
        }
    }

    public void Reset()
    {
        bgfx.reset_view(Id);
    }
}