using NUnit.Framework;
using Ploeh.AutoFixture;
using Shouldly;
using XogoEngine.OpenGL.Shaders;

namespace XogoEngine.OpenGL.Test.Shaders
{
    [TestFixture]
    internal sealed class ShaderTest
    {
        private Shader shader;

        [SetUp]
        public void SetUp()
        {
            shader = new Shader();
        }

        [Test]
        public void Constructor_CorrectlyInstantiates_Handle()
        {
            shader.Handle.ShouldBe(1);
        }
    }
}
