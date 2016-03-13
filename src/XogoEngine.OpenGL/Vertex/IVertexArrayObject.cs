using XogoEngine.OpenGL.Shaders;

namespace XogoEngine.OpenGL.Vertex
{
    public interface IVertexArrayObject
    {
        void Bind();
        void SetUp(IShaderProgram shaderProgram, IVertexDeclaration vertexDeclaration);
    }
}
