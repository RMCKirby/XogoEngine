using NUnit.Framework;
using Moq;
using Shouldly;
using System;
using XogoEngine.Graphics.Primitives;
using XogoEngine.OpenGL.Adapters;
using XogoEngine.OpenGL.Primitives;
using XogoEngine.OpenGL.Vertex;

namespace XogoEngine.Test.Graphics
{
    [TestFixture]
    internal sealed class LineRendererTest
    {
        private LineRenderer renderer;
        private Mock<IDrawAdapter> adapter;
        private Mock<IVertexArrayObject> vao;
        private Mock<IVertexBuffer<VertexPositionColour>> vbo;

        [SetUp]
        public void SetUp()
        {
            adapter = new Mock<IDrawAdapter>();
            vao = new Mock<IVertexArrayObject>();
            vbo = new Mock<IVertexBuffer<VertexPositionColour>>();
            renderer = new LineRenderer(adapter.Object, vao.Object, vbo.Object);
        }

        [Test]
        public void Constructor_ThrowsArgumentNullException_OnNullDrawAdapter()
        {
            Action construct = () => new LineRenderer(null, vao.Object, vbo.Object);
            construct.ShouldThrow<ArgumentNullException>().ParamName.ShouldBe("adapter");
        }

        [Test]
        public void Constructor_ThrowsArgumentNullException_OnNullVertexArrayObject()
        {
            Action construct = () => new LineRenderer(adapter.Object, null, vbo.Object);
            construct.ShouldThrow<ArgumentNullException>().ParamName.ShouldBe("vao");
        }

        [Test]
        public void Constructor_ThrowsArgumentNullException_OnNullVertexBufferObject()
        {
            Action construct = () => new LineRenderer(adapter.Object, vao.Object, null);
            construct.ShouldThrow<ArgumentNullException>().ParamName.ShouldBe("vbo");
        }

        [Test]
        public void Draw_AddsLine_ToSubmittedLines()
        {
            //var line = new Line(new VertexPositionColour(vect))
            //renderer.Draw(new Line(new vertexpos))
        }
    }
}
