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
            adapter.Setup(a => a.CreateProgram()).Returns(1);
            adapter.SetupSequence(a => a.CreateShader(It.IsAny<ShaderType>()))
                   .Returns(1)
                   .Returns(2);

            vertexShader = new Shader(adapter.Object, ShaderType.VertexShader);
            fragmentShader = new Shader(adapter.Object, ShaderType.FragmentShader);

            program = new ShaderProgram(adapter.Object, vertexShader);
        }

        [TearDown]
        public void TearDown()
        {
            program.Dispose();
        }

        [Test]
        public void Constructor_CorrectlyIntialises_Instance()
        {
            program.ShouldSatisfyAllConditions(
                () => program.Handle.ShouldBe(1),
                () => program.IsDisposed.ShouldBeFalse(),
                () => program.AttachedShaders.ShouldContain(vertexShader)
            );
        }

        [Test]
        public void Attach_ThrowsObjectDisposedException_OnDisposedProgram()
        {
            program.Dispose();
            Assert.Throws<ObjectDisposedException>(() => program.Attach(fragmentShader));
        }

        [Test]
        public void Shader_IsNotAttached_IfAlreadyAttached()
        {
            program.AttachedShaders.ShouldContain(vertexShader);
            program.AttachedShaders.Count().ShouldBe(1);
            //program.Attach(vertexShader);
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
