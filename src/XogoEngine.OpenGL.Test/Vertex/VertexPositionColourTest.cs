using NUnit.Framework;
using Shouldly;
using System.Collections.Generic;
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
        private static Vector2 position = Vector2.One;
        private static Vector4 colour = Vector4.One;

        [SetUp]
        public void SetUp()
        {
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

        [Test]
        public void ObjectEquals_ReturnsFalse_OnNullReference()
        {
            vertex.Equals(null).ShouldBeFalse();
        }

        [Test, TestCaseSource(nameof(UnequalVertices))]
        public void TypeEquals_ReturnsFalse_OnUnequalVertex(VertexPositionColour other)
        {
            vertex.Equals(other).ShouldBeFalse();
        }

        [Test, TestCaseSource(nameof(UnequalVertices))]
        public void ObjectEquals_ReturnsFalse_OnUnequalVertex(object other)
        {
            vertex.Equals(other).ShouldBeFalse();
        }

        [Test, TestCaseSource(nameof(EqualVertex))]
        public void TypeEquals_ReturnsTrue_OnEqualVertex(VertexPositionColour other)
        {
            vertex.Equals(other).ShouldBeTrue();
        }

        [Test, TestCaseSource(nameof(EqualVertex))]
        public void ObjectEquals_ReturnsTrue_OnEqualVertex(object other)
        {
            vertex.Equals(other).ShouldBeTrue();
        }

        [Test, TestCaseSource(nameof(EqualVertex))]
        public void EqualsOperator_ReturnsTrue_OnEqualVertex(VertexPositionColour other)
        {
            (vertex == other).ShouldBeTrue();
        }

        [Test, TestCaseSource(nameof(UnequalVertices))]
        public void EqualsOperator_ReturnsFalse_OnUnequalVertex(VertexPositionColour other)
        {
            (vertex == other).ShouldBeFalse();
        }

        [Test, TestCaseSource(nameof(UnequalVertices))]
        public void NotEqualsOperator_ReturnsTrue_OnUnequalVertex(VertexPositionColour other)
        {
            (vertex != other).ShouldBeTrue();
        }

        [Test, TestCaseSource(nameof(EqualVertex))]
        public void NotEqualsOperator_ReturnsFalse_OnEqualVertex(VertexPositionColour other)
        {
            (vertex != other).ShouldBeFalse();
        }

        [Test, TestCaseSource(nameof(EqualVertex))]
        public void HashCodes_ShouldBeEqual_ForEqualVertices(VertexPositionColour other)
        {
            vertex.GetHashCode().ShouldBe(other.GetHashCode());
        }

        [Test, TestCaseSource(nameof(UnequalVertices))]
        public void HashCodes_ShouldNotBeEqual_ForUnequalVertices(VertexPositionColour other)
        {
            vertex.GetHashCode().ShouldNotBe(other.GetHashCode());
        }

        private IEnumerable<TestCaseData> UnequalVertices
        {
            get
            {
                yield return new TestCaseData(new VertexPositionColour(
                    new Vector2(10, 5),
                    colour
                ));
                yield return new TestCaseData(new VertexPositionColour(
                    position,
                    Vector4.UnitW
                ));
            }
        }

        private IEnumerable<TestCaseData> EqualVertex
        {
            get
            {
                yield return new TestCaseData(new VertexPositionColour(
                    position,
                    colour
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
