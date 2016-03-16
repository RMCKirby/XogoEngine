using XogoEngine.OpenGL.Shaders;

namespace XogoEngine.OpenGL.Vertex
{
    public interface IVertexArrayObject : IResource<int>
    {
        void Bind();
        void SetUp(IShaderProgram shaderProgram, IVertexDeclaration vertexDeclaration);
    }
}
