using System.Collections.Generic;
using OpenTK;

namespace XogoEngine.OpenGL.Shaders
{
    public interface IShaderProgram : IResource<int>
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

        void SetMatrix4(ShaderUniform uniform, ref Matrix4 matrix, bool transpose);
    }
}
