using System;
using XogoEngine.OpenGL.Adapters;
using XogoEngine.OpenGL.Extensions;
using XogoEngine.OpenGL.Shaders;

namespace XogoEngine.OpenGL.Vertex
{
    public sealed class VertexArrayObject : IVertexArrayObject
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

        public int Handle => handle;
        public bool IsDisposed => isDisposed;

        public void Bind()
        {
            this.ThrowIfDisposed();
            adapter.BindVertexArray(handle);
        }

        public void SetUp(IShaderProgram shaderProgram, IVertexDeclaration vertexDeclaration)
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
            vertexDeclaration.Apply(adapter, shaderProgram);
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
