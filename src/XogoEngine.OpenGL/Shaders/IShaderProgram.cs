using System.Collections.Generic;

namespace XogoEngine.OpenGL.Shaders
{
    public interface IShaderProgram
    {
        IDictionary<string, ShaderAttribute> Attributes { get; }
        IDictionary<string, ShaderUniform> Uniforms { get; }
        bool Linked { get; }

        void Attach(Shader shader);
        void Link();

        int GetAttributeLocation(string name);
        int GetUniformLocation(string name);

        void Use();
        void DetachShaders();
        void DeleteShaders();
    }
}
