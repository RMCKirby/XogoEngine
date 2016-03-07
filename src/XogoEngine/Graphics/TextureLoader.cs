using System;
using System.IO.Abstractions;
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

        public void Load(string path)
        {
            ValidatePath(path);
        }

        private void ValidatePath(string path)
        {
            if (string.IsNullOrEmpty(path) || string.IsNullOrWhiteSpace(path))
            {
                throw new ArgumentException("The given source string was null, empty or whitespace");
            }
        }
    }
}
