using OpenTK;
using OpenTK.Graphics.OpenGL4;
using System.Runtime.InteropServices;

namespace XogoEngine.OpenGL.Vertex
{
    [StructLayout(LayoutKind.Sequential)]
    public struct VertexPositionColourTexture
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
    }
}
