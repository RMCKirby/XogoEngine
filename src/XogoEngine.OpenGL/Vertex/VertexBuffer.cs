using OpenTK.Graphics.OpenGL4;
using System;
using XogoEngine.OpenGL.Adapters;
using XogoEngine.OpenGL.Extensions;

namespace XogoEngine.OpenGL.Vertex
{
    public sealed class VertexBuffer<T> : IResource<int> where T : struct
    {
        private int handle;
        private readonly IBufferAdapter adapter;

        private bool isDisposed = false;

        public VertexBuffer(IBufferAdapter adapter)
        {
            this.adapter = adapter;
            this.handle = adapter.GenBuffer();
        }

        public int Handle { get { return handle; } }
        public BufferTarget Target { get { return BufferTarget.ArrayBuffer; } }
        public IntPtr Size { get; private set; }
        public bool IsDisposed { get { return isDisposed; } }

        public void Bind()
        {
            this.ThrowIfDisposed();
            adapter.BindBuffer(Target, handle);
        }

        public void Fill(IntPtr size, T[] data, BufferUsageHint usageHint)
        {
            this.ThrowIfDisposed();
            if (size == IntPtr.Zero || size.ToInt32() < 0)
            {
                throw new ArgumentException(
                    $"The size allocated to the buffer must be greater than zero, got : {size}"
                );
            }
            Size = size;
            adapter.BufferData(Target, size, data, usageHint);
        }

        public void FillPartial(IntPtr offset, IntPtr size, T[] data)
        {
            this.ThrowIfDisposed();
            if (Size == IntPtr.Zero)
            {
                throw new UnallocatedBufferSizeException(
                    "The size of the buffer has not yet been allocated. Have you called Fill?"
                );
            }
        }

        public void Dispose()
        {
            if (isDisposed)
            {
                return;
            }
            adapter.DeleteBuffer(handle);
            isDisposed = true;
            GC.SuppressFinalize(this);
        }
    }
}
