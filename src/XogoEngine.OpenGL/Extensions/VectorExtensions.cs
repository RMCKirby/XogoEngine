using OpenTK;

namespace XogoEngine.OpenGL.Extensions
{
    /* FIX for OpenTK Vector types GetHashCode implementation
     * see: https://github.com/opentk/opentk/issues/355 */
    public static class VectorExtensions
    {
        private const int HashCodeMultiplier = 37;
        private const int HashCodeInitialiser = 17;

        public static int GetFixedHashCode(this Vector2 vector)
        {
            unchecked
            {
                int hash = HashCodeInitialiser;
                hash = hash * HashCodeMultiplier + vector.X.GetHashCode();
                hash = hash * HashCodeMultiplier + vector.Y.GetHashCode();
                return hash;
            }
        }

        public static int GetFixedHashCode(this Vector3 vector)
        {
            unchecked
            {
                int hash = HashCodeInitialiser;
                hash = hash * HashCodeMultiplier + vector.X.GetHashCode();
                hash = hash * HashCodeMultiplier + vector.Y.GetHashCode();
                hash = hash * HashCodeMultiplier + vector.Z.GetHashCode();
                return hash;
            }
        }

        public static int GetFixedHashCode(this Vector4 vector)
        {
            unchecked
            {
                int hash = HashCodeInitialiser;
                hash = hash * HashCodeMultiplier + vector.X.GetHashCode();
                hash = hash * HashCodeMultiplier + vector.Y.GetHashCode();
                hash = hash * HashCodeMultiplier + vector.Z.GetHashCode();
                hash = hash * HashCodeMultiplier + vector.W.GetHashCode();
                return hash;
            }
        }
    }
}
