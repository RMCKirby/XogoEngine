using OpenTK;

namespace XogoEngine.OpenGL.Extensions
{
    public static class VectorExtensions
    {
        public static int GetFixedHashCode(this Vector2 vector)
        {
            unchecked
            {
                int hash = 17;
                hash = hash * 23 + vector.X.GetHashCode();
                hash = hash * 23 + vector.Y.GetHashCode();
                return hash;
            }
        }
    }
}
