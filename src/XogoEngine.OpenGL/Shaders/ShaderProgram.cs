using System;
using System.Linq;
using System.Collections.Generic;
using OpenTK.Graphics.OpenGL4;
using XogoEngine.OpenGL.Adapters;
using XogoEngine.OpenGL.Extensions;

namespace XogoEngine.OpenGL.Shaders
{
    public sealed class ShaderProgram : IResource<int>
    {
        private int handle;
        private readonly IShaderAdapter adapter;
        private List<Shader> attachedShaders = new List<Shader>();
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
            ThrowIfNull(shader);
            shader.ThrowIfDisposed();
            if (!AttachedShaders.Contains(shader))
            {
                adapter.AttachShader(handle, shader.Handle);
                attachedShaders.Add(shader);
            }
        }

        public void Link()
        {
            this.ThrowIfDisposed();
            adapter.LinkProgram(handle);
            bool linkSuccessful = adapter.GetShaderProgramStatus(
                handle,
                GetProgramParameterName.LinkStatus
            );
            if (!linkSuccessful)
            {
                string info = adapter.GetProgramInfoLog(handle);
                throw new ShaderProgramLinkException(
                    $"Failed to link program Id : {handle}, Reason : {info}"
                );
            }
        }

        public void DetachShaders()
        {
            this.ThrowIfDisposed();
            attachedShaders.ForEach(
                (s) => adapter.DetachShader(handle, s.Handle)
            );
        }

        public void DeleteShaders()
        {
            this.ThrowIfDisposed();
            attachedShaders.ForEach((s) => s.Dispose());
        }

        public void Dispose()
        {
            if (isDisposed)
            {
                return;
            }
            attachedShaders.ForEach((s) => s.Dispose());
            adapter.DeleteProgram(handle);
            isDisposed = true;
            GC.SuppressFinalize(this);
        }

        private void ThrowIfNull(Shader shader)
        {
            if (shader == null)
            {
                throw new ArgumentNullException(nameof(shader));
            }
        }
    }
}
