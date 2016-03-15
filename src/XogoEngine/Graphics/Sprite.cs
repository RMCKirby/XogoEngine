using System;
using OpenTK;
using XogoEngine.OpenGL.Vertex;

namespace XogoEngine.Graphics
{
    public sealed class Sprite
    {
        private TextureRegion textureRegion;
        private int x;
        private int y;
        private int width;
        private int height;
        private Colour4 colour;
        private VertexPositionColourTexture[] vertices = new VertexPositionColourTexture[VertexCount];

        public Sprite(TextureRegion textureRegion, int x, int y)
            : this(textureRegion, x, y, Colour4.White)
        {
        }

        public Sprite(TextureRegion textureRegion, int x, int y, Colour4 colour)
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
            this.colour = colour;
        }

        public int X => x;
        public int Y => y;
        public int Width => width;
        public int Height => height;
        public Colour4 Colour => colour;
        public TextureRegion TextureRegion => textureRegion;
        internal VertexPositionColourTexture[] Vertices => vertices;

        internal const int VertexCount = 4;
    }
}
