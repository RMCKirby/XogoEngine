using Moq;
using NUnit.Framework;
using Shouldly;
using System;
using XogoEngine.Graphics;
using XogoEngine.OpenGL.Adapters;
using XogoEngine.OpenGL.Shaders;
using XogoEngine.OpenGL.Vertex;

namespace XogoEngine.Test.OpenGL
{
    [TestFixture]
    internal sealed class SpriteRendererTest
    {
        private SpriteRenderer renderer;
        private Mock<IShaderProgram> shaderProgram;
        private Mock<ITexture> texture;
        private Mock<IVertexArrayObject> vertexArray;
        private Mock<IVertexBuffer<VertexPositionColourTexture>> vertexBuffer;
        private Mock<IElementBuffer<ushort>> elementBuffer;
        private Mock<IDrawAdapter> adapter;

        [SetUp]
        public void SetUp()
        {
            shaderProgram = new Mock<IShaderProgram>();
            texture = new Mock<ITexture>();
            vertexArray = new Mock<IVertexArrayObject>();
            vertexBuffer = new Mock<IVertexBuffer<VertexPositionColourTexture>>();
            elementBuffer = new Mock<IElementBuffer<ushort>>();
            adapter = new Mock<IDrawAdapter>();

            renderer = new SpriteRenderer(
                shaderProgram.Object,
                texture.Object,
                vertexArray.Object,
                vertexBuffer.Object,
                elementBuffer.Object,
                adapter.Object
            );
        }

        [Test]
        public void Constructor_ThrowsArgumentNullException_OnNullShaderProgram()
        {
            Action construct = () => new SpriteRenderer(
                null,
                texture.Object,
                vertexArray.Object,
                vertexBuffer.Object,
                elementBuffer.Object,
                adapter.Object
            );
            construct.ShouldThrow<ArgumentNullException>();
        }

        [Test]
        public void Constructor_ThrowsArgumentNullException_OnNullTexture()
        {
            Action construct = () => new SpriteRenderer(
                shaderProgram.Object,
                null,
                vertexArray.Object,
                vertexBuffer.Object,
                elementBuffer.Object,
                adapter.Object
            );
            construct.ShouldThrow<ArgumentNullException>();
        }

        [Test]
        public void Constructor_ThrowsArgumentNullException_OnNullVertexArrayObject()
        {
            Action construct = () => new SpriteRenderer(
                shaderProgram.Object,
                texture.Object,
                null,
                vertexBuffer.Object,
                elementBuffer.Object,
                adapter.Object
            );
            construct.ShouldThrow<ArgumentNullException>();
        }

        [Test]
        public void Constructor_ThrowsArgumentNullException_OnNullVertexBuffer()
        {
            Action construct = () => new SpriteRenderer(
                shaderProgram.Object,
                texture.Object,
                vertexArray.Object,
                null,
                elementBuffer.Object,
                adapter.Object
            );
            construct.ShouldThrow<ArgumentNullException>();
        }

        [Test]
        public void Constructor_ThrowsArgumentNullException_OnNullElementBuffer()
        {
            Action construct = () => new SpriteRenderer(
                shaderProgram.Object,
                texture.Object,
                vertexArray.Object,
                vertexBuffer.Object,
                null,
                adapter.Object
            );
            construct.ShouldThrow<ArgumentNullException>();
        }

        [Test]
        public void Constructor_ThrowsArgumentNullException_OnNullAdapter()
        {
            Action construct = () => new SpriteRenderer(
                shaderProgram.Object,
                texture.Object,
                vertexArray.Object,
                vertexBuffer.Object,
                elementBuffer.Object,
                null
            );
            construct.ShouldThrow<ArgumentNullException>();
        }

        [Test]
        public void Constructor_CorrectlyInitialises_Instance()
        {
            renderer.ShouldSatisfyAllConditions(
                () => renderer.ShaderProgram.ShouldBe(shaderProgram.Object),
                () => renderer.Texture.ShouldBe(texture.Object),
                () => renderer.Vao.ShouldBe(vertexArray.Object),
                () => renderer.Vbo.ShouldBe(vertexBuffer.Object),
                () => renderer.Ebo.ShouldBe(elementBuffer.Object),
                () => renderer.Adapter.ShouldBe(adapter.Object)
            );
        }
    }
}
