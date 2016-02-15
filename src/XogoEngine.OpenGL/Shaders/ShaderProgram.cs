using System;
using System.Collections.Generic;
using XogoEngine.OpenGL.Adapters;

namespace XogoEngine.OpenGL.Shaders
{
    public sealed class ShaderProgram : IResource<int>
    {
        private int handle;
        private readonly IShaderAdapter adapter;
        private bool isDisposed = false;

        public ShaderProgram(IShaderAdapter adapter, params Shader[] shaders)
        {
            this.adapter = adapter;
            this.handle = adapter.CreateProgram();
            AttachedShaders = shaders;
        }

        public int Handle { get { return handle; } }
        public IEnumerable<Shader> AttachedShaders { get; }
        public bool IsDisposed { get { return isDisposed; } }

        public void Attach(Shader shader)
        {
            if (isDisposed)
            {
                throw new ObjectDisposedException(GetType().FullName);
            }
        }

        public void Dispose()
        {
            if (isDisposed)
            {
                return;
            }
            adapter.DeleteProgram(handle);
            isDisposed = true;
            GC.SuppressFinalize(this);
        }
    }
}
