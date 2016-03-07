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
            
        }
    }
}
