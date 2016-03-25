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
    }
}
