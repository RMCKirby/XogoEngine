using NUnit.Framework;
using Shouldly;
using OpenTK.Graphics.OpenGL4;
using XogoEngine.OpenGL.Vertex;

namespace XogoEngine.OpenGL.Test.Vertex
{
    [TestFixture]
    internal sealed class VertexElementTest
    {
        [Test]
        public void Constructor_CorrectlyInstantiates_Instance()
        {
            var element = new VertexElement(
                0,
                VertexElementUsage.Position,
                VertexAttribPointerType.Float,
                2,
                false
            );

            element.ShouldSatisfyAllConditions(
                () => element.Offset.ShouldBe(0),
                () => element.Usage.ShouldBe(VertexElementUsage.Position),
                () => element.PointerType.ShouldBe(VertexAttribPointerType.Float),
                () => element.NumberOfComponents.ShouldBe(2),
                () => element.Normalised.ShouldBe(false)
            );
        }
    }
}
