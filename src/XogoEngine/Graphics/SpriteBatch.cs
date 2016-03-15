using System;
using System.Collections.Generic;
using System.Linq;
using OpenTK;
using XogoEngine.OpenGL.Adapters;
using XogoEngine.OpenGL.Shaders;
using XogoEngine.OpenGL.Vertex;

namespace XogoEngine.Graphics
{
    public sealed class SpriteBatch : IDisposable
    {
        private readonly ISpriteSheet spriteSheet;
        private List<Sprite> sprites = new List<Sprite>();
        private bool isDisposed = false;

        private Queue<int> availableSlots = new Queue<int>(Enumerable.Range(0, BatchSize));
        private readonly IShaderProgram shaderProgram;
        private readonly IVertexArrayObject vao;
        private readonly IVertexBuffer<VertexPositionColourTexture> vbo;
        private readonly IDrawAdapter drawAdapter;

        private const int BatchSize = 100;

        public SpriteBatch(ISpriteSheet spriteSheet)
        {
            // TODO: chain to internal constructor once concrete implementation in place
            spriteSheet.ThrowIfNull(nameof(spriteSheet));
            this.spriteSheet = spriteSheet;
        }

        internal SpriteBatch(
            ISpriteSheet spriteSheet,
            IShaderProgram shaderProgram,
            IVertexArrayObject vao,
            IVertexBuffer<VertexPositionColourTexture> vbo,
            IDrawAdapter drawAdapter)
        {
            spriteSheet.ThrowIfNull(nameof(spriteSheet));
            this.spriteSheet = spriteSheet;
            this.shaderProgram = shaderProgram;
            this.vao = vao;
            this.vbo = vbo;
            this.drawAdapter = drawAdapter;
        }

        public ISpriteSheet SpriteSheet => spriteSheet;
        public IEnumerable<Sprite> Sprites => sprites;
        public bool IsDisposed => isDisposed;

        public void Add(params Sprite[] sprites)
        {
            foreach (var sprite in sprites)
            {
                Add(sprite);
            }
        }

        public void Add(Sprite sprite)
        {
            EnsureValidToAdd(sprite);
            ValidateBatchSize();
            PrepareSpriteVertices(sprite);
            sprites.Add(sprite);
            sprite.BatchIndex = availableSlots.Dequeue();
        }

        public void Remove(params Sprite[] sprites)
        {
            foreach (var sprite in sprites)
            {
                Remove(sprite);
            }
        }

        public void Remove(Sprite sprite)
        {
            if (!sprites.Contains(sprite))
            {
                throw new ArgumentException(
                    nameof(sprite) + " was not found in this sprite batch"
                );
            }
            sprites.Remove(sprite);
            // may need a dictionary of weak references of sprites to batchIndexes
            // in the case a sprite becomes null after being added to the batch
            availableSlots.Enqueue(sprite.BatchIndex);
        }

        public void Dispose()
        {
            if (isDisposed)
            {
                return;
            }
            spriteSheet.Dispose();
            isDisposed = true;
            GC.SuppressFinalize(this);
        }

        private void PrepareSpriteVertices(Sprite sprite)
        {
            /* Take the sprite's texture region and scale each corner according
            * to the whole texture atlas width/height so that they fall in the
            * range 0-1 (to match the expectations of OpenGL) */
            float scaledWidth = 1.0f / spriteSheet.Texture.Width;
            float scaledHeight = 1.0f / spriteSheet.Texture.Height;
            var region = sprite.TextureRegion;

            /* TextureRegion x,y refer to bottom left position of the quad
            * whereas, OpenGL coordinate system has (0,0) at the upper-left */
            var topLeftCoord = new Vector2(region.X * scaledWidth, (region.Y + region.Height) * scaledHeight);
            var topRightCoord = new Vector2((region.X + region.Width) * scaledWidth, (region.Y + region.Height) * scaledHeight);
            var bottomRightCoord = new Vector2((region.X + region.Width) * scaledWidth, region.Y * scaledHeight);
            var bottomLeftCoord = new Vector2(region.X * scaledWidth, region.Y * scaledHeight);

            // Scale the sprite's colour to fall in the range 0-1
            var sColour = sprite.Colour;
            var scaledColour = new Vector4(sColour.R / 255, sColour.G / 255, sColour.B / 255, sColour.A / 255);

            var topLeftPosition = new Vector2(sprite.X, sprite.Y + scaledHeight);
            var topRightPosition = new Vector2(sprite.X + scaledWidth, sprite.Y + scaledHeight);
            var bottomRightPosition = new Vector2(sprite.X + scaledWidth, sprite.Y);
            var bottomLeftPosition = new Vector2(sprite.X, sprite.Y);

            sprite.Vertices[0] = new VertexPositionColourTexture(topLeftPosition, scaledColour, topLeftCoord);
            sprite.Vertices[1] = new VertexPositionColourTexture(topRightPosition, scaledColour, topRightCoord);
            sprite.Vertices[2] = new VertexPositionColourTexture(bottomRightPosition, scaledColour, bottomRightCoord);
            sprite.Vertices[3] = new VertexPositionColourTexture(bottomLeftPosition, scaledColour, bottomLeftCoord);
        }

        private void ValidateBatchSize()
        {
            if (sprites.Count == BatchSize)
            {
                throw new SpriteBatchSizeExceededException(
                    "The sprite batch is full. No free slots."
                );
            }
        }

        private void EnsureValidToAdd(Sprite sprite)
        {
            sprite.ThrowIfNull(nameof(sprite));
            if (!spriteSheet.TextureRegions.Contains(sprite.TextureRegion))
            {
                throw new ArgumentException(
                    "The given sprite must use a texture region from the batch's SpriteSheet"
                );
            }
            if (sprites.Contains(sprite))
            {
                throw new DuplicateSpriteException(
                    "The given sprite has already been added to this sprite batch"
                );
            }
        }
    }
}
