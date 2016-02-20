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
                   .Returns(2);
            adapter.Setup(a => a.GetShaderProgramStatus(
                    It.IsAny<int>(), GetProgramParameterName.LinkStatus))
                   .Returns(true);

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
        public void AdapterCreateProgram_IsInvoked_OnConstruction()
        {
            adapter.Verify(a => a.CreateProgram(), Times.Once);
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
        public void AttachedShaders_Contain_ExpectedValues()
        {
            program.AttachedShaders.ShouldContain(vertexShader);
            program.AttachedShaders.ShouldContain(fragmentShader);
        }

        [Test]
        public void ProgramUniforms_ShouldContain_ExpectedValues()
        {
            int attributeCount = 2;
            var positionAttribute = new ShaderAttribute("position", 0, 8, ActiveAttribType.FloatVec2);
            var colourAttribute = new ShaderAttribute("colour", 1, 16, ActiveAttribType.FloatVec4);

            adapter.Setup(a => a.GetProgram(program.Handle, GetProgramParameterName.ActiveAttributes))
                   .Returns(attributeCount);
            adapter.SetupSequence(a => a.GetActiveAttrib(program.Handle, It.IsAny<int>(), It.IsAny<int>()))
                   .Returns(positionAttribute)
                   .Returns(colourAttribute);

            program.Attributes.ShouldContainKeyAndValue(positionAttribute.Name, positionAttribute);
            program.Attributes.ShouldContainKeyAndValue(colourAttribute.Name, colourAttribute);
        }

        [Test]
        public void Link_ThrowsObjectDisposedException_OnDisposedProgram()
        {
            AssertThrowsDisposedException(() => program.Link(), program.GetType().FullName);
        }

        [Test]
        public void AdapterLinkProgram_isInvoked_OnLink()
        {
            program.Link();
            adapter.Verify(a => a.LinkProgram(program.Handle), Times.Once);
        }

        [Test]
        public void Link_ThrowsLinkException_OnFailureToLinkProgram()
        {
            Action link = () => program.Link();
            string errorMessage = "error: syntax error";
            adapter.Setup(a => a.GetShaderProgramStatus(program.Handle, GetProgramParameterName.LinkStatus))
                   .Returns(false);
            adapter.Setup(a => a.GetProgramInfoLog(program.Handle))
                   .Returns(errorMessage);

            link.ShouldThrow<ShaderProgramLinkException>()
                .Message.ShouldContain(
                    $"Failed to link program Id : {program.Handle}, Reason : {errorMessage}"
                );
        }

        [Test]
        public void Use_ThrowsObjectDisposedException_OnDisposedProgram()
        {
            AssertThrowsDisposedException(() => program.Use(), program.GetType().FullName);
        }

        [Test]
        public void AdapterUseProgram_isInvoked_OnUse()
        {
            program.Use();
            adapter.Verify(a => a.UseProgram(program.Handle), Times.Once);
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
            Action attach = () => program.Attach(fragmentShader);
            AssertThrowsDisposedException(attach, program.GetType().FullName);
        }

        [Test]
        public void Attach_ThrowsArgumentNullException_OnNullShader()
        {
            Assert.Throws<ArgumentNullException>(() => program.Attach(null));
        }

        [Test]
        public void Attach_ThrowsObjectDisposedException_OnDisposedShader()
        {
            vertexShader.Dispose();
            Action attach = () => program.Attach(vertexShader);

            attach.ShouldThrow<ObjectDisposedException>()
                  .ObjectName.ShouldContain(vertexShader.GetType().FullName);
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
        public void DetachShaders_ThrowsObjectDisposedException_OnDisposedProgram()
        {
            AssertThrowsDisposedException(() => program.DetachShaders(), program.GetType().FullName);
        }

        [Test]
        public void AdapterDetachShader_IsInvokedOnEachShader_OnDetach()
        {
            program.DetachShaders();
            adapter.Verify(a => a.DetachShader(program.Handle, vertexShader.Handle), Times.Once);
            adapter.Verify(a => a.DetachShader(program.Handle, fragmentShader.Handle), Times.Once);
        }

        [Test]
        public void DeleteShaders_ThrowsObjectDisposedException_OnDisposedProgram()
        {
            AssertThrowsDisposedException(() => program.DeleteShaders(), program.GetType().FullName);
        }

        [Test]
        public void DeleteShaders_DisposesOfAttachedShaders_OnInvocation()
        {
            program.DeleteShaders();
            program.AttachedShaders.ShouldAllBe(
                (s) => s.IsDisposed == true
            );
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

        [Test]
        public void AttachedShaders_ShouldBeDisposed_AfterDisposal()
        {
            program.Dispose();
            program.AttachedShaders.ShouldAllBe(
                (s) => s.IsDisposed == true
            );
        }

        private void AssertThrowsDisposedException(Action action, string objectName)
        {
            program.Dispose();
            action.ShouldThrow<ObjectDisposedException>()
                  .ObjectName.ShouldBe(objectName);
        }
    }
}
