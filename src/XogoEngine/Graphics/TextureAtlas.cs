using System;
using System.Collections.Generic;

namespace XogoEngine.Graphics
{
    internal class TextureAtlas
    {
        private List<TextureRegion> regions = new List<TextureRegion>();

        public TextureAtlas(int width, int height)
        {
            Width = width;
            Height = height;
        }

        public int Width { get; }
        public int Height { get; }
        public IEnumerable<TextureRegion> TextureRegions { get { return regions; } }

        public void Add(TextureRegion textureRegion)
        {
            if (textureRegion == null)
            {
                throw new ArgumentNullException(nameof(textureRegion));
            }
            regions.Add(textureRegion);
        }
    }
}
