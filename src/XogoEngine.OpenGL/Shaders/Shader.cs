using XogoEngine.OpenGL.Adapters;

namespace XogoEngine.OpenGL.Shaders
{
    public sealed class Shader
    {
        private int handle;
        private IShaderAdapter adapter;

        public Shader(IShaderAdapter adapter)
        {
            this.handle = adapter.CreateShader(OpenTK.Graphics.OpenGL4.ShaderType.VertexShader);
        }

        public int Handle { get { return handle; } }
    }
}
