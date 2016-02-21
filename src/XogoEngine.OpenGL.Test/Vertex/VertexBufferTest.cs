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
            Action fill = () => buffer.Fill(new IntPtr(20), new int[] { 1 }, BufferUsageHint.StaticDraw);
            buffer.Dispose();

            fill.ShouldThrow<ObjectDisposedException>()
                .ObjectName.ShouldBe(buffer.GetType().FullName);
        }

        [Test]
        public void Fill_ThrowsArgumentException_OnZeroSize()
        {
            Action fill = () => buffer.Fill(IntPtr.Zero, new int[]{1}, BufferUsageHint.StaticDraw);
            fill.ShouldThrow<ArgumentException>().Message.ShouldContain(
                "The size allocated to the buffer must be greater than zero"
            );
        }

        [Test]
        public void BufferSize_IsStored_OnFill()
        {
            var size = new IntPtr(10);
            buffer.Fill(size, new int[] { 1 }, BufferUsageHint.StaticDraw);

            buffer.Size.ShouldBe(size);
        }

        [Test]
        public void AdapterBufferData_IsInvoked_OnFill()
        {
            int[] data = { 1, 2, 3, 4 };
            var size = new IntPtr(20);
            var usageHint = BufferUsageHint.DynamicDraw;

            buffer.Fill(size, data, usageHint);
            adapter.Verify(a => a.BufferData(buffer.Target, size, data, usageHint), Times.Once);
        }

        [Test]
        public void FillPartial_ThrowsObjectDisposedException_OnDisposedBuffer()
        {
            Action fillPartial = () => buffer.FillPartial(IntPtr.Zero, new IntPtr(10), new int[] { 1 });
            buffer.Dispose();

            fillPartial.ShouldThrow<ObjectDisposedException>()
                       .ObjectName.ShouldBe(buffer.GetType().FullName);
        }

        [Test]
        public void FillPartial_ThrowsUnallocatedBufferSizeException_WhenInvokedBeforeFill()
        {
            Action fillPartial = () => buffer.FillPartial(IntPtr.Zero, IntPtr.Zero, new int[] { 1 });

            fillPartial.ShouldThrow<UnallocatedBufferSizeException>().Message.ShouldContain(
                $"The size of the buffer has not yet been allocated. Have you called Fill?"
            );
        }

        [Test]
        public void FillPartial_ThrowsArgumentException_ForIllegalSize()
        {
            IntPtr bufferSize = new IntPtr(50);
            IntPtr partialSize = new IntPtr(51);
            Action fillPartial = () => buffer.FillPartial(partialSize, IntPtr.Zero, new int[] { 1 });

            buffer.Fill(bufferSize, new int[] { 1, 2 }, BufferUsageHint.DynamicDraw);
            /*fillPartial.ShouldThrow<ArgumentException>().Message.ShouldContain(
                $"given size of {partialSize} was larger than the allocated size of {bufferSize}"
            );*/
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
