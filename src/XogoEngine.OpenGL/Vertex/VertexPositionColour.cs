using OpenTK;
using OpenTK.Graphics.OpenGL4;
using System;
using System.Runtime.InteropServices;
using XogoEngine.OpenGL.Extensions;

namespace XogoEngine.OpenGL.Vertex
{
    [StructLayout(LayoutKind.Sequential)]
    public struct VertexPositionColour : IVertexDeclarable, IEquatable<VertexPositionColour>
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

        public override bool Equals(object obj)
        {
            if (!(obj is VertexPositionColour))
            {
                return false;
            }
            return Equals((VertexPositionColour)obj);
        }

        public bool Equals(VertexPositionColour other)
        {
            return Position == other.Position
                && Colour == other.Colour;
        }

        public static bool operator ==(VertexPositionColour left, VertexPositionColour right) => left.Equals(right);
        public static bool operator !=(VertexPositionColour left, VertexPositionColour right) => !left.Equals(right);

        public override int GetHashCode()
        {
            unchecked
            {
                int hash = 17;
                hash = hash * 23 + Position.GetFixedHashCode();
                hash = hash * 23 + Colour.GetFixedHashCode();
                return hash;
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
