using Moq;
using NUnit.Framework;
using OpenTK.Graphics.OpenGL4;
using Shouldly;
using System;
using System.Collections.Generic;
using XogoEngine.OpenGL.Adapters;
using XogoEngine.OpenGL.Vertex;

namespace XogoEngine.OpenGL.Test.Vertex
{
    [TestFixture]
    internal sealed class VertexBufferTest
    {
        private struct Vertex : IVertexDeclarable
        {
            IVertexDeclaration IVertexDeclarable.Declaration
            {
                get { return null; }
            }
        }

        private Vertex[] vertices = new Vertex[]
        {
            new Vertex(),
            new Vertex()
        };

        private VertexBuffer<Vertex> buffer;
        private Mock<IBufferAdapter> adapter;

        [SetUp]
        public void SetUp()
        {
            adapter = new Mock<IBufferAdapter>();
            adapter.Setup(a => a.GenBuffer())
                   .Returns(1);
            buffer = new VertexBuffer<Vertex>(adapter.Object);
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
            Action fill = () => buffer.Fill(new IntPtr(20), vertices, BufferUsageHint.StaticDraw);
            buffer.Dispose();

            fill.ShouldThrow<ObjectDisposedException>()
                .ObjectName.ShouldBe(buffer.GetType().FullName);
        }

        [Test]
        public void Fill_ThrowsArgumentException_OnZeroSize()
        {
            Action fill = () => buffer.Fill(IntPtr.Zero, vertices, BufferUsageHint.StaticDraw);
            fill.ShouldThrow<ArgumentOutOfRangeException>().Message.ShouldContain(
                "The size allocated to the buffer must be greater than zero"
            );
        }

        [Test]
        public void BufferSize_IsStored_OnFill()
        {
            var size = new IntPtr(10);
            buffer.Fill(size, vertices, BufferUsageHint.StaticDraw);

            buffer.Size.ShouldBe(size);
        }

        [Test]
        public void AdapterBufferData_IsInvoked_OnFill()
        {
            var size = new IntPtr(20);
            var usageHint = BufferUsageHint.DynamicDraw;

            buffer.Fill(size, vertices, usageHint);
            adapter.Verify(a => a.BufferData(buffer.Target, size, vertices, usageHint), Times.Once);
        }

        [Test]
        public void FillPartial_ThrowsObjectDisposedException_OnDisposedBuffer()
        {
            Action fillPartial = () => buffer.FillPartial(IntPtr.Zero, new IntPtr(10), vertices);
            buffer.Dispose();

            fillPartial.ShouldThrow<ObjectDisposedException>()
                       .ObjectName.ShouldBe(buffer.GetType().FullName);
        }

        [Test]
        public void FillPartial_ThrowsUnallocatedBufferSizeException_WhenInvokedBeforeFill()
        {
            Action fillPartial = () => buffer.FillPartial(IntPtr.Zero, IntPtr.Zero, vertices);

            fillPartial.ShouldThrow<UnallocatedBufferSizeException>().Message.ShouldContain(
                $"The size of the buffer has not yet been allocated. Have you called Fill?"
            );
        }

        [Test, TestCaseSource(nameof(InvalidPartialInputs))]
        public void FillPartial_ThrowsArgumentOutOfRangeException_ForNegativeInputs(IntPtr offset, IntPtr size, string name)
        {
            FillBufferCorrectly();
            Action fillPartial = () => buffer.FillPartial(offset, size, vertices);
            fillPartial.ShouldThrow<ArgumentOutOfRangeException>()
                       .Message.ShouldContain($"{nameof(name)}");
        }

        private IEnumerable<TestCaseData> InvalidPartialInputs
        {
            get
            {
                yield return new TestCaseData(new IntPtr(-1), new IntPtr(1), "offset");
                yield return new TestCaseData(new IntPtr(1), new IntPtr(-1), "size");
            }
        }

        [Test]
        public void FillPartial_ThrowsArgumentOutOfRangeException_ForIllegalSize()
        {
            FillBufferCorrectly();
            IntPtr size = new IntPtr(100);
            IntPtr offset = new IntPtr(100);
            Action fillPartial = () => buffer.FillPartial(size, offset, vertices);

            fillPartial.ShouldThrow<ArgumentOutOfRangeException>().Message.ShouldContain(
                $"The given offset : {offset} and size : {size} were outside the buffer's size"
            );
        }

        [Test]
        public void AdapterBufferSubData_IsInvoked_OnFillPartial()
        {
            FillBufferCorrectly();
            FillPartialCorrectly();

            adapter.Verify(a => a.BufferSubData(
                buffer.Target, new IntPtr(4), new IntPtr(4), vertices),
                Times.Once
           );
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

        private void FillBufferCorrectly()
        {
            buffer.Fill(new IntPtr(10), vertices, BufferUsageHint.StaticDraw);
        }

        private void FillPartialCorrectly()
        {
            buffer.FillPartial(new IntPtr(4), new IntPtr(4), vertices);
        }
    }
}
