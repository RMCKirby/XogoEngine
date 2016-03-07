using System;
using XogoEngine.OpenGL.Adapters;

namespace XogoEngine.Graphics
{
    public sealed class Texture : IDisposable
    {
        private int handle;
        private int width;
        private int height;
        private readonly ITextureAdapter adapter;

        private bool isDisposed = false;

        internal Texture(ITextureAdapter adapter, int glHandle, int width, int height)
        {
            if (adapter == null)
            {
                throw new ArgumentNullException(nameof(adapter));
            }
            this.adapter = adapter;
            this.handle = glHandle;
            this.width = width;
            this.height = height;
        }

        public int Handle { get { return handle; } }
        public int Width { get { return width; } }
        public int Height { get { return height; } }
        public bool IsDisposed { get { return isDisposed; } }

        public void Dispose()
        {
            if (isDisposed)
            {
                return;
            }
            adapter.DeleteTexture(handle);
            isDisposed = true;
            GC.SuppressFinalize(this);
        }
    }
}
