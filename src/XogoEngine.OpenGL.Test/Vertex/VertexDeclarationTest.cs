using NUnit.Framework;
using Shouldly;
using OpenTK.Graphics.OpenGL4;
using XogoEngine.OpenGL.Vertex;

namespace XogoEngine.OpenGL.Test.Vertex
{
    [TestFixture]
    internal sealed class VertexDeclarationTest
    {
        private VertexDeclaration vertexDeclaration;
        private VertexElement[] vertexElements;

        [SetUp]
        public void SetUp()
        {
            vertexElements = new VertexElement[]
            {
                new VertexElement(0, "position", VertexAttribPointerType.Float, 2, false)
            };
            vertexDeclaration = new VertexDeclaration(20, vertexElements);
        }

        [Test]
        public void Constructor_CorrectlyInstantiates_instance()
        {
            vertexDeclaration.ShouldSatisfyAllConditions(
                () => vertexDeclaration.Stride.ShouldBe(20),
                () => vertexDeclaration.Elements.ShouldBe(vertexElements)
            );
        }
    }
}
