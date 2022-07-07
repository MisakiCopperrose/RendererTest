using Bgfx;
using BgfxRenderer.Enums;

namespace BgfxRenderer.Occlusion;

public static unsafe class OcclusionQueryUtils
{
    public static OcclusionResult GetResult(OcclusionQuery occlusionQuery, out int numberOfPassedPixels)
    {
        numberOfPassedPixels = default;

        var intBuffer = 0;
        var resultBuffer = (OcclusionResult)bgfx.get_result(occlusionQuery.Handle, &intBuffer);

        numberOfPassedPixels = intBuffer;

        return resultBuffer;
    }

    public static OcclusionResult GetResult(OcclusionQuery occlusionQuery)
    {
        return (OcclusionResult)bgfx.get_result(occlusionQuery.Handle, null);
    }
}