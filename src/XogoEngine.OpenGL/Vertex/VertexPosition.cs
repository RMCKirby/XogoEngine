using OpenTK;
using OpenTK.Graphics.OpenGL4;
using System.Runtime.InteropServices;

namespace XogoEngine.OpenGL.Vertex
{
    [StructLayout(LayoutKind.Sequential)]
    public struct VertexPosition : IVertexDeclarable
    {
        public VertexPosition(Vector2 position)
        {
            Position = position;
        }

        public Vector2 Position { get; }

        public IVertexDeclaration Declaration
        {
            get
            {
                return declaration;
            }
        }

        private static VertexDeclaration declaration;

        static VertexPosition()
        {
            var vertexElements = new VertexElement[]
            {
                new VertexElement(0, VertexElementUsage.Position, VertexAttribPointerType.Float, 2, false)
            };
            declaration = new VertexDeclaration(Marshal.SizeOf(default(VertexPosition)), vertexElements);
        }
    }
}
