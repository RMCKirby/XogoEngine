using Moq;
using NUnit.Framework;
using OpenTK.Graphics.OpenGL4;
using Shouldly;
using System;
using System.Linq;
using System.Collections.Generic;
using XogoEngine.OpenGL.Adapters;
using XogoEngine.OpenGL.Shaders;

namespace XogoEngine.OpenGL.Test.Shaders
{
    [TestFixture]
    internal sealed class ShaderProgramTest
    {
        private ShaderProgram program;
        private Shader vertexShader;
        private Shader fragmentShader;
        private Mock<IShaderAdapter> adapter;

        [SetUp]
        public void SetUp()
        {
            adapter = new Mock<IShaderAdapter>();
            adapter.SetupSequence(a => a.CreateProgram())
                   .Returns(1)
                   .Returns(2);
            adapter.SetupSequence(a => a.CreateShader(It.IsAny<ShaderType>()))
                   .Returns(1)
                   .Returns(2)
                   .Returns(3);

            vertexShader = new Shader(adapter.Object, ShaderType.VertexShader);
            fragmentShader = new Shader(adapter.Object, ShaderType.FragmentShader);

            program = new ShaderProgram(adapter.Object, vertexShader, fragmentShader);
        }

        [TearDown]
        public void TearDown()
        {
            program.Dispose();
        }

        [Test]
        public void Constructor_ThrowsNullArgumentException_OnNullAdapter()
        {
            IShaderAdapter nullAdapter = null;
            Action construct = () => program = new ShaderProgram(nullAdapter);

            construct.ShouldThrow<ArgumentNullException>();
        }

        [Test]
        public void Handle_IsInitialised_OnConstruction()
        {
            program.Handle.ShouldBe(1);
        }

        [Test]
        public void ShaderProgram_IsNotDisposed_AfterConstruction()
        {
            program.IsDisposed.ShouldBeFalse();
        }

        [Test]
        public void AdapterCreateProgram_IsInvoked_OnConstruction()
        {
            adapter.Verify(a => a.CreateProgram(), Times.Once);
        }

        [Test]
        public void AttachedShaders_Contain_ExpectedValues()
        {
            program.AttachedShaders.ShouldContain(vertexShader);
            program.AttachedShaders.ShouldContain(fragmentShader);
        }

        [Test]
        public void Attach_IsNotInvokedOnConstruction_WhenNoShadersAreSupplied()
        {
            var otherProgram = new ShaderProgram(adapter.Object);

            adapter.Verify(a => a.AttachShader(otherProgram.Handle, It.IsAny<int>()), Times.Never);
            otherProgram.AttachedShaders.ShouldNotBeNull();
            otherProgram.AttachedShaders.ShouldBeEmpty();
        }

        [Test]
        public void Attach_ThrowsObjectDisposedException_OnDisposedProgram()
        {
            program.Dispose();
            Assert.Throws<ObjectDisposedException>(() => program.Attach(fragmentShader));
        }

        [Test]
        public void AdapterAttachShader_isInvoked_OnNonAttachedShader()
        {
            var computeShader = new Shader(adapter.Object, ShaderType.ComputeShader);
            program.Attach(computeShader);
            adapter.Verify(a => a.AttachShader(program.Handle, computeShader.Handle), Times.Once);
        }

        [Test]
        public void AdapterAttachShader_IsNotInvoked_OnAlreadyAttachedShader()
        {
            program.Attach(vertexShader);
            adapter.Verify(a => a.AttachShader(program.Handle, vertexShader.Handle), Times.Once);
        }

        [Test]
        public void Program_isOnlyDisposedOnce_OnDisposal()
        {
            program.Dispose();
            program.Dispose();
            adapter.Verify(a => a.DeleteProgram(program.Handle), Times.Once);
        }

        [Test]
        public void Program_ShouldBeDisposed_AfterDisposal()
        {
            program.Dispose();
            program.IsDisposed.ShouldBeTrue();
        }
    }
}
