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
            buffer = new VertexBuffer<int>(adapter.Object);
        }
    }
}
