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
    }
}
