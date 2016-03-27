using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL4;
using System;
using System.Diagnostics.CodeAnalysis;

namespace XogoEngine.OpenGL.Adapters
{
    [ExcludeFromCodeCoverage]
    public sealed class TextureAdapter : ITextureAdapter
    {
        public int GenTexture()
        {
            GraphicsContext.Assert();
            int texture = GL.GenTexture();
            OpenGlErrorHelper.CheckGlError();
            return texture;
        }

        public void Bind(TextureTarget target, int handle)
        {
            GraphicsContext.Assert();
            GL.BindTexture(target, handle);
            OpenGlErrorHelper.CheckGlError();
        }

        public void DeleteTexture(int handle)
        {
            GraphicsContext.Assert();
            GL.DeleteTexture(handle);
            OpenGlErrorHelper.CheckGlError();
        }

        public void TexImage2D(
            TextureTarget target,
            int level,
            PixelInternalFormat internalFormat,
            int width,
            int height,
            int border,
            PixelFormat format,
            PixelType type,
            IntPtr pixels)
        {
            GraphicsContext.Assert();
            GL.TexImage2D(target, level, internalFormat, width, height, border, format, type, pixels);
            OpenGlErrorHelper.CheckGlError();
        }
    }
}
