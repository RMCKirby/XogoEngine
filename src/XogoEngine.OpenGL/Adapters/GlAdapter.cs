using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL4;
using System;
using System.Diagnostics.CodeAnalysis;
using XogoEngine.OpenGL.Shaders;

namespace XogoEngine.OpenGL.Adapters
{
    [ExcludeFromCodeCoverage]
    public sealed class GlAdapter : IGlAdapter
    {
        private readonly IBufferAdapter bufferAdapter = new BufferAdapter();
        private readonly IDrawAdapter drawAdapter = new DrawAdapter();
        private readonly IShaderAdapter shaderAdapter = new ShaderAdapter();
        private readonly ITextureAdapter textureAdapter = new TextureAdapter();
        private readonly IVertexArrayAdapter vertexAdapter = new VertexArrayAdapter();

        public void Clear(ClearBufferMask mask)
        {
            GraphicsContext.Assert();
            GL.Clear(mask);
            OpenGlErrorHelper.CheckGlError();
        }

        public void ClearColor(float red, float green, float blue, float alpha)
        {
            GraphicsContext.Assert();
            GL.ClearColor(red, green, blue, alpha);
            OpenGlErrorHelper.CheckGlError();
        }

        public int GenBuffer()
            => bufferAdapter.GenBuffer();
        public void DeleteBuffer(int handle)
            => bufferAdapter.DeleteBuffer(handle);
        public void BindBuffer(BufferTarget target, int handle)
            => bufferAdapter.BindBuffer(target, handle);
        public void BufferData<T>(BufferTarget target, IntPtr size, T[] data, BufferUsageHint usage) where T : struct
            => bufferAdapter.BufferData(target, size, data, usage);
        public void BufferSubData<T>(BufferTarget target, IntPtr offset, IntPtr size, T[] data) where T : struct
            => bufferAdapter.BufferSubData(target, offset, size, data);

        public void DrawArrays(PrimitiveType type, int first, int count)
            => drawAdapter.DrawArrays(type, first, count);
        public void DrawElements(BeginMode mode, int indiceCount, DrawElementsType elementType, int offset)
            => drawAdapter.DrawElements(mode, indiceCount, elementType, offset);

        public int CreateShader(ShaderType shaderType)
            => shaderAdapter.CreateShader(shaderType);
        public int CreateProgram()
            => shaderAdapter.CreateProgram();
        public void DeleteShader(int handle)
            => shaderAdapter.DeleteShader(handle);
        public void DeleteProgram(int handle)
            => shaderAdapter.DeleteProgram(handle);
        public void ShaderSource(int handle, string source)
            => shaderAdapter.ShaderSource(handle, source);
        public void CompileShader(int handle)
            => shaderAdapter.CompileShader(handle);
        public void LinkProgram(int handle)
            => shaderAdapter.LinkProgram(handle);
        public void UseProgram(int handle)
            => shaderAdapter.UseProgram(handle);
        public void AttachShader(int programHandle, int shaderHandle)
            => shaderAdapter.AttachShader(programHandle, shaderHandle);
        public void DetachShader(int programHandle, int shaderHandle)
            => shaderAdapter.DetachShader(programHandle, shaderHandle);
        public int GetProgram(int handle, GetProgramParameterName pname)
            => shaderAdapter.GetProgram(handle, pname);
        public bool GetShaderStatus(int handle, ShaderParameter pname)
            => shaderAdapter.GetShaderStatus(handle, pname);
        public bool GetShaderProgramStatus(int handle, GetProgramParameterName pname)
            => shaderAdapter.GetShaderProgramStatus(handle, pname);
        public string GetShaderInfoLog(int handle)
            => shaderAdapter.GetShaderInfoLog(handle);
        public string GetProgramInfoLog(int handle)
            => shaderAdapter.GetProgramInfoLog(handle);
        public int GetAttribLocation(int handle, string name)
            => shaderAdapter.GetAttribLocation(handle, name);
        public int GetUniformLocation(int handle, string name)
            => shaderAdapter.GetUniformLocation(handle, name);
        public ShaderAttribute GetActiveAttrib(int handle, int index, int bufferSize)
            => shaderAdapter.GetActiveAttrib(handle, index, bufferSize);
        public ShaderUniform GetActiveUniform(int handle, int index, int bufferSize)
            => shaderAdapter.GetActiveUniform(handle, index, bufferSize);
        public void UniformMatrix4(int location, bool transpose, ref Matrix4 matrix)
            => shaderAdapter.UniformMatrix4(location, transpose, ref matrix);

        public int GenTexture()
            => textureAdapter.GenTexture();
        public void Bind(TextureTarget target, int handle)
            => textureAdapter.Bind(target, handle);
        public void DeleteTexture(int handle)
            => textureAdapter.DeleteTexture(handle);
        public void TexParameter(TextureTarget target, TextureParameterName pname, int param)
            => textureAdapter.TexParameter(target, pname, param);
        public void TexImage2D(
            TextureTarget target,
            int level,
            PixelInternalFormat internalFormat,
            int width,
            int height,
            int border,
            PixelFormat format,
            PixelType type,
            IntPtr pixels)
            => textureAdapter.TexImage2D(target, level, internalFormat, width, height, border, format, type, pixels);

        public int GenVertexArray()
            => vertexAdapter.GenVertexArray();
        public void BindVertexArray(int handle)
            => vertexAdapter.BindVertexArray(handle);
        public void DeleteVertexArray(int handle)
            => vertexAdapter.DeleteVertexArray(handle);
        public void EnableVertexAttribArray(int location)
            => vertexAdapter.EnableVertexAttribArray(location);
        public void VertexAttribPointer(
            int location,
            int numberOfComponents,
            VertexAttribPointerType type,
            bool normalised,
            int vertexStride,
            int offset)
            => vertexAdapter.VertexAttribPointer(location, numberOfComponents, type, normalised, vertexStride, offset);
    }
}
