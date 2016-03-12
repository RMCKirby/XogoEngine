using System;
using System.Collections.Generic;

namespace XogoEngine.Graphics
{
    internal class TextureAtlas
    {
        public TextureAtlas(int width, int height)
        {
            Width = width;
            Height = height;
        }

        public int Width { get; }
        public int Height { get; }
    }
}
