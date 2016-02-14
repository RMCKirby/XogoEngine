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

        public override string ToString()
        {
            return string.Format(
                "[Shader: Handle={0}, ShaderType={1}, IsDisposed={2}]",
                handle,
                ShaderType,
                isDisposed
            );
        }

        public void Dispose()
        {
            if (isDisposed)
            {
                return;
            }
            adapter.DeleteShader(handle);
            isDisposed = true;
            GC.SuppressFinalize(this);
        }
    }
}
