using System;
using OpenTK.Graphics.OpenGL4;

namespace XogoEngine.OpenGL.Adapters
{
    public interface ITextureAdapter
    {
        int GenTexture();
        void Bind(TextureTarget target, int handle);
        void DeleteTexture(int handle);

        void TexImage2D(
            TextureTarget target,
            int level,
            PixelInternalFormat internalFormat,
            int width,
            int height,
            int border,
            PixelFormat format,
            PixelType type,
            IntPtr pixels);
    }
}
