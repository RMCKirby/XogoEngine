using XogoEngine.OpenGL.Adapters;

namespace XogoEngine.OpenGL.Shaders
{
    public sealed class ShaderProgram : IResource<int>
    {
        private int handle;
        private readonly IShaderAdapter adapter;
        private bool isDisposed = false;

        public ShaderProgram(IShaderAdapter adapter)
        {
            this.adapter = adapter;
            handle = adapter.CreateProgram();
        }

        public int Handle { get { return handle; } }
        public bool IsDisposed { get { return isDisposed; } }

        public void Dispose()
        {
            isDisposed = true;
        }
    }
}
