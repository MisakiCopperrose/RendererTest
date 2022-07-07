using System.Collections;
using Bgfx;

namespace BgfxRenderer.Views;

// TODO: come up with better system to regulate view pass id's
public struct ViewPassTable : IEnumerable<ViewPass>
{
    private IList<ViewPass> _viewPasses;

    public ViewPass CreateNewPass()
    {
        var id = _viewPasses.Count - 1;
        var pass = new ViewPass();
        
        _viewPasses.Add(pass);

        return pass;
    }

    public bool TryRemovePass(ushort id)
    {
        
    }

    public bool TryRemovePass(string name)
    {
        throw new NotImplementedException();
    }

    public void Reorder()
    {
        
    }

    public void Clear()
    {
        foreach (var viewPass in _viewPasses)
        {
            bgfx.reset_view(viewPass.Id);
        }
    }

    public IEnumerator<ViewPass> GetEnumerator()
    {
        return _viewPasses.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}