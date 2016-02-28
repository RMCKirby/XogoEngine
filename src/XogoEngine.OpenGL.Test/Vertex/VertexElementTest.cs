using NUnit.Framework;
using Shouldly;
using XogoEngine.OpenGL.Vertex;

namespace XogoEngine.OpenGL.Test.Vertex
{
    [TestFixture]
    internal sealed class VertexElementTest
    {
        [Test]
        public void Constructor_CorrectlyInstantiates_Instance()
        {
            var element = new VertexElement(0);
            element.Offset.ShouldBe(0);
        }
    }
}
