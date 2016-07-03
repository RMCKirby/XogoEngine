using NUnit.Framework;
using Shouldly;
using System.Linq;
using System.Collections.Generic;
using OpenTK;
using XogoEngine.OpenGL.Primitives;
using XogoEngine.OpenGL.Vertex;

namespace XogoEngine.OpenGL.Test.Primitives
{
	[TestFixture]
	internal sealed class LineTest
	{
        private Line line;
        private static VertexPositionColour vertex1;
        private static VertexPositionColour vertex2;

		static LineTest()
		{
            vertex1 = new VertexPositionColour(Vector2.Zero, Vector4.Zero);
            vertex2 = new VertexPositionColour(Vector2.One, Vector4.One);
        }

        [SetUp]
		public void SetUp()
		{
            line = new Line(vertex1, vertex2);
        }

		[Test]
		public void Constructor_CorrectlyInitialises_Instance()
		{
            line.ShouldSatisfyAllConditions(
                () => line.Start.ShouldBe(vertex1),
                () => line.End.ShouldBe(vertex2)
            );
        }

		[Test]
		public void Vertices_Return_ExpectedValues()
		{
            line.Vertices.Count().ShouldBe(2);
            line.Vertices.ShouldContain(vertex1);
            line.Vertices.ShouldContain(vertex2);
        }

		[Test]
		public void Stride_ShouldBeSize_OfVertices()
		{
            line.Stride.ShouldBe(default(VertexPositionColour).Declaration.Stride * 2);
        }

		[Test]
		public void ObjectEquals_ReturnsFalse_OnNullReference()
		{
            line.Equals(null).ShouldBeFalse();
        }

		[Test, TestCaseSource(nameof(UnequalLines))]
		public void ObjectEquals_ReturnsFalse_OnUnequalLine(object other)
		{
            line.Equals(other).ShouldBeFalse();
        }

		[Test, TestCaseSource(nameof(UnequalLines))]
		public void TypeEquals_ReturnsFalse_OnUnequalLine(Line other)
		{
            line.Equals(other).ShouldBeFalse();
        }

		[Test, TestCaseSource(nameof(EqualLine))]
		public void ObjectEquals_ReturnsTrue_OnEqualLine(object other)
		{
            line.Equals(other).ShouldBeTrue();
        }

		[Test, TestCaseSource(nameof(EqualLine))]
		public void TypeEquals_ReturnsTrue_OnEqualLine(Line other)
		{
            line.Equals(other).ShouldBeTrue();
        }

		private static IEnumerable<TestCaseData> UnequalLines
		{
			get
			{
                yield return new TestCaseData(new Line(
					vertex2,
					vertex1
				));
				yield return new TestCaseData(new Line(
					vertex1,
					new VertexPositionColour(Vector2.Zero, Vector4.UnitY)
				));
            }
		}

		private IEnumerable<TestCaseData> EqualLine
		{
			get
			{
                yield return new TestCaseData(new Line(
                    vertex1,
                    vertex2
                ));
            }
		}
    }
}
