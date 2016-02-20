using OpenTK.Graphics.OpenGL4;
using System;

namespace XogoEngine.OpenGL.Adapters
{
    public interface IBufferAdapter
    {
        int GenBuffer();

        void BindBuffer(BufferTarget target, int handle);

        void BufferData<T>(BufferTarget target, IntPtr size, T[] data, BufferUsageHint usageHint) where T : struct;

        void DeleteBuffer(int handle);
    }
}
