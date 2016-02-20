using NUnit.Framework;
using Shouldly;
using OpenTK.Graphics.OpenGL4;
using XogoEngine.OpenGL.Shaders;

namespace XogoEngine.OpenGL.Test.Shaders
{
    [TestFixture]
    internal sealed class ShaderAttributeTest
    {
        [Test]
        public void Constructor_CorrectlyInitialises_Instance()
        {
            var attribute = new ShaderAttribute("position", 0, 8, ActiveAttribType.FloatVec2);

            attribute.ShouldSatisfyAllConditions(
                () => attribute.Name.ShouldBe("position"),
                () => attribute.Location.ShouldBe(0),
                () => attribute.Size.ShouldBe(8),
                () => attribute.AttribType.ShouldBe(ActiveAttribType.FloatVec2)
            );
        }
    }
}
