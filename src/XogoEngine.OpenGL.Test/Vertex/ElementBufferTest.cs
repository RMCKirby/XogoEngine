using Moq;
using NUnit.Framework;
using Shouldly;
using System;
using System.Collections.Generic;
using OpenTK.Graphics.OpenGL4;
using XogoEngine.OpenGL.Adapters;
using XogoEngine.OpenGL.Vertex;

namespace XogoEngine.OpenGL.Test.Vertex
{
    [TestFixture]
    internal sealed class ElementBufferTest
    {
        private ElementBuffer<ushort> elementBuffer;
        private Mock<IBufferAdapter> adapter;

        private ushort[] indices = new ushort[]
        {
            0,1,2,
            2,1,0
        };

        [SetUp]
        public void SetUp()
        {
            adapter = new Mock<IBufferAdapter>();
            adapter.Setup(a => a.GenBuffer())
                   .Returns(1);

            elementBuffer = new ElementBuffer<ushort>(adapter.Object);
        }

        [Test]
        public void Constructor_CorrectlyInitialises_Instance()
        {
            elementBuffer.ShouldSatisfyAllConditions(
                () => elementBuffer.Handle.ShouldBe(1),
                () => elementBuffer.IsDisposed.ShouldBeFalse(),
                () => elementBuffer.Target.ShouldBe(BufferTarget.ElementArrayBuffer)
            );
        }

        [Test]
        public void AdapterGenBuffer_IsInvoked_OnConstruction()
        {
            adapter.Verify(a => a.GenBuffer(), Times.Once);
        }

        [Test]
        public void Bind_ThrowsObjectDisposedException_OnDisposedBuffer()
        {
            Action bind = () => elementBuffer.Bind();
            elementBuffer.Dispose();

            bind.ShouldThrow<ObjectDisposedException>()
                .ObjectName.ShouldBe(elementBuffer.GetType().FullName);
        }

        [Test]
        public void AdapterBindBuffer_isInvoked_OnBind()
        {
            elementBuffer.Bind();
            adapter.Verify(a => a.BindBuffer(elementBuffer.Target, elementBuffer.Handle), Times.Once);
        }

        [Test]
        public void Fill_ThrowsObjectDisposedException_OnDisposedBuffer()
        {
            Action fill = () => elementBuffer.Fill(new IntPtr(10), indices, BufferUsageHint.StaticDraw);
            elementBuffer.Dispose();

            fill.ShouldThrow<ObjectDisposedException>()
                .ObjectName.ShouldBe(elementBuffer.GetType().FullName);
        }

        [Test, TestCaseSource(nameof(InvalidSizes))]
        public void Fill_ThrowsArgumentOutOfRangeException_OnInvalidSizes(IntPtr size)
        {
            Action fill = () => elementBuffer.Fill(size, indices, BufferUsageHint.StaticDraw);
            fill.ShouldThrow<ArgumentOutOfRangeException>().Message.ShouldContain(
                "The size allocated to the buffer must be greater than zero"
            );
        }

        private IEnumerable<TestCaseData> InvalidSizes
        {
            get
            {
                yield return new TestCaseData(IntPtr.Zero);
                yield return new TestCaseData(new IntPtr(0));
                yield return new TestCaseData(new IntPtr(-1));
            }
        }

        [Test]
        public void BufferSize_isStored_OnFill()
        {
            IntPtr expectedSize = new IntPtr(10);
            elementBuffer.Fill(expectedSize, indices, BufferUsageHint.StaticDraw);

            elementBuffer.Size.ShouldBe(expectedSize);
        }

        [Test]
        public void AdapterBufferData_isInvoked_OnFill()
        {
            elementBuffer.Fill(new IntPtr(10), indices, BufferUsageHint.StaticDraw);
            adapter.Verify(a => a.BufferData(
                elementBuffer.Target, elementBuffer.Size, indices, BufferUsageHint.StaticDraw),
                Times.Once
            );
        }

        [Test]
        public void AdapterDeleteBuffer_IsInvokedOnlyOnce_OnDisposal()
        {
            elementBuffer.Dispose();
            elementBuffer.Dispose();
            adapter.Verify(a => a.DeleteBuffer(elementBuffer.Handle), Times.Once);
        }

        [Test]
        public void Buffer_isDisposed_AfterDisposal()
        {
            elementBuffer.Dispose();
            elementBuffer.IsDisposed.ShouldBeTrue();
        }
    }
}
