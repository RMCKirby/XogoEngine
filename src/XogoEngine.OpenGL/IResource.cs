using System;

namespace XogoEngine.OpenGL
{
    public interface IResource<out T> : IDisposable where T : struct
    {
        T Handle { get; }
        bool IsDisposed { get; }
    }
}
