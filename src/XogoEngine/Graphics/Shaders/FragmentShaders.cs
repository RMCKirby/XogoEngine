namespace XogoEngine.Graphics.Shaders
{
    internal static class FragmentShaders
    {
        public static string Sprite => sprite;
        public static string Primitive => primitive;

        private static readonly string sprite =
        @"
#version 130

uniform sampler2D tex;

in vec4 passColour;
in vec2 passTexCoord;

out vec4 outColour;

void main()
{
    outColour = texture(tex, passTexCoord) * vec4(passColour);
}";

        private static readonly string primitive =
        @"
#version 130

in vec4 passColour;

out vec4 outColour;

void main()
{
    outColour = passColour;
}";
    }
}
