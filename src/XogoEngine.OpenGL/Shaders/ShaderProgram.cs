using System;
using System.Linq;
using System.Collections.Generic;
using XogoEngine.OpenGL.Adapters;
using XogoEngine.OpenGL.Extensions;

namespace XogoEngine.OpenGL.Shaders
{
    public sealed class ShaderProgram : IResource<int>
    {
        private int handle;
        private readonly IShaderAdapter adapter;
        private IList<Shader> attachedShaders = new List<Shader>();
        private bool isDisposed = false;

        public ShaderProgram(IShaderAdapter adapter, params Shader[] shaders)
        {
            if (adapter == null)
            {
                throw new ArgumentNullException(nameof(adapter));
            }
            this.adapter = adapter;
            this.handle = adapter.CreateProgram();
            foreach (var shader in shaders)
            {
                Attach(shader);
            }
        }

        public int Handle { get { return handle; } }
        public IEnumerable<Shader> AttachedShaders { get { return attachedShaders; } }
        public bool IsDisposed { get { return isDisposed; } }

        public void Attach(Shader shader)
        {
            this.ThrowIfDisposed();
            shader.ThrowIfDisposed();
            if (!AttachedShaders.Contains(shader))
            {
                adapter.AttachShader(handle, shader.Handle);
                attachedShaders.Add(shader);
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
