namespace XogoEngine.OpenGL.Extensions
{
    public static class IResourceExtensions
    {
        internal static void ThrowIfDisposed<T>(this IResource<T> instance) where T : struct
        {
            if (instance.IsDisposed)
            {
                throw new System.ObjectDisposedException(instance.GetType().FullName);
            }
        }
    }
}
