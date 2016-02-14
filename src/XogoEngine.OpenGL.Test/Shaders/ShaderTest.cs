using Moq;
using NUnit.Framework;
using OpenTK.Graphics.OpenGL4;
using Shouldly;
using XogoEngine.OpenGL.Adapters;
using XogoEngine.OpenGL.Shaders;

namespace XogoEngine.OpenGL.Test.Shaders
{
    [TestFixture]
    internal sealed class ShaderTest
    {
        private Shader shader;
        private Mock<IShaderAdapter> adapter;

        [SetUp]
        public void SetUp()
        {
            adapter = new Mock<IShaderAdapter>();
            adapter.Setup(a => a.CreateShader(It.IsAny<ShaderType>()))
                   .Returns(1);

            shader = new Shader(adapter.Object, ShaderType.VertexShader);
        }

        [Test]
        public void Constructor_CorrectlyInstantiates_Instance()
        {
            shader.ShouldSatisfyAllConditions(
                () => shader.Handle.ShouldBe(1),
                () => shader.ShaderType.ShouldBe(ShaderType.VertexShader),
                () => shader.IsDisposed.ShouldBeFalse()
            );
        }

        [Test]
        public void Shader_ShouldBeDisposed_AfterDisposal()
        {
            shader.Dispose();
            shader.IsDisposed.ShouldBeTrue();
        }

        [Test]
        public void Adapter_ShouldDeleteHandleOnce_OnDisposal()
        {
            shader.Dispose();
            shader.Dispose();
            adapter.Verify(a => a.DeleteShader(shader.Handle), Times.Once);
        }

        [Test]
        public void Shader_ToString_ReturnsExpectedString()
        {
            string expected = string.Format(
                "[Shader: Handle={0}, ShaderType={1}, IsDisposed={2}]",
                shader.Handle,
                shader.ShaderType,
                shader.IsDisposed
            );
            expected.ShouldBe(shader.ToString());
        }
    }
}