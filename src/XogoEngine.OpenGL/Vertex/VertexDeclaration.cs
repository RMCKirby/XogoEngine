using System;
using XogoEngine.OpenGL.Adapters;
using XogoEngine.OpenGL.Shaders;

namespace XogoEngine.OpenGL.Vertex
{
    public sealed class VertexDeclaration
    {
        public VertexDeclaration(int stride, VertexElement[] elements)
        {
            Stride = stride;
            Elements = elements;
        }

        public int Stride { get; }
        public VertexElement[] Elements { get; }

        public void Apply(IVertexArrayAdapter adapter, IShaderProgram shaderProgram)
        {
            if (adapter == null)
            {
                throw new ArgumentNullException(nameof(adapter));
            }
            if (shaderProgram == null)
            {
                throw new ArgumentNullException(nameof(shaderProgram));
            }
        }
    }
}
