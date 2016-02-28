using Moq;
using NUnit.Framework;
using Shouldly;
using System;
using System.Collections.Generic;
using OpenTK.Graphics.OpenGL4;
using XogoEngine.OpenGL.Adapters;
using XogoEngine.OpenGL.Shaders;
using XogoEngine.OpenGL.Vertex;

namespace XogoEngine.OpenGL.Test.Vertex
{
    [TestFixture]
    internal sealed class VertexDeclarationTest
    {
        private VertexDeclaration vertexDeclaration;
        private VertexElement[] vertexElements;
        private Mock<IShaderProgram> shaderProgram;
        private Mock<IVertexArrayAdapter> adapter;

        [SetUp]
        public void SetUp()
        {
            vertexElements = new VertexElement[]
            {
                new VertexElement(0, "position", VertexAttribPointerType.Float, 2, false)
            };
            vertexDeclaration = new VertexDeclaration(20, vertexElements);

            shaderProgram = new Mock<IShaderProgram>();
            adapter = new Mock<IVertexArrayAdapter>();
        }

        [Test]
        public void Constructor_CorrectlyInstantiates_instance()
        {
            vertexDeclaration.ShouldSatisfyAllConditions(
                () => vertexDeclaration.Stride.ShouldBe(20),
                () => vertexDeclaration.Elements.ShouldBe(vertexElements)
            );
        }

        [Test, TestCaseSource(nameof(ApplyNullArguments))]
        public void Apply_ThrowsArgumentNullException_OnNullArguments(
            IVertexArrayAdapter adapter,
            IShaderProgram shaderProgram)
        {
            Action apply = () => vertexDeclaration.Apply(adapter, shaderProgram);
            apply.ShouldThrow<ArgumentNullException>();
        }

        private IEnumerable<TestCaseData> ApplyNullArguments
        {
            get
            {
                yield return new TestCaseData(null, shaderProgram);
                yield return new TestCaseData(adapter, null);
            }
        }
    }
}
