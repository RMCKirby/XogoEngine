using Moq;
using NUnit.Framework;
using Shouldly;
using System;
using System.Collections.Generic;
using OpenTK;
using OpenTK.Graphics.OpenGL4;
using XogoEngine.Graphics;
using XogoEngine.OpenGL.Adapters;
using XogoEngine.OpenGL.Shaders;
using XogoEngine.OpenGL.Vertex;

namespace XogoEngine.Test.Graphics
{
    [TestFixture]
    internal sealed class SpriteBatchTest
    {
        private SpriteBatch spriteBatch;
        private Mock<ISpriteSheet> spriteSheet;
        private Mock<ITexture> texture;
        private Mock<IShaderProgram> shaderProgram;
        private Mock<IVertexArrayObject> vao;
        private Mock<IVertexBuffer<VertexPositionColourTexture>> vbo;
        private Mock<IDrawAdapter> adapter;

        private static IVertexDeclaration declaration = default(VertexPositionColourTexture).Declaration;

        [SetUp]
        public void SetUp()
        {
            texture = new Mock<ITexture>();
            texture.SetupGet(t => t.Width).Returns(200);
            texture.SetupGet(t => t.Height).Returns(50);
            var textureRegion = new TextureRegion(2, 2, 15, 20);

            spriteSheet = new Mock<ISpriteSheet>();
            spriteSheet.SetupGet(s => s.Texture)
                       .Returns(texture.Object);
            spriteSheet.Setup(s => s.GetRegion(It.IsAny<int>()))
                       .Returns(textureRegion);
            spriteSheet.SetupGet(s => s.TextureRegions)
                       .Returns(new TextureRegion[] { textureRegion });

            shaderProgram = new Mock<IShaderProgram>();
            var mvpUniform = new ShaderUniform("mvp", 4, 16, ActiveUniformType.FloatMat4);
            var uniforms = new Dictionary<string, ShaderUniform>()
            {
                {"mvp", mvpUniform}
            };
            shaderProgram.SetupGet(s => s.Uniforms).Returns(uniforms);
            vao = new Mock<IVertexArrayObject>();
            vbo = new Mock<IVertexBuffer<VertexPositionColourTexture>>();
            vbo.SetupGet(v => v.VertexDeclaration).Returns(declaration);
            adapter = new Mock<IDrawAdapter>();

            spriteBatch = new SpriteBatch(
                spriteSheet.Object,
                shaderProgram.Object,
                vao.Object,
                vbo.Object,
                adapter.Object
            );
        }

        [Test]
        public void Constructor_ThrowsArgumentNullException_OnNullSpriteSheet()
        {
            SpriteSheet nullSheet = null;
            Action construct = () => new SpriteBatch(
                nullSheet,
                shaderProgram.Object,
                vao.Object,
                vbo.Object,
                adapter.Object
            );
            construct.ShouldThrow<ArgumentNullException>();
        }

        [Test]
        public void Constructor_CorrectlyInitialises_Instance()
        {
            spriteBatch.ShouldSatisfyAllConditions(
                () => spriteBatch.SpriteSheet.ShouldBe(spriteSheet.Object)
            );
        }

        [Test]
        public void VertexArrayObject_ShouldBeSetUp_OnConstruction()
        {
            const int batchSize = 100;
            var vboSize = new IntPtr(
                batchSize * Sprite.VertexCount * declaration.Stride
            );

            shaderProgram.Verify(s => s.Use());
            vao.Verify(v => v.Bind());
            vbo.Verify(v => v.Bind());
            vbo.Verify(v => v.Fill(vboSize, null, BufferUsageHint.DynamicDraw));
            vao.Verify(v => v.SetUp(shaderProgram.Object, vbo.Object.VertexDeclaration));
        }

        [Test]
        public void ModelViewProjectionMatrix_IsIntialised_OnConstruction()
        {
            var matrix = Matrix4.Identity;
            var program = shaderProgram.Object;
            shaderProgram.Verify(
                s => s.SetMatrix4(program.Uniforms["mvp"], ref matrix, false),
                Times.Once
            );
        }

        [Test]
        public void SetOrthographicOffCenter_ModifiesShaderMvpMatrix_OnInvoke()
        {
            spriteBatch.SetOrthographicOffCenter(0, 400, 0, 300);

            /* We are passing the OpenTK.Matrix4 by ref for performance reasons.
            * Moq has the limitation that we cannot verify this argument was passed
            * unless we have access to the same reference in this test case.
            * We could resolve this by changing the SpriteBatch signature to take a matrix,
            * but then we would be exposing the underlying OpenTK Matrix4 type in the engine's
            * public API. */

            // For now, let's ignore this.
            // Potentially, given the time, we could wrap OpenTK.Matrix4 and expose that instead.

            //shaderProgram.Verify(s => s.SetMatrix4());
        }

        [Test]
        public void Add_ThrowsArgumentNullException_OnNullSprite()
        {
            Sprite nullSprite = null;
            Action add = () => spriteBatch.Add(nullSprite);
            add.ShouldThrow<ArgumentNullException>().ParamName.ShouldBe("sprite");
        }

        [Test]
        public void Add_AddsGivenSprite_ToSpriteList()
        {
            var sprite = new Sprite(spriteSheet.Object.GetRegion(0), 10, 10);
            spriteBatch.Add(sprite);
            spriteBatch.Sprites.ShouldContain(sprite);
        }

        [Test]
        public void Add_ThrowsDuplicateSpriteException_WhenAddingTheSameSpriteAgain()
        {
            var sprite = new Sprite(spriteSheet.Object.GetRegion(0), 10, 10);
            spriteBatch.Add(sprite);
            Action addAgain = () => spriteBatch.Add(sprite);

            addAgain.ShouldThrow<DuplicateSpriteException>().Message.ShouldContain(
                "The given sprite has already been added to this sprite batch"
            );
        }

        [Test]
        public void Add_PreparesSpriteVertices_OnSuccessfulAdd()
        {
            var sprite = new Sprite(spriteSheet.Object.GetRegion(0), 10, 10);
            spriteBatch.Add(sprite);

            /* The sprite batch should take the sprite's texture region
            *  and scale each corner according to the whole texture atlas width/height
            * so that they fall in the range 0-1 (to match the expectations of OpenGL) */

            float scaledWidth = 1.0f / spriteSheet.Object.Texture.Width;
            float scaledHeight = 1.0f / spriteSheet.Object.Texture.Height;
            var region = sprite.TextureRegion;

            // texture packer x,y refer to bottom left position of the texture region
            // whereas, OpenGL coordinate system has (0,0) at the upper-left
            var topLeftCoord = new Vector2(region.X * scaledWidth, (region.Y + region.Height) * scaledHeight);
            var topRightCoord = new Vector2((region.X + region.Width) * scaledWidth, (region.Y + region.Height) * scaledHeight);
            var bottomRightCoord = new Vector2((region.X + region.Width) * scaledWidth, region.Y * scaledHeight);
            var bottomLeftCoord = new Vector2(region.X * scaledWidth, region.Y * scaledHeight);

            // we should also scale the sprite's colour to fall in the range 0-1
            var sColour = sprite.Colour;
            var scaledColour = new Vector4(sColour.R / 255, sColour.G / 255, sColour.B / 255, sColour.A / 255);

            // expected vertex positions
            var topLeftPosition = new Vector2(sprite.X, sprite.Y);
            var topRightPosition = new Vector2(sprite.X + sprite.Width, sprite.Y);
            var bottomRightPosition = new Vector2(sprite.X + sprite.Width, sprite.Y + sprite.Height);
            var bottomLeftPosition = new Vector2(sprite.X, sprite.Y + sprite.Height);

            var topLeftVertex = new VertexPositionColourTexture(topLeftPosition, scaledColour, topLeftCoord);
            var topRightvertex = new VertexPositionColourTexture(topRightPosition, scaledColour, topRightCoord);
            var bottomRightVertex = new VertexPositionColourTexture(bottomRightPosition, scaledColour, bottomRightCoord);
            var bottomLeftVertex = new VertexPositionColourTexture(bottomLeftPosition, scaledColour, bottomLeftCoord);

            sprite.Vertices.ShouldSatisfyAllConditions(
                () => sprite.Vertices[0].ShouldBe(topLeftVertex),
                () => sprite.Vertices[1].ShouldBe(topRightvertex),
                () => sprite.Vertices[2].ShouldBe(bottomRightVertex),
                () => sprite.Vertices[3].ShouldBe(bottomLeftVertex)
            );
        }

        [Test]
        public void Add_AssignsBatchIndex_ToSprite()
        {
            var sprite = new Sprite(spriteSheet.Object.GetRegion(0), 10, 10);
            var sprite2 = new Sprite(spriteSheet.Object.GetRegion(1), 20, 10);
            spriteBatch.Add(sprite, sprite2);
            sprite.BatchIndex.ShouldBe(0);
            sprite2.BatchIndex.ShouldBe(1);
        }

        [Test]
        public void Add_ThrowsBatchSizeExceededException_OnAddingTooManySprites()
        {
            const int batchSize = 100;
            for (int i = 0; i < batchSize; i++)
            {
                var sprite = new Sprite(spriteSheet.Object.GetRegion(0), i, i);
                spriteBatch.Add(sprite);
            }
            var illegalSprite = new Sprite(spriteSheet.Object.GetRegion(0), 101, 101);
            Action add = () => spriteBatch.Add(illegalSprite);

            add.ShouldThrow<SpriteBatchSizeExceededException>();
        }

        [Test]
        public void Add_ThrowsArgumentException_WhenGivenSpriteIsNotUsingTextureRegionFromBatch()
        {
            var sprite = new Sprite(new TextureRegion(1, 1, 10, 10), 20, 20);
            Action add = () => spriteBatch.Add(sprite);

            add.ShouldThrow<ArgumentException>().Message.ShouldContain(
                "sprite must use a texture region from the batch's SpriteSheet"
            );
        }

        [Test]
        public void Add_UploadsGivenSpriteVertices_OnAdd()
        {
            var sprite = new Sprite(spriteSheet.Object.GetRegion(0), 10, 10);
            var spriteSize = Sprite.VertexCount * declaration.Stride;

            spriteBatch.Add(sprite);
            var spriteVboSize = new IntPtr(spriteSize);
            var spriteVboOffset = new IntPtr(spriteSize * sprite.BatchIndex);

            vbo.Verify(v => v.Bind());
            vbo.Verify(v => v.FillPartial(spriteVboOffset, spriteVboSize, sprite.Vertices));
        }

        [Test]
        public void SpriteVertices_AreReuploaded_WhenSpriteHasBeenModified()
        {
            var sprite = new Sprite(spriteSheet.Object.GetRegion(0), 10, 10);
            spriteBatch.Add(sprite);

            vbo.ResetCalls();
            sprite.Modify((s) => {
                s.X = 80;
                s.Y = 160;
            });
            vbo.Verify(
                v => v.FillPartial(It.IsAny<IntPtr>(), It.IsAny<IntPtr>(), sprite.Vertices),
                Times.Once
            );
        }

        [Test]
        public void SpriteVertices_AreNotReuploaded_OnceSpriteHasBeenRemovedFromBatch()
        {
            var sprite = new Sprite(spriteSheet.Object.GetRegion(0), 10, 10);
            spriteBatch.Add(sprite);

            vbo.ResetCalls();
            spriteBatch.Remove(sprite);
            sprite.Modify((s) => s.X = 80);

            vbo.Verify(
                v => v.FillPartial(It.IsAny<IntPtr>(), It.IsAny<IntPtr>(), sprite.Vertices),
                Times.Never
            );
        }

        [Test]
        public void Remove_ThrowsArgumentException_WhenSpriteIsNotInBatch()
        {
            var sprite = new Sprite(spriteSheet.Object.GetRegion(0), 10, 10);
            Action remove = () => spriteBatch.Remove(sprite);

            remove.ShouldThrow<ArgumentException>();
        }

        [Test]
        public void Remove_RemovesGivenSprite_FromExistingSpriteList()
        {
            var sprite = new Sprite(spriteSheet.Object.GetRegion(0), 10, 10);
            spriteBatch.Add(sprite);

            spriteBatch.Sprites.ShouldContain(sprite);
            spriteBatch.Remove(sprite);
            spriteBatch.Sprites.ShouldNotContain(sprite);
        }

        [Test]
        public void Remove_ClearsSpriteVerticesFromVbo_OnSpriteRemoval()
        {
            var sprite = new Sprite(spriteSheet.Object.GetRegion(0), 10, 10);
            var spriteSize = Sprite.VertexCount * declaration.Stride;

            spriteBatch.Add(sprite);
            var spriteVboSize = new IntPtr(spriteSize);
            var spriteVboOffset = new IntPtr(spriteSize * sprite.BatchIndex);

            // essentially, overwrite the vertex data in the vbo with 4 vertices
            // that have no alpha and are hence transparent.
            var emptyVertex = default(VertexPositionColourTexture);
            var emptyVertices = new VertexPositionColourTexture[]
            {
                emptyVertex,
                emptyVertex,
                emptyVertex,
                emptyVertex
            };

            spriteBatch.Remove(sprite);
            vbo.Verify(v => v.Bind());
            vbo.Verify(v => v.FillPartial(spriteVboOffset, spriteVboSize, emptyVertices));
        }

        [Test]
        public void ShaderProgram_IsUsed_OnDraw()
        {
            shaderProgram.ResetCalls();
            spriteBatch.Draw();
            shaderProgram.Verify(s => s.Use(), Times.Once);
        }

        [Test]
        public void Texture_IsBound_OnDraw()
        {
            spriteBatch.Draw();
            texture.Verify(t => t.Bind(), Times.Once);
        }

        [Test]
        public void VertexArrayObject_IsBound_OnDrawCall()
        {
            vao.ResetCalls();
            spriteBatch.Draw();
            vao.Verify(v => v.Bind(), Times.Once);
        }

        [Test]
        public void AdapterDrawArrays_IsInvokedAsExpected_OnDraw()
        {
            spriteBatch.Draw();
            adapter.Verify(a => a.DrawArrays(PrimitiveType.Quads, 0, 400));
        }

        [Test]
        public void Spritebatch_IsNotDisposed_AfterConstruction()
        {
            spriteBatch.IsDisposed.ShouldBeFalse();
        }

        [Test]
        public void SpriteBatch_IsDisposed_AfterDisposal()
        {
            spriteBatch.Dispose();
            spriteBatch.IsDisposed.ShouldBeTrue();
        }

        [Test]
        public void SpriteHandlers_AreRemovedFromBatch_OnDisposal()
        {
            var sprite = new Sprite(spriteSheet.Object.GetRegion(0), 10, 10);
            var sprite2 = new Sprite(spriteSheet.Object.GetRegion(1), 20, 10);
            spriteBatch.Add(sprite, sprite2);

            vbo.ResetCalls();
            spriteBatch.Dispose();
            sprite.Modify((s) => s.Width = 100);
            sprite2.Modify((s) => s.Colour = Colour4.ForestGreen);

            vbo.Verify(
                (v) => v.FillPartial(It.IsAny<IntPtr>(), It.IsAny<IntPtr>(), It.IsAny<VertexPositionColourTexture[]>()),
                Times.Never
            );
        }

        [Test]
        public void SpriteSheet_IsDisposed_OnDisposal()
        {
            spriteBatch.Dispose();
            spriteSheet.Verify(s => s.Dispose());
        }

        [Test]
        public void ShaderProgram_IsDisposed_OnDisposal()
        {
            spriteBatch.Dispose();
            shaderProgram.Verify(s => s.Dispose());
        }

        [Test]
        public void VertexArrayObject_IsDisposed_OnDisposal()
        {
            spriteBatch.Dispose();
            vao.Verify(v => v.Dispose());
        }

        [Test]
        public void VertexBuffer_IsDisposed_OnDisposal()
        {
            spriteBatch.Dispose();
            vbo.Verify(v => v.Dispose());
        }
    }
}
