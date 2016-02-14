using OpenTK.Graphics.OpenGL4;
using XogoEngine.OpenGL.Adapters;

namespace XogoEngine.OpenGL.Shaders
{
    public sealed class Shader
    {
        private int handle;
        private IShaderAdapter adapter;
        private bool isDisposed = false;

        public Shader(IShaderAdapter adapter, ShaderType shaderType)
        {
            this.handle = adapter.CreateShader(OpenTK.Graphics.OpenGL4.ShaderType.VertexShader);
            this.ShaderType = shaderType;
        }

        public int Handle { get { return handle; } }
        public bool IsDisposed { get { return isDisposed; } }
        public ShaderType ShaderType { get; }
    }
}
