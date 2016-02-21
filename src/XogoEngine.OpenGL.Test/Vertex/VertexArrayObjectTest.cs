using Moq;
using NUnit.Framework;
using Shouldly;
using XogoEngine.OpenGL.Adapters;
using XogoEngine.OpenGL.Vertex;

namespace XogoEngine.OpenGL.Test.Vertex
{
    [TestFixture]
    internal sealed class VertexArrayObjectTest
    {
        private VertexArrayObject vertexArray;
        private Mock<IVertexArrayAdapter> vertexAdapter;
        private Mock<VertexBuffer> vertexBuffer;

        [SetUp]
        public void SetUp()
        {
            vertexArray = new VertexArrayObject();
        }
    }
}
