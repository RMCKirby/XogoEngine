namespace XogoEngine.Graphics.Shaders
{
    internal static class VertexShaders
    {
        public static string Sprite => sprite;

        private static readonly string sprite =
        @"
#version 130

in vec2 position;
in vec4 colour;
in vec2 texCoord;

out vec4 passColour;
out vec2 passTexCoord;

uniform mat4 model;
uniform mat4 view;
uniform mat4 projection;

uniform mat4 mvp;

void main()
{
    gl_Position = mvp * vec4(position, 0.0, 1.0);
    passColour = colour;
    passTexCoord = texCoord;
}";
    }
}
