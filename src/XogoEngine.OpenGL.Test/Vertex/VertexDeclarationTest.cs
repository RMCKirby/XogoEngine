using NUnit.Framework;
using Shouldly;
using XogoEngine.OpenGL.Vertex;

namespace XogoEngine.OpenGL.Test.Vertex
{
    [TestFixture]
    internal sealed class VertexDeclarationTest
    {
        [Test]
        public void Constructor_CorrectlyInstantiates_instance()
        {
            var vertexDeclaration = new VertexDeclaration(20);
            vertexDeclaration.Stride.ShouldBe(20);
        }
    }
}
