using Moq;
using NUnit.Framework;
using Shouldly;
using OpenTK.Graphics.OpenGL4;
using XogoEngine.OpenGL.Adapters;
using XogoEngine.OpenGL.Vertex;

namespace XogoEngine.OpenGL.Test.Vertex
{
    [TestFixture]
    internal sealed class ElementBufferTest
    {
        private ElementBuffer<ushort> elementBuffer;
        private Mock<IBufferAdapter> adapter;

        [SetUp]
        public void SetUp()
        {
            adapter = new Mock<IBufferAdapter>();
            adapter.Setup(a => a.GenBuffer())
                   .Returns(1);

            elementBuffer = new ElementBuffer<ushort>(adapter.Object);
        }

        [Test]
        public void Constructor_CorrectlyInitialises_Instance()
        {
            elementBuffer.ShouldSatisfyAllConditions(
                () => elementBuffer.Handle.ShouldBe(1),
                () => elementBuffer.IsDisposed.ShouldBeFalse(),
                () => elementBuffer.Target.ShouldBe(BufferTarget.ElementArrayBuffer)
            );
        }
    }
}
