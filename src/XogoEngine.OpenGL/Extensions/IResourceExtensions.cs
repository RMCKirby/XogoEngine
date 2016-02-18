namespace XogoEngine.OpenGL.Extensions
{
    public static class IResourceExtensions
    {
        public static void ThrowIfDisposed<T>(this IResource<T> instance) where T : struct
        {
            if (instance?.IsDisposed ?? false)
            {
                throw new System.ObjectDisposedException(instance.GetType().FullName);
            }
        }
    }
}
