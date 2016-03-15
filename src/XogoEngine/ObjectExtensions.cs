using System;

namespace XogoEngine
{
    internal static class ObjectExtensions
    {
        public static void ThrowIfNull(this object obj, string paramName)
        {
            if (obj == null)
            {
                throw new ArgumentNullException(paramName);
            }
        }
    }
}
