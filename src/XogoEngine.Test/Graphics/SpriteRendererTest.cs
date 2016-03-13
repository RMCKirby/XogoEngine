using Moq;
using NUnit.Framework;
using Shouldly;
using System;
using XogoEngine.Graphics;
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

        [SetUp]
        public void SetUp()
        {
            shaderProgram = new Mock<IShaderProgram>();
            texture = new Mock<ITexture>();
            vertexArray = new Mock<IVertexArrayObject>();
            vertexBuffer = new Mock<IVertexBuffer<VertexPositionColourTexture>>();
            elementBuffer = new Mock<IElementBuffer<ushort>>();
        }

        [Test]
        public void Constructor_ThrowsArgumentNullException_OnNullShaderProgram()
        {
            Action construct = () => new SpriteRenderer(
                null,
                texture.Object,
                vertexArray.Object,
                vertexBuffer.Object,
                elementBuffer.Object
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
                elementBuffer.Object
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
                elementBuffer.Object
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
                elementBuffer.Object
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
                null
            );
            construct.ShouldThrow<ArgumentNullException>();
        }
    }
}
