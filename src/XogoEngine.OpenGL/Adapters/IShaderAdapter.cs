using OpenTK.Graphics.OpenGL4;
using XogoEngine.OpenGL.Shaders;

namespace XogoEngine.OpenGL.Adapters
{
    public interface IShaderAdapter
    {
        int CreateShader(ShaderType shaderType);
        int CreateProgram();

        void DeleteShader(int handle);
        void DeleteProgram(int handle);

        void ShaderSource(int handle, string source);
        void CompileShader(int handle);
        void LinkProgram(int handle);

        void UseProgram(int handle);

        void AttachShader(int programHandle, int shaderHandle);
        void DetachShader(int programHandle, int shaderHandle);

        int GetProgram(int handle, GetProgramParameterName pname);

        bool GetShaderStatus(int handle, ShaderParameter pname);
        bool GetShaderProgramStatus(int handle, GetProgramParameterName pname);

        string GetShaderInfoLog(int handle);
        string GetProgramInfoLog(int handle);

        ShaderAttribute GetActiveAttrib(int handle, int index, int bufferSize);
        ShaderUniform GetActiveUniform(int handle, int index, int bufferSize);
    }
}