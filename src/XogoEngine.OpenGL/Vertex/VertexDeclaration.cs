namespace XogoEngine.OpenGL.Vertex
{
    public sealed class VertexDeclaration
    {
        public VertexDeclaration(int stride)
        {
            Stride = stride;
        }

        public int Stride { get; }
    }
}
