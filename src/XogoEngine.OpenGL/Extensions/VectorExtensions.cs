using OpenTK;
using XogoEngine.OpenGL.Utilities;

namespace XogoEngine.OpenGL.Extensions
{
    /* FIX for OpenTK Vector types GetHashCode implementation
     * see: https://github.com/opentk/opentk/issues/355 */
    public static class VectorExtensions
    {
        public static int GetFixedHashCode(this Vector2 vector)
            => HashCodeGenerator.Initialise().Hash(vector.X).Hash(vector.Y).Value;

        public static int GetFixedHashCode(this Vector3 vector)
            => HashCodeGenerator.Initialise().Hash(vector.X).Hash(vector.Y).Hash(vector.Z).Value;

        public static int GetFixedHashCode(this Vector4 vector)
            => HashCodeGenerator.Initialise().Hash(vector.X).Hash(vector.Y).Hash(vector.Z).Hash(vector.W).Value;
    }
}
