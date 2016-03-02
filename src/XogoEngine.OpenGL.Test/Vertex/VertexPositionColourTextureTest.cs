using NUnit.Framework;
using OpenTK;
using OpenTK.Graphics.OpenGL4;
using Shouldly;
using System.Linq;
using XogoEngine.OpenGL.Vertex;

namespace XogoEngine.Test.OpenGL.Vertex
{
    [TestFixture]
    internal sealed class VertexPositionColourTextureTest
    {
        private VertexPositionColourTexture vertex;
        private Vector2 position;
        private Vector4 colour;
        private Vector2 textureCoordinate;

        [SetUp]
        public void SetUp()
        {
            position = Vector2.Zero;
            colour = Vector4.Zero;
            textureCoordinate = Vector2.One;

            vertex = new VertexPositionColourTexture(
                position,
                colour,
                textureCoordinate
            );
        }

        [Test]
        public void Constructor_CorrectlyInstantiates_Instance()
        {
            vertex.ShouldSatisfyAllConditions(
                () => vertex.Position.ShouldBe(position),
                () => vertex.Colour.ShouldBe(colour),
                () => vertex.TextureCoordinate.ShouldBe(textureCoordinate)
            );
        }

        [Test]
        public void VertexStride_ShouldBe_ExpectedSize()
        {
            vertex.Declaration.Stride.ShouldBe(8 * sizeof(float));
        }

        [Test]
        public void VertexElements_ShouldContain_ExpectedElements()
        {
            vertex.Declaration.Elements.Length.ShouldBe(3);
            vertex.Declaration.Elements.ShouldContain(e => e.Usage == "position");
            vertex.Declaration.Elements.ShouldContain(e => e.Usage == "colour");
            vertex.Declaration.Elements.ShouldContain(e => e.Usage == "texCoord");
        }

        [Test]
        public void VertexElements_ShouldHave_ExpectedProperties()
        {
            var position = vertex.Declaration.Elements.Single(e => e.Usage == "position");
            var colour = vertex.Declaration.Elements.Single(e => e.Usage == "colour");
            var textureCoordinate = vertex.Declaration.Elements.Single(e => e.Usage == "texCoord");

            var pointerType = VertexAttribPointerType.Float;
            AssertVertexElementProperties(position, 0, pointerType, 2, false);
            AssertVertexElementProperties(colour, 8, pointerType, 4, false);
            AssertVertexElementProperties(textureCoordinate, 24, pointerType, 2, false);
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
