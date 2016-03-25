using System;
using System.Diagnostics.CodeAnalysis;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL4;

namespace XogoEngine.OpenGL.Adapters
{
    [ExcludeFromCodeCoverageAttribute]
    public sealed class BufferAdapter : IBufferAdapter
    {
        public int GenBuffer()
        {
            GraphicsContext.Assert();
            int handle = GL.GenBuffer();
            return handle;
        }

        public void BindBuffer(BufferTarget target, int handle)
        {
            GraphicsContext.Assert();
            GL.BindBuffer(target, handle);
        }

        public void BufferData<T>(BufferTarget target, IntPtr size, T[] data, BufferUsageHint usageHint) where T : struct
        {
            GraphicsContext.Assert();
            GL.BufferData(target, size, data, usageHint);
        }

        public void BufferSubData<T>(BufferTarget target, IntPtr offset, IntPtr size, T[] data) where T : struct
        {
            GraphicsContext.Assert();
            GL.BufferSubData(target, offset, size, data);
        }

        public void DeleteBuffer(int handle)
        {
            GraphicsContext.Assert();
            GL.DeleteBuffer(handle);
        }
    }
}
