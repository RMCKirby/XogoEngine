using NUnit.Framework;
using Shouldly;
using XogoEngine.Graphics.Shaders;

namespace XogoEngine.Test.Graphics.Shaders
{
    [TestFixture]
    internal sealed class VertexShadersTest
    {
        [Test]
        public void SpriteVertexShader_Returns_ExpectedGlslCode()
        {
            string expected = @"
#version 130

in vec2 position;
in vec4 colour;
in vec2 texCoord;

out vec4 passColour;
out vec2 passTexCoord;

uniform mat4 mvp;

void main()
{
    gl_Position = mvp * vec4(position, 0.0, 1.0);
    passColour = colour;
    passTexCoord = texCoord;
}";

            VertexShaders.Sprite.ShouldBe(expected);
        }
    }
}
