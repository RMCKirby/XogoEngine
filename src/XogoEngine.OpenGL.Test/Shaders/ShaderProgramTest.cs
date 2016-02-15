using Moq;
using NUnit.Framework;
using Shouldly;
using XogoEngine.OpenGL.Adapters;
using XogoEngine.OpenGL.Shaders;

namespace XogoEngine.OpenGL.Test.Shaders
{
    [TestFixture]
    internal sealed class ShaderProgramTest
    {
        private ShaderProgram program;
        private Mock<IShaderAdapter> adapter;

        [SetUp]
        public void SetUp()
        {
            adapter = new Mock<IShaderAdapter>();
            adapter.Setup(a => a.CreateProgram()).Returns(1);

            program = new ShaderProgram(adapter.Object);
        }

        [Test]
        public void Constructor_CorrectlyIntialises_Instance()
        {
            program.ShouldSatisfyAllConditions(
                () => program.Handle.ShouldBe(1),
                () => program.IsDisposed.ShouldBeFalse()
            );
        }

        [Test]
        public void Program_ShouldBeDisposed_AfterDisposal()
        {
            program.Dispose();
            program.IsDisposed.ShouldBeTrue();
        }
    }
}
