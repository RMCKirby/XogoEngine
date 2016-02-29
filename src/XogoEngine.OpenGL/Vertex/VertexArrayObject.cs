using System;
using XogoEngine.OpenGL.Adapters;
using XogoEngine.OpenGL.Extensions;
using XogoEngine.OpenGL.Shaders;

namespace XogoEngine.OpenGL.Vertex
{
    public sealed class VertexArrayObject : IResource<int>
    {
        private int handle;
        private readonly IVertexArrayAdapter adapter;
        private bool isDisposed = false;

        public VertexArrayObject(IVertexArrayAdapter adapter)
        {
            if (adapter == null)
            {
                throw new ArgumentNullException(nameof(adapter));
            }
            this.adapter = adapter;
            handle = adapter.GenVertexArray();
        }

        public int Handle { get { return handle; } }
        public bool IsDisposed { get { return isDisposed; } }

        public void Bind()
        {
            this.ThrowIfDisposed();
            adapter.BindVertexArray(handle);
        }

        public void SetUp(IShaderProgram shaderProgram, VertexDeclaration vertexDeclaration)
        {
            this.ThrowIfDisposed();
            if (shaderProgram == null)
            {
                throw new ArgumentNullException(nameof(shaderProgram));
            }
            if (vertexDeclaration == null)
            {
                throw new ArgumentNullException(nameof(vertexDeclaration));
            }
        }

        public void Dispose()
        {
            if (isDisposed)
            {
                return;
            }
            adapter.DeleteVertexArray(handle);
            isDisposed = true;
            GC.SuppressFinalize(this);
        }
    }
}
