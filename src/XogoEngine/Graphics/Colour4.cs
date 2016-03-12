using System;
using System.Runtime.InteropServices;
using XogoEngine.OpenGL.Utilities;

namespace XogoEngine.Graphics
{
    [StructLayout(LayoutKind.Sequential)]
    public struct Colour4 : IEquatable<Colour4>
    {
        public Colour4(float red, float green, float blue)
            : this(red, green, blue, 1)
        {
        }

        public Colour4(float red, float green, float blue, float alpha)
        {
            R = red;
            G = green;
            B = blue;
            A = alpha;
        }

        public float R { get; }
        public float G { get; }
        public float B { get; }
        public float A { get; }

        public override bool Equals(object obj)
        {
            if (!(obj is Colour4))
            {
                return false;
            }
            return Equals((Colour4)obj);
        }

        public bool Equals(Colour4 other)
        {
            return R == other.R &&
                   G == other.G &&
                   B == other.B &&
                   A == other.A;
        }

        public static bool operator ==(Colour4 left, Colour4 right) => left.Equals(right);
        public static bool operator !=(Colour4 left, Colour4 right) => !left.Equals(right);

        public override int GetHashCode()
            => HashCodeGenerator.Initialise().Hash(R).Hash(G).Hash(B).Hash(A).Value;

        public override string ToString() => $"[Colour4 : R={R}, G={G}, B={B}, A={A}]";
    }
}
