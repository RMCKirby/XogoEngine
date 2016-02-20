using NUnit.Framework;
using OpenTK.Graphics.OpenGL4;
using Shouldly;
using XogoEngine.OpenGL.Shaders;

namespace XogoEngine.OpenGL.Test.Shaders
{
    [TestFixture]
    internal sealed class ShaderUniformTest
    {
        [Test]
        public void Constructor_CorrectlyInitialises_Instance()
        {
            var uniform = new ShaderUniform("model", 0, 64, ActiveUniformType.FloatMat4);

            uniform.ShouldSatisfyAllConditions(
                () => uniform.Name.ShouldBe("model"),
                () => uniform.Location.ShouldBe(0),
                () => uniform.Size.ShouldBe(64),
                () => uniform.UniformType.ShouldBe(ActiveUniformType.FloatMat4)
            );
        }
    }
}
