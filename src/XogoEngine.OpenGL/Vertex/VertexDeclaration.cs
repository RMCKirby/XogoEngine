using System;
using System.Linq;
using XogoEngine.OpenGL.Adapters;
using XogoEngine.OpenGL.Extensions;
using XogoEngine.OpenGL.Shaders;

namespace XogoEngine.OpenGL.Vertex
{
    public sealed class VertexDeclaration : IVertexDeclaration
    {
        private VertexElement[] elements;

        public VertexDeclaration(int stride, VertexElement[] elements)
        {
            Stride = stride;
            this.elements = elements;
        }

        public int Stride { get; }
        public VertexElement[] Elements
        {
            get { return elements.ToArray(); }
        }

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
                //TODO: cache these values
                adapter.VertexAttribPointer(
                    location,
                    element.NumberOfComponents,
                    element.PointerType,
                    element.Normalised,
                    Stride,
                    element.Offset
                );
            }
        }
    }
}
