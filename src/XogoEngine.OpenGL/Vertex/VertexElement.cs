using OpenTK.Graphics.OpenGL4;
using System.Runtime.InteropServices;

namespace XogoEngine.OpenGL.Vertex
{
    [StructLayout(LayoutKind.Sequential)]
    public struct VertexElement
    {
        public VertexElement(int offset, string usage, VertexAttribPointerType type)
        {
            Offset = offset;
            Usage = usage;
            PointerType = type;
        }

        public int Offset { get; }
        public string Usage { get; }
        public VertexAttribPointerType PointerType { get; }

    }
}
