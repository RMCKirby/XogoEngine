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
    internal sealed class VertexPositionTest
    {
        private VertexPosition vertex;
        private static Vector2 position = Vector2.One;

        [SetUp]
        public void SetUp()
        {
            vertex = new VertexPosition(position);
        }

        [Test]
        public void VertexPosition_ShouldEqual_InjectedVector()
        {
            vertex.Position.ShouldBe(position);
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

        [Test]
        public void ObjectEquals_ReturnsFalse_OnNullReference()
        {
            vertex.Equals(null).ShouldBeFalse();
        }

        [Test, TestCaseSource(nameof(UnequalVertex))]
        public void TypeEquals_ReturnsFalse_OnUnequalVertex(VertexPosition other)
        {
            vertex.Equals(other).ShouldBeFalse();
        }

        [Test, TestCaseSource(nameof(UnequalVertex))]
        public void ObjectEquals_ReturnsFalse_OnUnequalVertex(object other)
        {
            vertex.Equals(other).ShouldBeFalse();
        }

        [Test, TestCaseSource(nameof(EqualVertex))]
        public void TypeEquals_ReturnsTrue_OnEqualVertex(VertexPosition other)
        {
            vertex.Equals(other).ShouldBeTrue();
        }

        [Test, TestCaseSource(nameof(EqualVertex))]
        public void ObjectEquals_ReturnsTrue_OnEqualVertex(object other)
        {
            vertex.Equals(other).ShouldBeTrue();
        }

        [Test, TestCaseSource(nameof(EqualVertex))]
        public void EqualsOperator_ReturnsTrue_OnEqualVertex(VertexPosition other)
        {
            (vertex == other).ShouldBeTrue();
        }

        [Test, TestCaseSource(nameof(UnequalVertex))]
        public void EqualsOperator_ReturnsFalse_OnUnequalVertex(VertexPosition other)
        {
            (vertex == other).ShouldBeFalse();
        }

        [Test, TestCaseSource(nameof(UnequalVertex))]
        public void NotEqualsOperator_ReturnsTrue_OnUnequalVertex(VertexPosition other)
        {
            (vertex != other).ShouldBeTrue();
        }

        [Test, TestCaseSource(nameof(EqualVertex))]
        public void NotEqualsOperator_ReturnsFalse_OnEqualVertex(VertexPosition other)
        {
            (vertex != other).ShouldBeFalse();
        }

        [Test, TestCaseSource(nameof(EqualVertex))]
        public void HashCodes_ShouldBeEqual_ForEqualVertices(VertexPosition other)
        {
            vertex.GetHashCode().ShouldBe(other.GetHashCode());
        }

        [Test, TestCaseSource(nameof(UnequalVertex))]
        public void HashCodes_ShouldNotBeEqual_ForUnequalVertices(VertexPosition other)
        {
            vertex.GetHashCode().ShouldNotBe(other.GetHashCode());
        }

        [Test]
        public void ToString_Returns_ExpectedString()
        {
            string expected = $"[VertexPosition : Position={vertex.Position}]";
            vertex.ToString().ShouldBe(expected);
        }

        private IEnumerable<TestCaseData> UnequalVertex
        {
            get
            {
                yield return new TestCaseData(new VertexPosition(new Vector2(10, 5)));
            }
        }

        private IEnumerable<TestCaseData> EqualVertex
        {
            get
            {
                yield return new TestCaseData(new VertexPosition(position));
            }
        }
    }
}
