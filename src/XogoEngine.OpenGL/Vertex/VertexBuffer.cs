using OpenTK.Graphics.OpenGL4;
using System;
using XogoEngine.OpenGL.Adapters;
using XogoEngine.OpenGL.Extensions;

namespace XogoEngine.OpenGL.Vertex
{
    public sealed class VertexBuffer<TVertex> :
        IVertexBuffer<TVertex>
        where TVertex : struct, IVertexDeclarable
    {
        private int handle;
        private readonly IBufferAdapter adapter;

        private bool isDisposed = false;

        public VertexBuffer(IBufferAdapter adapter)
        {
            this.adapter = adapter;
            this.handle = adapter.GenBuffer();
            this.VertexDeclaration = default(TVertex).Declaration;
        }

        public int Handle => handle;
        public BufferTarget Target => BufferTarget.ArrayBuffer;
        public IntPtr Size { get; private set; }
        public bool IsDisposed => isDisposed;

        public IVertexDeclaration VertexDeclaration { get; }

        public void Bind()
        {
            this.ThrowIfDisposed();
            adapter.BindBuffer(Target, handle);
        }

        public void Fill(IntPtr size, TVertex[] data, BufferUsageHint usageHint)
        {
            this.ThrowIfDisposed();
            if (size == IntPtr.Zero || size.ToInt32() < 0)
            {
                throw new ArgumentOutOfRangeException(
                    $"The size allocated to the buffer must be greater than zero, got : {size}"
                );
            }
            Size = size;
            adapter.BufferData(Target, size, data, usageHint);
        }

        public void FillPartial(IntPtr offset, IntPtr size, TVertex[] data)
        {
            this.ThrowIfDisposed();
            if (Size == IntPtr.Zero)
            {
                throw new UnallocatedBufferSizeException(
                    "The size of the buffer has not yet been allocated. Have you called Fill?"
                );
            }
            ValidateNonNegativePtr(offset, nameof(offset));
            ValidateNonNegativePtr(size, nameof(size));
            int bufferSize = Size.ToInt32();
            if (offset.ToInt32() + size.ToInt32() > bufferSize)
            {
                throw new ArgumentOutOfRangeException(
                    $"The given offset : {offset} and size : {size} were outside the buffer's size"
                );
            }
            adapter.BufferSubData(Target, offset, size, data);
        }

        private void ValidateNonNegativePtr(IntPtr pointer, string name)
        {
            int size = pointer.ToInt32();
            if (size < 0)
            {
                throw new ArgumentOutOfRangeException(
                    $"{name} cannot be negative, got : {size}"
                );
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~VertexBuffer()
        {
            Dispose(false);
        }

        private void Dispose(bool manual)
        {
            if (isDisposed)
            {
                return;
            }
            if (!manual && OpenTK.Graphics.GraphicsContext.CurrentContext != null)
            {
                GL.DeleteBuffer(handle);
            }
            else
            {
                adapter.DeleteBuffer(handle);
            }
            isDisposed = true;
        }
    }
}
