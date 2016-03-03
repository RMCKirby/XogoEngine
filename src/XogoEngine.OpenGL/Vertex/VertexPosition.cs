using OpenTK;
using OpenTK.Graphics.OpenGL4;
using System;
using System.Runtime.InteropServices;
using XogoEngine.OpenGL.Extensions;

namespace XogoEngine.OpenGL.Vertex
{
    [StructLayout(LayoutKind.Sequential)]
    public struct VertexPosition : IVertexDeclarable, IEquatable<VertexPosition>
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

        public override bool Equals(object obj)
        {
            if (!(obj is VertexPosition))
            {
                return false;
            }
            return Equals((VertexPosition)obj);
        }

        public bool Equals(VertexPosition other) => Position == other.Position;

        public static bool operator ==(VertexPosition left, VertexPosition right) => left.Equals(right);
        public static bool operator !=(VertexPosition left, VertexPosition right) => !left.Equals(right);

        public override int GetHashCode()
        {
            unchecked
            {
                int hash = 17;
                hash = hash * 23 + Position.GetFixedHashCode();
                return hash;
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
