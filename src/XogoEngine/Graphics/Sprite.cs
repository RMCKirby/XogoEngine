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

        private bool modified = false;

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

        public int X
        {
            get { return x; }
            set {
                if (value != x)
                {
                    x = value;
                    modified = true;
                }
            }
        }
        public int Y
        {
            get { return y; }
            set {
                if (value != y)
                {
                    y = value;
                    modified = true;
                }
            }
        }
        public int Width
        {
            get { return width; }
            set {
                if (value != width)
                {
                    width = value;
                    modified = true;
                }
            }
        }
        public int Height
        {
            get { return height; }
            set {
                if (value != height)
                {
                    height = value;
                    modified = true;
                }
            }
        }
        public Colour4 Colour
        {
            get { return colour; }
            set {
                if (value != colour)
                {
                    colour = value;
                    modified = true;
                }
            }
        }
        public TextureRegion TextureRegion
        {
            get { return textureRegion; }
            set {
                if (value != textureRegion)
                {
                    textureRegion = value;
                    modified = true;
                }
            }
        }

        public void Modify(Action<Sprite> action)
        {
            action(this);
            if (modified)
            {
                OnSpriteModified();
                modified = false;
            }
        }

        private void OnSpriteModified()
        {
            SpriteModified?.Invoke(this, EventArgs.Empty);
        }

        internal VertexPositionColourTexture[] Vertices => vertices;
        internal int BatchIndex;
        internal event EventHandler SpriteModified;

        internal const int VertexCount = 4;
    }
}
