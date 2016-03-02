using NUnit.Framework;
using Shouldly;
using System.Linq;
using OpenTK;
using OpenTK.Graphics.OpenGL4;
using XogoEngine.OpenGL.Vertex;

namespace XogoEngine.OpenGL.Test.Vertex
{
    [TestFixture]
    internal sealed class VertexPositionColourTest
    {
        private VertexPositionColour vertex;
        private Vector2 position;
        private Vector4 colour;

        [SetUp]
        public void SetUp()
        {
            position = new Vector2(1, 2);
            colour = new Vector4(1, 1, 1, 1);
            vertex = new VertexPositionColour(position, colour);
        }

        [Test]
        public void Constructor_CorrectlyInstantiates_Instance()
        {
            vertex.ShouldSatisfyAllConditions(
                () => vertex.Position.ShouldBe(position),
                () => vertex.Colour.ShouldBe(colour)
            );
        }

        [Test]
        public void VertexStride_ShouldBeExpectedSize_ForDeclaration()
        {
            vertex.Declaration.Stride.ShouldBe(6 * sizeof(float));
        }

        [Test]
        public void VertexElements_ShouldContain_ExpectedElements()
        {
            vertex.Declaration.Elements.Length.ShouldBe(2);
            vertex.Declaration.Elements.ShouldContain(e => e.Usage == "position");
            vertex.Declaration.Elements.ShouldContain(e => e.Usage == "colour");
        }

        [Test]
        public void VertexElements_ShouldHave_ExpectedProperties()
        {
            var position = vertex.Declaration.Elements.Single(e => e.Usage == "position");
            var colour = vertex.Declaration.Elements.Single(e => e.Usage == "colour");

            var pointerType = VertexAttribPointerType.Float;
            AssertVertexElementProperties(position, 0, pointerType, 2, false);
            AssertVertexElementProperties(colour, 8, pointerType, 4, false);
        }

        private void AssertVertexElementProperties(
            VertexElement element,
            int offset,
            VertexAttribPointerType type,
            int numberOfComponents,
            bool normalised)
        {
            element.ShouldSatisfyAllConditions(
                () => element.Offset.ShouldBe(offset),
                () => element.PointerType.ShouldBe(type),
                () => element.NumberOfComponents.ShouldBe(numberOfComponents),
                () => element.Normalised.ShouldBe(normalised)
            );
        }
    }
}
