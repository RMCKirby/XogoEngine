using System;

namespace XogoEngine.Graphics
{
    public sealed class Sprite
    {
        private TextureRegion textureRegion;
        private int x;
        private int y;
        private int width;
        private int height;

        public Sprite(TextureRegion textureRegion, int x, int y)
        {
            if (textureRegion == null)
            {
                throw new ArgumentNullException(nameof(textureRegion));
            }
            this.textureRegion = textureRegion;
            this.x = x;
            this.y = y;
            this.width = textureRegion.Width;
            this.height = textureRegion.Height;
        }

        public int X => x;
        public int Y => y;
        public int Width => width;
        public int Height => height;
        public TextureRegion TextureRegion => textureRegion;
    }
}
