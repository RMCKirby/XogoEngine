using Moq;
using NUnit.Framework;
using OpenTK.Graphics.OpenGL4;
using Shouldly;
using System;
using XogoEngine.OpenGL.Adapters;
using XogoEngine.OpenGL.Vertex;

namespace XogoEngine.OpenGL.Test.Vertex
{
    [TestFixture]
    internal sealed class VertexBufferTest
    {
        private VertexBuffer<int> buffer;
        private Mock<IBufferAdapter> adapter;

        [SetUp]
        public void SetUp()
        {
            adapter = new Mock<IBufferAdapter>();
            adapter.Setup(a => a.GenBuffer())
                   .Returns(1);
            buffer = new VertexBuffer<int>(adapter.Object);
        }

        [TearDown]
        public void TearDown()
        {
            buffer.Dispose();
        }

        [Test]
        public void AdapterGenBuffer_isInvokedOnlyOnce_OnConstruction()
        {
            adapter.Verify(a => a.GenBuffer(), Times.Once);
        }

        [Test]
        public void Constructor_CorrectlyInitialises_Instance()
        {
            buffer.ShouldSatisfyAllConditions(
                () => buffer.Handle.ShouldBe(1),
                () => buffer.IsDisposed.ShouldBe(false)
            );
        }

        [Test]
        public void BufferTarget_ShouldBeArrayBuffer_AfterConstruction()
        {
            buffer.Target.ShouldBe(BufferTarget.ArrayBuffer);
        }

        [Test]
        public void Bind_ThrowsObjectDisposedException_OnDisposedBuffer()
        {
            Action bind = () => buffer.Bind();
            buffer.Dispose();
            bind.ShouldThrow<ObjectDisposedException>()
                .ObjectName.ShouldBe(buffer.GetType().FullName);

        }

        [Test]
        public void AdapterBindBuffer_IsInvoked_OnBind()
        {
            buffer.Bind();
            adapter.Verify(a => a.BindBuffer(buffer.Target, buffer.Handle), Times.Once);
        }

        [Test]
        public void Fill_ThrowsObjectDisposedException_OnDisposedBuffer()
        {
            buffer.Dispose();
            //buffer.Fill(new IntPtr(100), new int[] { 0, 1 }, BufferUsageHint.DynamicDraw);
        }

        [Test]
        public void AdapterDeleteBuffer_IsInvokedOnce_OnDisposal()
        {
            buffer.Dispose();
            buffer.Dispose();
            adapter.Verify(a => a.DeleteBuffer(buffer.Handle), Times.Once);
        }

        [Test]
        public void Buffer_ShouldBeDisposed_AfterDisposal()
        {
            buffer.Dispose();
            buffer.IsDisposed.ShouldBeTrue();
        }
    }
}
