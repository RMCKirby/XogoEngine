using Moq;
using NUnit.Framework;
using Shouldly;
using System;
using XogoEngine.OpenGL.Adapters;
using XogoEngine.OpenGL.Shaders;
using XogoEngine.OpenGL.Vertex;

namespace XogoEngine.OpenGL.Test.Vertex
{
    [TestFixture]
    internal sealed class VertexArrayObjectTest
    {
        private VertexArrayObject vertexArray;
        private Mock<IVertexArrayAdapter> adapter;
        private Mock<IVertexDeclarable> vertex;
        private Mock<IShaderProgram> shaderProgram;

        [SetUp]
        public void SetUp()
        {
            adapter = new Mock<IVertexArrayAdapter>();
            vertex = new Mock<IVertexDeclarable>();
            adapter.Setup(a => a.GenVertexArray())
                   .Returns(1);
            vertex.SetupGet(v => v.Declaration)
                  .Returns(new VertexDeclaration(0, null));

            shaderProgram = new Mock<IShaderProgram>();

            vertexArray = new VertexArrayObject(adapter.Object);
        }

        [Test]
        public void Constructor_ThrowsArgumentNullException_OnNullAdapter()
        {
            Action construct = () => new VertexArrayObject(adapter: null);
            construct.ShouldThrow<ArgumentNullException>(nameof(adapter));
        }

        [Test]
        public void AdapterGenVertexArray_IsInvokedOnlyOnce_OnConstruction()
        {
            adapter.Verify(a => a.GenVertexArray(), Times.Once);
        }

        [Test]
        public void Instance_IsCorrectlySet_OnConstruction()
        {
            vertexArray.ShouldSatisfyAllConditions(
                () => vertexArray.Handle.ShouldBe(1),
                () => vertexArray.IsDisposed.ShouldBe(false)
            );
        }

        [Test]
        public void Bind_ThrowsObjectDisposedException_OnDisposedInstance()
        {
            Action bind = () => vertexArray.Bind();
            vertexArray.Dispose();

            AssertThrowsObjectDisposedException(bind);
        }

        [Test]
        public void AdapterBindVertexArray_IsInvoked_OnBind()
        {
            vertexArray.Bind();
            adapter.Verify(a => a.BindVertexArray(vertexArray.Handle), Times.Once);
        }

        [Test]
        public void SetUp_Throws_ObjectDisposedException_OnDisposedInstance()
        {
            Action setUp = () => vertexArray.SetUp(shaderProgram.Object, vertex.Object.Declaration);

            vertexArray.Dispose();
            AssertThrowsObjectDisposedException(setUp);
        }

        [Test]
        public void SetUp_ThrowsArgumentNullException_OnNullShaderProgram()
        {
            Action setUp = () => vertexArray.SetUp(null, vertex.Object.Declaration);
            setUp.ShouldThrow<ArgumentNullException>();
        }

        [Test]
        public void SetUp_ThrowsArgumentNullException_OnNullVertexDeclaration()
        {
            Action setUp = () => vertexArray.SetUp(shaderProgram.Object, null);
            setUp.ShouldThrow<ArgumentNullException>();
        }

        [Test]
        public void AdapterDeleteVertexArray_IsInvokedOnce_OnDisposal()
        {
            vertexArray.Dispose();
            vertexArray.Dispose();
            adapter.Verify(a => a.DeleteVertexArray(vertexArray.Handle), Times.Once);
        }

        [Test]
        public void Instance_ShouldBeDisposed_AfterDisposal()
        {
            vertexArray.Dispose();
            vertexArray.IsDisposed.ShouldBe(true);
        }

        private void AssertThrowsObjectDisposedException(Action action)
        {
            vertexArray.Dispose();
            action.ShouldThrow<ObjectDisposedException>()
                  .ObjectName.ShouldBe(vertexArray.GetType().FullName);
        }
    }
}
