using System;
using System.Drawing;
using Imaging = System.Drawing.Imaging;
using System.IO;
using System.IO.Abstractions;
using System.Runtime.InteropServices;
using OpenTK.Graphics.OpenGL4;
using XogoEngine.OpenGL.Adapters;

namespace XogoEngine.Graphics
{
    public sealed class TextureLoader
    {
        private readonly ITextureAdapter adapter;
        private readonly IFileSystem fileSystem;

        internal TextureLoader(ITextureAdapter adapter, IFileSystem fileSystem)
        {
            if (adapter == null)
            {
                throw new ArgumentNullException(nameof(adapter));
            }
            if (fileSystem == null)
            {
                throw new ArgumentNullException(nameof(fileSystem));
            }
            this.adapter = adapter;
            this.fileSystem = fileSystem;
        }

        public Texture Load(string path)
        {
            ValidatePath(path);
            int textureHandle = adapter.GenTexture();
            adapter.Bind(TextureTarget.Texture2D, textureHandle);
            SetTextureParameters();

            using (var image = new Bitmap(Bitmap.FromFile(path)))
            {
                var data = new byte[image.Width * image.Height * 4];
                var bitmapData = image.LockBits(
                    new Rectangle(0, 0, image.Width, image.Height),
                    Imaging.ImageLockMode.ReadOnly,
                    Imaging.PixelFormat.Format32bppArgb
                );
                Marshal.Copy(bitmapData.Scan0, data, 0, data.Length);
                adapter.TexImage2D(
                    TextureTarget.Texture2D,
                    0,
                    PixelInternalFormat.Rgba,
                    bitmapData.Width,
                    bitmapData.Height,
                    0,
                    PixelFormat.Bgra,
                    PixelType.UnsignedByte,
                    bitmapData.Scan0
                );
                image.UnlockBits(bitmapData);

                var texture = new Texture(adapter, textureHandle, image.Width, image.Height, data);
                return texture;
            }
        }

        private void SetTextureParameters()
        {
            adapter.TexParameter(
                TextureTarget.Texture2D,
                TextureParameterName.TextureMinFilter,
                (int)TextureMinFilter.Nearest
            );
            adapter.TexParameter(
                TextureTarget.Texture2D,
                TextureParameterName.TextureMagFilter,
                (int)TextureMagFilter.Nearest
            );
            adapter.TexParameter(
                TextureTarget.Texture2D,
                TextureParameterName.TextureWrapS,
                (int)TextureParameterName.ClampToEdge
            );
            adapter.TexParameter(
                TextureTarget.Texture2D,
                TextureParameterName.TextureWrapT,
                (int)TextureParameterName.ClampToEdge
            );
        }

        private void ValidatePath(string path)
        {
            if (string.IsNullOrEmpty(path) || string.IsNullOrWhiteSpace(path))
            {
                throw new ArgumentException("The given path string was null, empty or whitespace");
            }
            if (!fileSystem.File.Exists(path))
            {
                throw new FileNotFoundException(path);
            }
        }
    }
}
