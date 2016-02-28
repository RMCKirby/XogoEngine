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
    }
}
