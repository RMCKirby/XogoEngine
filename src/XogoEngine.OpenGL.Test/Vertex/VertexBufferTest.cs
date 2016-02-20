using Moq;
using NUnit.Framework;
using OpenTK.Graphics.OpenGL4;
using Shouldly;
using XogoEngine.OpenGL.Adapters;
using XogoEngine.OpenGL.Vertex;

namespace XogoEngine.OpenGL.Test.Vertex
{
    [TestFixture]
    internal sealed class VertexBufferTest
    {
        private VertexBuffer<int> buffer;
        private Mock<IBufferAdapter> adapter;

        [SetUp]
        public void SetUp()
        {
            adapter = new Mock<IBufferAdapter>();
            adapter.Setup(a => a.GenBuffer())
                   .Returns(1);
            buffer = new VertexBuffer<int>(adapter.Object);
        }

        [Test]
        public void AdapterGenBuffer_isInvokedOnlyOnce_OnConstruction()
        {
            adapter.Verify(a => a.GenBuffer(), Times.Once);
        }

        [Test]
        public void Constructor_CorrectlyInitialises_Instance()
        {
            buffer.ShouldSatisfyAllConditions(
                () => buffer.Handle.ShouldBe(1),
                () => buffer.IsDisposed.ShouldBe(false)
            );
        }
    }
}
