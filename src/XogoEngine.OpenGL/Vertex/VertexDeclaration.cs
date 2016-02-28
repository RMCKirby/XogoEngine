using System;
using XogoEngine.OpenGL.Adapters;
using XogoEngine.OpenGL.Extensions;
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
            shaderProgram.ThrowIfDisposed();

            foreach (var element in Elements)
            {
                /* Let ShaderAttributeNotFoundException bubble up
                 * If we can't find an attribute in the shaderProgram that matches
                 * the usages defined in the given vertex declaration. */
                int location = shaderProgram.GetAttributeLocation(element.Usage);
                adapter.EnableVertexAttribArray(location);
            }
        }
    }
}
