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
        public void Constructor_CorrectlyInstantiates_Handle()
        {
            shader.Handle.ShouldBe(1);
        }

        [Test]
        public void Constructor_CorrectlyInstantiates_ShaderType()
        {
            shader.ShaderType.ShouldBe(ShaderType.VertexShader);
        }

        [Test]
        public void Shader_isNotDisposed_AfterConstruction()
        {
            shader.IsDisposed.ShouldBeFalse();
        }
    }
}
