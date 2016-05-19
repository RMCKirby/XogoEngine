using System;
using System.Linq;
using OpenTK.Graphics.OpenGL4;
using XogoEngine.OpenGL.Adapters;

namespace XogoEngine.Graphics
{
    public sealed class Texture : ITexture
    {
        private int handle;
        private int width;
        private int height;
        private readonly ITextureAdapter adapter;

        private byte[] data;
        private bool isDisposed = false;

        internal Texture(ITextureAdapter adapter, int glHandle, int width, int height, byte[] data)
        {
            if (adapter == null)
            {
                throw new ArgumentNullException(nameof(adapter));
            }
            this.adapter = adapter;
            this.handle = glHandle;
            this.width = width;
            this.height = height;
            this.data = data;
        }

        public int Handle { get { return handle; } }
        public int Width { get { return width; } }
        public int Height { get { return height; } }
        public bool IsDisposed { get { return isDisposed; } }

        internal TextureTarget Target { get { return TextureTarget.Texture2D; } }
        internal byte[] Data
        {
            get { return data.ToArray(); }
        }

        public void Bind()
        {
            adapter.Bind(Target, handle);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~Texture()
        {
            Dispose(false);
        }

        private void Dispose(bool managed)
        {
            if (isDisposed)
            {
                return;
            }
            if (managed)
            {
                adapter.DeleteTexture(handle);
            }
            else
            {
                OpenTK.Graphics.GraphicsContext.Assert();
                GL.DeleteTexture(handle);
            }
            isDisposed = true;
        }
    }
}
