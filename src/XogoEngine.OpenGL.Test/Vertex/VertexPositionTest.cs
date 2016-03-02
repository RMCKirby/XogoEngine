using NUnit.Framework;
using Shouldly;
using System.Linq;
using OpenTK;
using OpenTK.Graphics.OpenGL4;
using XogoEngine.OpenGL.Vertex;

namespace XogoEngine.OpenGL.Test.Vertex
{
    [TestFixture]
    internal sealed class VertexPositionTest
    {
        private VertexPosition vertex;

        [SetUp]
        public void SetUp()
        {
            vertex = new VertexPosition(new Vector2(5, 10));
        }

        [Test]
        public void VertexPosition_ShouldEqual_InjectedVector()
        {
            vertex.ShouldSatisfyAllConditions(
                () => vertex.Position.X.ShouldBe(5),
                () => vertex.Position.Y.ShouldBe(10)
            );
        }

        [Test]
        public void VertexStride_ShouldBeExpectedSize_ForDeclaration()
        {
            vertex.Declaration.Stride.ShouldBe(2 * sizeof(float));
        }

        [Test]
        public void VertexElements_ShouldContain_SinglePositionElement_WithExpectedProperties()
        {
            vertex.Declaration.Elements.Length.ShouldBe(1);
            var positionElement = vertex.Declaration.Elements.Single();

            positionElement.ShouldSatisfyAllConditions(
                () => positionElement.Offset.ShouldBe(0),
                () => positionElement.Usage.ShouldBe(VertexElementUsage.Position),
                () => positionElement.PointerType.ShouldBe(VertexAttribPointerType.Float),
                () => positionElement.NumberOfComponents.ShouldBe(2),
                () => positionElement.Normalised.ShouldBe(false)
            );
        }
    }
}
