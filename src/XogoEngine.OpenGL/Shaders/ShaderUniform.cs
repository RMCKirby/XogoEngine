using OpenTK.Graphics.OpenGL4;

namespace XogoEngine.OpenGL.Shaders
{
    public sealed class ShaderUniform
    {
        public ShaderUniform(string name, int location, int size, ActiveUniformType uniformType)
        {
            Name = name;
            Location = location;
            Size = size;
            UniformType = uniformType;
        }

        public string Name { get; }
        public int Location { get; }
        public int Size { get; }
        public ActiveUniformType UniformType { get; }
    }
}