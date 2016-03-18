using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using OpenTK;
using OpenTK.Graphics.OpenGL4;
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
        private readonly IDrawAdapter adapter;

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
            IDrawAdapter adapter)
        {
            spriteSheet.ThrowIfNull(nameof(spriteSheet));
            this.spriteSheet = spriteSheet;
            Debug.Assert(shaderProgram != null, $"{nameof(shaderProgram)} was null in SpriteBatch");
            Debug.Assert(vao != null, $"{nameof(vao)} was null in SpriteBatch");
            Debug.Assert(vbo != null, $"{nameof(vbo)} was null ins SpriteBatch");
            Debug.Assert(adapter != null, $"{nameof(adapter)} was null in SpriteBatch");
            this.shaderProgram = shaderProgram;
            this.vao = vao;
            this.vbo = vbo;
            this.adapter = adapter;

            Initialise();
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
            sprite.SpriteModified += HandleSpriteModified;
            sprite.BatchIndex = availableSlots.Dequeue();
            UploadSpriteVertices(sprite);
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
            sprite.SpriteModified -= HandleSpriteModified;
            // may need a dictionary of weak references of sprites to batchIndexes
            // in the case a sprite becomes null after being added to the batch
            availableSlots.Enqueue(sprite.BatchIndex);
            ClearSpriteData(sprite);
        }

        public void SetOrthographicOffCenter(float left, float right, float bottom, float top)
        {
            var matrix = Matrix4.CreateOrthographicOffCenter(left, right, bottom, top, -1, 1);
            shaderProgram.SetMatrix4(
                shaderProgram.Uniforms["mvp"],
                ref matrix,
                false
            );
        }

        public void Draw()
        {
            shaderProgram.Use();
            spriteSheet.Texture.Bind();
            vao.Bind();
            adapter.DrawArrays(PrimitiveType.Quads, 0, BatchSize * Sprite.VertexCount);
        }

        public void Dispose()
        {
            if (isDisposed)
            {
                return;
            }
            sprites.ForEach((s) => s.SpriteModified -= HandleSpriteModified);
            shaderProgram?.Dispose();
            vao?.Dispose();
            vbo?.Dispose();
            spriteSheet?.Dispose();
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

            var topLeftPosition = new Vector2(sprite.X, sprite.Y);
            var topRightPosition = new Vector2(sprite.X + sprite.Width, sprite.Y);
            var bottomRightPosition = new Vector2(sprite.X + sprite.Width, sprite.Y + sprite.Height);
            var bottomLeftPosition = new Vector2(sprite.X, sprite.Y + sprite.Height);

            sprite.Vertices[0] = new VertexPositionColourTexture(topLeftPosition, scaledColour, topLeftCoord);
            sprite.Vertices[1] = new VertexPositionColourTexture(topRightPosition, scaledColour, topRightCoord);
            sprite.Vertices[2] = new VertexPositionColourTexture(bottomRightPosition, scaledColour, bottomRightCoord);
            sprite.Vertices[3] = new VertexPositionColourTexture(bottomLeftPosition, scaledColour, bottomLeftCoord);
        }

        private void Initialise()
        {
            var vboSize = new IntPtr(
                BatchSize * Sprite.VertexCount * vbo.VertexDeclaration.Stride
            );
            shaderProgram.Use();
            vao.Bind();
            vbo.Bind();
            vbo.Fill(vboSize, null, BufferUsageHint.DynamicDraw);
            InitialiseShaderUniforms();
            vao.SetUp(shaderProgram, vbo.VertexDeclaration);
        }

        private void InitialiseShaderUniforms()
        {
            // load the uniform into shaderProgram.Uniforms
            shaderProgram.GetUniformLocation("mvp");
            Debug.Assert(shaderProgram.Uniforms.ContainsKey("mvp"));

            var defaultMatrix = Matrix4.Identity;
            shaderProgram.SetMatrix4(shaderProgram.Uniforms["mvp"], ref defaultMatrix, false);
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

        private void HandleSpriteModified(object sender, EventArgs args)
        {
            Debug.Assert(sender.GetType() == typeof(Sprite));

            var sprite = (Sprite)sender;
            PrepareSpriteVertices(sprite);
            UploadSpriteVertices(sprite);
        }

        private void UploadSpriteVertices(Sprite sprite)
        {
            var size = new IntPtr(vbo.VertexDeclaration.Stride * Sprite.VertexCount);
            var offset = new IntPtr(
                vbo.VertexDeclaration.Stride * Sprite.VertexCount * sprite.BatchIndex
            );
            vbo.Bind();
            vbo.FillPartial(offset, size, sprite.Vertices);
        }

        private void ClearSpriteData(Sprite sprite)
        {
            var size = new IntPtr(vbo.VertexDeclaration.Stride * Sprite.VertexCount);
            var offset = new IntPtr(
                vbo.VertexDeclaration.Stride * Sprite.VertexCount * sprite.BatchIndex
            );
            vbo.Bind();
            vbo.FillPartial(offset, size, new VertexPositionColourTexture[4]);
        }
    }
}
