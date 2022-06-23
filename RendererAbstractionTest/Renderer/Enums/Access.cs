using Bgfx;

namespace RendererAbstractionTest.Renderer.Enums;

public enum Access
{
    Read = bgfx.Access.Read, 
    Write = bgfx.Access.Write, 
    ReadWrite = bgfx.Access.ReadWrite
}