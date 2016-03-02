using XogoEngine.OpenGL.Adapters;
using XogoEngine.OpenGL.Shaders;

namespace XogoEngine.OpenGL.Vertex
{
    public interface IVertexDeclaration
    {
        int Stride { get; }
        VertexElement[] Elements { get; }

        void Apply(IVertexArrayAdapter adapter, IShaderProgram shaderProgram);
    }
}
