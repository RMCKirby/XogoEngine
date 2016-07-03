using OpenTK;
using OpenTK.Graphics.OpenGL4;
using System;
using System.Runtime.InteropServices;
using XogoEngine.OpenGL.Extensions;

namespace XogoEngine.OpenGL.Vertex
{
    [StructLayout(LayoutKind.Sequential)]
    public struct VertexPositionColourTexture : IVertexDeclarable, IEquatable<VertexPositionColourTexture>
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

        public IVertexDeclaration Declaration => declaration;

        public override bool Equals(object obj)
            => obj is VertexPositionColourTexture && Equals((VertexPositionColourTexture)obj);

        public bool Equals(VertexPositionColourTexture other)
            => Position == other.Position && Colour == other.Colour && TextureCoordinate == other.TextureCoordinate;

        public static bool operator ==(VertexPositionColourTexture left, VertexPositionColourTexture right)
            => left.Equals(right);

        public static bool operator !=(VertexPositionColourTexture left, VertexPositionColourTexture right)
            => !left.Equals(right);

        public override int GetHashCode()
        {
            unchecked
            {
                int hash = 17;
                hash = hash * 23 + Position.GetFixedHashCode();
                hash = hash * 23 + Colour.GetFixedHashCode();
                hash = hash * 23 + TextureCoordinate.GetFixedHashCode();
                return hash;
            }
        }

        public override string ToString()
        {
            return string.Format(
                "[VertexPositionColourTexture : Position={0}, Colour={1}, TextureCoordinate={2}]",
                Position,
                Colour,
                TextureCoordinate
            );
        }

        private static VertexDeclaration declaration;

        static VertexPositionColourTexture()
        {
            var stride = Marshal.SizeOf(typeof(VertexPositionColourTexture));
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
