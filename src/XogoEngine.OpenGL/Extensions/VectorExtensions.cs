using OpenTK;

namespace XogoEngine.OpenGL.Extensions
{
    /* FIX for OpenTK Vector types GetHashCode implementation
     * see: https://github.com/opentk/opentk/issues/355 */
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

        public static int GetFixedHashCode(this Vector3 vector)
        {
            unchecked
            {
                int hash = 17;
                hash = hash * 23 + vector.X.GetHashCode();
                hash = hash * 23 + vector.Y.GetHashCode();
                hash = hash * 23 + vector.Z.GetHashCode();
                return hash;
            }
        }

        public static int GetFixedHashCode(this Vector4 vector)
        {
            unchecked
            {
                int hash = 17;
                hash = hash * 23 + vector.X.GetHashCode();
                hash = hash * 23 + vector.Y.GetHashCode();
                hash = hash * 23 + vector.Z.GetHashCode();
                hash = hash * 23 + vector.W.GetHashCode();
                return hash;
            }
        }
    }
}
