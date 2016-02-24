using System;
using System.Linq;
using System.Collections.Generic;
using OpenTK.Graphics.OpenGL4;
using XogoEngine.OpenGL.Adapters;
using XogoEngine.OpenGL.Extensions;

namespace XogoEngine.OpenGL.Shaders
{
    using AttribDictionary = Dictionary<string, ShaderAttribute>;
    using UniformDictionary = Dictionary<string, ShaderUniform>;

    public sealed class ShaderProgram : IResource<int>
    {
        private int handle;
        private readonly IShaderAdapter adapter;

        private List<Shader> attachedShaders = new List<Shader>();
        private IDictionary<string, ShaderAttribute> attributes = new AttribDictionary();
        private IDictionary<string, ShaderUniform> uniforms = new UniformDictionary();
        private bool linked = false;
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
        public IDictionary<string, ShaderAttribute> Attributes { get { return attributes; } }
        public IDictionary<string, ShaderUniform> Uniforms { get { return uniforms; } }
        public bool Linked { get { return linked; } }
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
            linked = true;
        }

        public int GetAttributeLocation(string name)
        {
            if (string.IsNullOrWhiteSpace(name) || string.IsNullOrEmpty(name))
            {
                throw new ArgumentException(nameof(name) + " was null, empty or whitespace");
            }
            ThrowIfNotLinked();
            if (attributes.ContainsKey(name))
            {
                return attributes[name].Location;
            }
            int location = adapter.GetAttribLocation(handle, name);
            if (location == -1)
            {
                throw new ShaderAttributeNotFoundException(
                    $"Attribute name : {name} could not be found for shader program Id : {handle}"
                );
            }
            /* Currently bufferSize is hard-coded as 200
             * in the future we may want to query for GL_ACTIVE_ATTRIBUTE_MAX_LENGTH
             * and use its result instead */
            var attribute = adapter.GetActiveAttrib(handle, location, 200);
            attributes.Add(attribute.Name, attribute);
            return location;
        }

        public void Use()
        {
            this.ThrowIfDisposed();
            adapter.UseProgram(handle);
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

        private void ThrowIfNotLinked()
        {
            if (!linked)
            {
                throw new ProgramNotLinkedException(
                    $"Shader program Id : {handle} has not been linked. Have you called Link?"
                );
            }
        }
    }
}
