using OpenTK.Graphics.OpenGL4;
using System.Runtime.InteropServices;

namespace XogoEngine.OpenGL.Vertex
{
    [StructLayout(LayoutKind.Sequential)]
    public struct VertexElement
    {
        public VertexElement(
            int offset,
            string usage,
            VertexAttribPointerType type,
            int numberOfComponents,
            bool normalised)
        {
            Offset = offset;
            Usage = usage;
            PointerType = type;
            NumberOfComponents = numberOfComponents;
            Normalised = normalised;
        }

        public int Offset { get; }
        public string Usage { get; }
        public VertexAttribPointerType PointerType { get; }
        public int NumberOfComponents { get; }
        public bool Normalised { get; }

    }
}
