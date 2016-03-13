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
        private VertexPositionColourTexture[] vertices = new VertexPositionColourTexture[4];

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
            InitialiseVertices();
        }

        public int X => x;
        public int Y => y;
        public int Width => width;
        public int Height => height;
        public Colour4 Colour => colour;
        public TextureRegion TextureRegion => textureRegion;
        internal VertexPositionColourTexture[] Vertices => vertices;

        /* Will need to revisit once at the stage to render on screen
        * Feel these will be incorrect as of now */
        private void InitialiseVertices()
        {
            vertices[0] = new VertexPositionColourTexture(
                new Vector2(textureRegion.X, textureRegion.Y),
                new Vector4(colour.R, colour.G, colour.B, colour.A),
                new Vector2(textureRegion.X, textureRegion.Y)
            );// top left vertex
            vertices[1] = new VertexPositionColourTexture(
                new Vector2(textureRegion.X + width, textureRegion.Y),
                new Vector4(colour.R, colour.G, colour.B, colour.A),
                new Vector2(textureRegion.X + width, textureRegion.Y)
            );// top right vertex
            vertices[2] = new VertexPositionColourTexture(
                new Vector2(textureRegion.X + width, textureRegion.Y + height),
                new Vector4(colour.R, colour.G, colour.B, colour.A),
                new Vector2(textureRegion.X + width, textureRegion.Y + height)
            );// bottom right vertex
            vertices[3] = new VertexPositionColourTexture(
                new Vector2(textureRegion.X, textureRegion.Y + height),
                new Vector4(colour.R, colour.G, colour.B, colour.A),
                new Vector2(textureRegion.X, textureRegion.Y + height)
            );// bottom left vertex
        }
    }
}
