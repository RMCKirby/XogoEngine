using OpenTK;
using OpenTK.Graphics.OpenGL4;
using System.Runtime.InteropServices;

namespace XogoEngine.OpenGL.Vertex
{
    [StructLayout(LayoutKind.Sequential)]
    public struct VertexPositionColourTexture : IVertexDeclarable
    {
        public VertexPositionColourTexture(Vector2 position, Vector4 colour, Vector2 textureCoordinate)
        {
            Position = position;
            Colour = colour;
            TextureCoordinate = textureCoordinate;
        }

        public Vector2 Position { get; }
        public Vector4 Colour { get; }
        public Vector2 TextureCoordinate { get; }

        public IVertexDeclaration Declaration
        {
            get
            {
                return declaration;
            }
        }

        private static VertexDeclaration declaration;

        static VertexPositionColourTexture()
        {
            var stride = Marshal.SizeOf(default(VertexPositionColourTexture));
            var elements = new VertexElement[]
            {
                new VertexElement(0, VertexElementUsage.Position, VertexAttribPointerType.Float, 2, false),
                new VertexElement(8, VertexElementUsage.Colour, VertexAttribPointerType.Float, 4, false),
                new VertexElement(24, VertexElementUsage.TexCoord, VertexAttribPointerType.Float, 2, false)
            };
            declaration = new VertexDeclaration(stride, elements);
        }
    }
}
