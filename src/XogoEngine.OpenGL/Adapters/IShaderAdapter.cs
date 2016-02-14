using OpenTK.Graphics.OpenGL4;

namespace XogoEngine.OpenGL.Adapters
{
    public interface IShaderAdapter
    {
        int CreateShader(ShaderType shaderType);
    }
}