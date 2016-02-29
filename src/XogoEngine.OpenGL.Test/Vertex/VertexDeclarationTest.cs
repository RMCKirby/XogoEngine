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
                new VertexElement(0, "position", VertexAttribPointerType.Float, 2, false),
                new VertexElement(8, "colour", VertexAttribPointerType.Float, 4, false)
            };
            vertexDeclaration = new VertexDeclaration(20, vertexElements);

            shaderProgram = new Mock<IShaderProgram>();
            adapter = new Mock<IVertexArrayAdapter>();

            shaderProgram.SetupGet(s => s.IsDisposed).Returns(false);
        }

        [Test]
        public void Constructor_CorrectlyInstantiates_instance()
        {
            vertexDeclaration.ShouldSatisfyAllConditions(
                () => vertexDeclaration.Stride.ShouldBe(20),
                () => vertexDeclaration.Elements.ShouldBe(vertexElements)
            );
        }

        [Test]
        public void VertexElementsProperty_ReturnsCopy_RatherThanActualArray()
        {
            vertexDeclaration.Elements.ShouldNotBeSameAs(vertexElements);
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

        [Test]
        public void Apply_ThrowsObjectDisposedException_OnDisposedShaderProgram()
        {
            shaderProgram.SetupGet(s => s.IsDisposed).Returns(true);
            Action apply = () => vertexDeclaration.Apply(adapter.Object, shaderProgram.Object);

            apply.ShouldThrow<ObjectDisposedException>()
                 .ObjectName.ShouldBe(shaderProgram.Object.GetType().FullName);
        }

        [Test]
        public void ShaderProgramGetAttributeLocation_isInvoked_ForEachElement()
        {
            vertexDeclaration.Apply(adapter.Object, shaderProgram.Object);
            foreach (var element in vertexDeclaration.Elements)
            {
                shaderProgram.Verify(s => s.GetAttributeLocation(element.Usage));
            }
        }

        [Test]
        public void AdapterEnableVertexAttribArray_IsInvokedForEachLocation_OnApply()
        {
            shaderProgram.Setup(s => s.GetAttributeLocation(vertexDeclaration.Elements[0].Usage))
                         .Returns(0);
            shaderProgram.Setup(s => s.GetAttributeLocation(vertexDeclaration.Elements[1].Usage))
                         .Returns(1);

            vertexDeclaration.Apply(adapter.Object, shaderProgram.Object);

            adapter.Verify(a => a.EnableVertexAttribArray(0));
            adapter.Verify(a => a.EnableVertexAttribArray(1));
        }

        [Test]
        public void AdapterVertexAttribPointer_IsInvokedForEachElement_OnApply()
        {
            var firstElement = vertexDeclaration.Elements[0];
            var secondElement = vertexDeclaration.Elements[1];
            shaderProgram.Setup(s => s.GetAttributeLocation(firstElement.Usage))
                         .Returns(0);
            shaderProgram.Setup(s => s.GetAttributeLocation(secondElement.Usage))
                         .Returns(1);

            vertexDeclaration.Apply(adapter.Object, shaderProgram.Object);

            AssertVertexAttribPointerInvocation(firstElement, 0);
            AssertVertexAttribPointerInvocation(secondElement, 1);
        }

        private void AssertVertexAttribPointerInvocation(VertexElement element, int location)
        {
            adapter.Verify(a => a.VertexAttribPointer(
                location,
                element.NumberOfComponents,
                element.PointerType,
                element.Normalised,
                vertexDeclaration.Stride,
                element.Offset
            ));
        }
    }
}
