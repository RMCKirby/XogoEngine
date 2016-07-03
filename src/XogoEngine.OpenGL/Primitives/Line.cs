using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using XogoEngine.OpenGL.Utilities;
using XogoEngine.OpenGL.Vertex;

namespace XogoEngine.OpenGL.Primitives
{
    [StructLayout(LayoutKind.Sequential)]
    public struct Line : IPrimitiveShape<VertexPositionColour>, IEquatable<Line>
    {
        public Line(VertexPositionColour start, VertexPositionColour end)
        {
            Start = start;
            End = end;
        }

        public VertexPositionColour Start { get; }
        public VertexPositionColour End { get; }

        public int Stride => Marshal.SizeOf<Line>();

        public IEnumerable<VertexPositionColour> Vertices
            => new List<VertexPositionColour>() { Start, End };

        public override bool Equals(object obj)
        {
            if (!(obj is Line))
            {
                return false;
            }
            return Equals((Line)obj);
        }

        public bool Equals(Line other) => Start == other.Start && End == other.End;

        public override int GetHashCode() => HashCodeGenerator.Initialise().Hash(Start).Hash(End).Value;
    }
}
