using OpenTK;
using OpenTK.Graphics.OpenGL4;
using System.Runtime.InteropServices;

namespace XogoEngine.OpenGL.Vertex
{
    [StructLayout(LayoutKind.Sequential)]
    public struct VertexPositionColour : IVertexDeclarable
    {
        public VertexPositionColour(Vector2 position, Vector4 colour)
        {
            Position = position;
            Colour = colour;
        }

        public Vector2 Position { get; }
        public Vector4 Colour { get; }

        public IVertexDeclaration Declaration
        {
            get
            {
                return declaration;
            }
        }

        private static VertexDeclaration declaration;

        static VertexPositionColour()
        {
            var vertexElements = new VertexElement[]
            {
                new VertexElement(0, VertexElementUsage.Position, VertexAttribPointerType.Float, 2, false),
                new VertexElement(8, VertexElementUsage.Colour, VertexAttribPointerType.Float, 4, false)
            };
            declaration = new VertexDeclaration(Marshal.SizeOf(default(VertexPositionColour)), vertexElements);
        }
    }
}
