using OpenTK.Graphics.OpenGL4;

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
        void AttachShader(int programHandle, int shaderHandle);

        bool GetShaderStatus(int handle, ShaderParameter pname);

        string GetShaderInfoLog(int handle);
    }
}