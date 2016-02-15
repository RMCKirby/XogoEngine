using OpenTK.Graphics.OpenGL4;

namespace XogoEngine.OpenGL.Adapters
{
    public interface IShaderAdapter
    {
        int CreateShader(ShaderType shaderType);
        int CreateProgram();

        void ShaderSource(int handle, string source);
        void CompileShader(int handle);
        void DeleteShader(int handle);

        bool GetShaderStatus(int handle, ShaderParameter pname);

        string GetShaderInfoLog(int handle);
    }
}