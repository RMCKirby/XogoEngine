using System;

namespace XogoEngine.Graphics
{
    public interface ITexture
    {
        int Handle { get; }
        int Width { get; }
        int Height { get; }
        bool IsDisposed { get; }
    }
}
