using System.Runtime.InteropServices;

namespace XogoEngine.OpenGL.Vertex
{
    [StructLayout(LayoutKind.Sequential)]
    public struct VertexElement
    {
        public VertexElement(int offset)
        {
            Offset = offset;
        }

        public int Offset { get; }
    }
}
