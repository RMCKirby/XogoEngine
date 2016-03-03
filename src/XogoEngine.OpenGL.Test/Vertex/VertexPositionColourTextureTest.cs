using NUnit.Framework;
using OpenTK;
using OpenTK.Graphics.OpenGL4;
using Shouldly;
using System.Collections.Generic;
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
            textureCoordinate = Vector2.Zero;

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

        [Test]
        public void ObjectEquals_ReturnsFalse_OnNullReference()
        {
            vertex.Equals(null).ShouldBeFalse();
        }

        [Test, TestCaseSource(nameof(UnequalVertices))]
        public void TypeEquals_ReturnsFalse_OnUnequalVertices(VertexPositionColourTexture other)
        {
            vertex.Equals(other).ShouldBeFalse();
        }

        [Test, TestCaseSource(nameof(UnequalVertices))]
        public void ObjectEquals_ReturnsFalse_OnUnequalVertices(object other)
        {
            vertex.Equals(other).ShouldBeFalse();
        }

        [Test, TestCaseSource(nameof(EqualVertex))]
        public void TypeEquals_ReturnsTrue_OnEqualVertex(VertexPositionColourTexture other)
        {
            vertex.Equals(other).ShouldBeTrue();
        }

        [Test, TestCaseSource(nameof(EqualVertex))]
        public void ObjectEquals_ReturnsTrue_OnEqualVertex(object other)
        {
            vertex.Equals(other).ShouldBeTrue();
        }

        [Test, TestCaseSource(nameof(EqualVertex))]
        public void EqualsOperator_ReturnsTrue_OnEqualVertex(VertexPositionColourTexture other)
        {
            (vertex == other).ShouldBeTrue();
        }

        [Test, TestCaseSource(nameof(UnequalVertices))]
        public void EqualsOperator_ReturnsFalse_OnUnequalVertices(VertexPositionColourTexture other)
        {
            (vertex == other).ShouldBeFalse();
        }

        [Test, TestCaseSource(nameof(UnequalVertices))]
        public void NotEqualsOperator_ReturnsTrue_OnUnequalVertices(VertexPositionColourTexture other)
        {
            (vertex != other).ShouldBeTrue();
        }

        [Test, TestCaseSource(nameof(EqualVertex))]
        public void NotEqualsOperator_ReturnsFalse_OnEqualVertex(VertexPositionColourTexture other)
        {
            (vertex != other).ShouldBeFalse();
        }

        [Test, TestCaseSource(nameof(EqualVertex))]
        public void GetHashcode_IsEqual_ForEqualVertices(VertexPositionColourTexture other)
        {
            vertex.GetHashCode().ShouldBe(other.GetHashCode());
        }

        [Test, TestCaseSource(nameof(UnequalVertices))]
        public void GetHashCode_IsNotEqual_ForUnequalVertices(VertexPositionColourTexture other)
        {
            vertex.GetHashCode().ShouldNotBe(other.GetHashCode());
        }

        private IEnumerable<TestCaseData> UnequalVertices
        {
            get
            {
                yield return new TestCaseData(new VertexPositionColourTexture(
                    Vector2.One,
                    vertex.Colour,
                    vertex.TextureCoordinate
                ));
                yield return new TestCaseData(new VertexPositionColourTexture(
                    vertex.Position,
                    Vector4.One,
                    vertex.TextureCoordinate
                ));
                yield return new TestCaseData(new VertexPositionColourTexture(
                    vertex.Position,
                    vertex.Colour,
                    Vector2.One
                ));
            }
        }

        private IEnumerable<TestCaseData> EqualVertex
        {
            get
            {
                yield return new TestCaseData(new VertexPositionColourTexture(
                    vertex.Position,
                    vertex.Colour,
                    vertex.TextureCoordinate
                ));
            }
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
