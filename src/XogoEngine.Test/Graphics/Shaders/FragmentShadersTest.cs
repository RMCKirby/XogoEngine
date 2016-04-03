using NUnit.Framework;
using Shouldly;
using XogoEngine.Graphics.Shaders;

namespace XogoEngine.Test.Graphics.Shaders
{
    [TestFixture]
    internal sealed class FragmentShadersTest
    {
        [Test]
        public void SpriteFragmentShader_Returns_ExpectedGlslCode()
        {
            string expected = @"
#version 130

uniform sampler2D tex;

in vec4 passColour;
in vec2 passTexCoord;

out vec4 outColour;

void main()
{
    outColour = texture(tex, passTexCoord) * vec4(passColour);
}";

            FragmentShaders.Sprite.ShouldBe(expected);
        }
    }
}
