using OpenTK.Graphics.OpenGL4;
using System;
using XogoEngine.OpenGL.Adapters;

namespace XogoEngine.OpenGL.Shaders
{
    public sealed class Shader : IDisposable
    {
        private int handle;
        private readonly IShaderAdapter adapter;
        private bool isDisposed = false;

        public Shader(IShaderAdapter adapter, ShaderType shaderType)
        {
            this.handle = adapter.CreateShader(shaderType);
            this.ShaderType = shaderType;
            this.adapter = adapter;
        }

        public int Handle { get { return handle; } }
        public bool IsDisposed { get { return isDisposed; } }
        public ShaderType ShaderType { get; }

        public void Dispose()
        {
            adapter.DeleteShader(handle);
            isDisposed = true;
        }
    }
}
