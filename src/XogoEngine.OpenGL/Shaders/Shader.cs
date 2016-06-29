using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL4;
using System;
using XogoEngine.OpenGL.Adapters;
using XogoEngine.OpenGL.Extensions;
using XogoEngine.OpenGL.Utilities;

namespace XogoEngine.OpenGL.Shaders
{
    public sealed class Shader : IResource<int>, IEquatable<Shader>
    {
        private int handle;
        private readonly IShaderAdapter adapter;
        private bool isDisposed = false;

        public Shader(IShaderAdapter adapter, ShaderType shaderType)
        {
            if (adapter == null)
            {
                throw new ArgumentNullException(nameof(adapter));
            }
            this.handle = adapter.CreateShader(shaderType);
            this.ShaderType = shaderType;
            this.adapter = adapter;
        }

        public int Handle { get { return handle; } }
        public bool IsDisposed { get { return isDisposed; } }
        public ShaderType ShaderType { get; }

        public void Load(string source)
        {
            this.ThrowIfDisposed();
            if (string.IsNullOrEmpty(source) || string.IsNullOrWhiteSpace(source))
            {
                throw new ArgumentException("The given source string was null, empty or whitespace");
            }
            adapter.ShaderSource(handle, source);
            Compile();
        }

        private void Compile()
        {
            adapter.CompileShader(handle);
            bool compiled = adapter.GetShaderStatus(handle, ShaderParameter.CompileStatus);
            if (!compiled)
            {
                string reason = adapter.GetShaderInfoLog(handle);
                throw new ShaderCompilationException(
                    $"Failed to compile shader Id : {handle}, reason : {reason}"
                );
            }
        }

        public override bool Equals(object obj) => Equals(obj as Shader);

        public bool Equals(Shader other)
        {
            return other == null ? false :
                   handle == other.handle && ShaderType == other.ShaderType;
        }

        public override int GetHashCode()
            => HashCodeGenerator.Initialise().Hash(handle).Hash(ShaderType).Value;

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
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~Shader()
        {
            Dispose(false);
        }

        private void Dispose(bool manual)
        {
            if (isDisposed)
            {
                return;
            }
            if (manual)
            {
                adapter.DeleteShader(handle);
            }
            else
            {
                if (GraphicsContext.CurrentContext != null)
                {
                    GL.DeleteShader(handle);
                }
            }
            isDisposed = true;
        }
    }
}
