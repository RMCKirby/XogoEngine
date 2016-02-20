using OpenTK.Graphics.OpenGL4;

namespace XogoEngine.OpenGL.Shaders
{
    public sealed class ShaderAttribute
    {
        public ShaderAttribute(string name, int location, int size, ActiveAttribType attribType)
        {
            Name = name;
            Location = location;
            Size = size;
            AttribType = attribType;
        }

        public string Name { get; }
        public int Location { get; }
        public int Size { get; }
        public ActiveAttribType AttribType { get; }
    }
}
