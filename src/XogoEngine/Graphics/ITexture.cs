using System;

namespace XogoEngine.Graphics
{
    public interface ITexture : IDisposable
    {
        int Handle { get; }
        int Width { get; }
        int Height { get; }
        bool IsDisposed { get; }
    }
}
