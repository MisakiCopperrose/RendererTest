using Bgfx;

namespace BgfxRenderer.Enums;

public enum OcclusionResult
{
    Invisible = bgfx.OcclusionQueryResult.Invisible,
    Visible = bgfx.OcclusionQueryResult.Visible,
    None = bgfx.OcclusionQueryResult.NoResult
}