using Bgfx;

namespace BgfxRenderer.Enums;

public enum VertexAttributes
{
    Position = bgfx.Attrib.Position,
    Normal = bgfx.Attrib.Normal,
    Tangent = bgfx.Attrib.Tangent,
    BiTangent = bgfx.Attrib.Bitangent,
    Color0 = bgfx.Attrib.Color0,
    Color1 = bgfx.Attrib.Color1,
    Color2 = bgfx.Attrib.Color2,
    Color3 = bgfx.Attrib.Color3,
    Indices = bgfx.Attrib.Indices,
    Weight = bgfx.Attrib.Weight,
    TexCoord0 = bgfx.Attrib.TexCoord0,
    TexCoord1 = bgfx.Attrib.TexCoord1,
    TexCoord2 = bgfx.Attrib.TexCoord2,
    TexCoord3 = bgfx.Attrib.TexCoord3,
    TexCoord4 = bgfx.Attrib.TexCoord4,
    TexCoord5 = bgfx.Attrib.TexCoord5,
    TexCoord6 = bgfx.Attrib.TexCoord6,
    TexCoord7 = bgfx.Attrib.TexCoord7
}