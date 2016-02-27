using Moq;
using NUnit.Framework;
using Shouldly;
using System;
using XogoEngine.OpenGL.Adapters;
using XogoEngine.OpenGL.Vertex;

namespace XogoEngine.OpenGL.Test.Vertex
{
    [TestFixture]
    internal sealed class VertexArrayObjectTest
    {
        private VertexArrayObject vertexArray;
        private Mock<IVertexArrayAdapter> adapter;
        private Mock<IVertexBuffer<VertexPosition>> vertexBuffer;

        [SetUp]
        public void SetUp()
        {
            adapter = new Mock<IVertexArrayAdapter>();
            adapter.Setup(a => a.GenVertexArray())
                   .Returns(1);

            vertexBuffer = new Mock<IVertexBuffer<VertexPosition>>();
            vertexArray = new VertexArrayObject(adapter.Object);
        }

        [Test]
        public void Constructor_ThrowsArgumentNullException_OnNullAdapter()
        {
            Action construct = () => new VertexArrayObject(adapter: null);
            construct.ShouldThrow<ArgumentNullException>(nameof(adapter));
        }

        [Test]
        public void Instance_IsCorrectlySet_OnConstruction()
        {
            vertexArray.ShouldSatisfyAllConditions(
                () => vertexArray.Handle.ShouldBe(1),
                () => vertexArray.IsDisposed.ShouldBe(false)
            );
        }

        private struct VertexPosition : IVertexDeclarable
        {

        }
    }
}
