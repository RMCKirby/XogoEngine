using OpenTK.Graphics;
using OpenTK;
using OpenTK.Graphics.OpenGL4;
using System.Text;
using System.Diagnostics.CodeAnalysis;
using XogoEngine.OpenGL.Shaders;

namespace XogoEngine.OpenGL.Adapters
{
    [ExcludeFromCodeCoverage]
    public sealed class ShaderAdapter : IShaderAdapter
    {
        public int CreateShader(ShaderType shaderType)
        {
            GraphicsContext.Assert();
            int shader = GL.CreateShader(shaderType);
            OpenGlErrorHelper.CheckGlError();
            return shader;
        }

        public int CreateProgram()
        {
            GraphicsContext.Assert();
            int program = GL.CreateProgram();
            OpenGlErrorHelper.CheckGlError();
            return program;
        }

        public void DeleteShader(int handle)
        {
            GraphicsContext.Assert();
            GL.DeleteShader(handle);
            OpenGlErrorHelper.CheckGlError();
        }

        public void DeleteProgram(int handle)
        {
            GraphicsContext.Assert();
            GL.DeleteProgram(handle);
            OpenGlErrorHelper.CheckGlError();
        }

        public void ShaderSource(int handle, string source)
        {
            GraphicsContext.Assert();
            GL.ShaderSource(handle, source);
            OpenGlErrorHelper.CheckGlError();
        }

        public void CompileShader(int handle)
        {
            GraphicsContext.Assert();
            GL.CompileShader(handle);
            OpenGlErrorHelper.CheckGlError();
        }

        public void LinkProgram(int handle)
        {
            GraphicsContext.Assert();
            GL.LinkProgram(handle);
            OpenGlErrorHelper.CheckGlError();
        }

        public void UseProgram(int handle)
        {
            GraphicsContext.Assert();
            GL.UseProgram(handle);
            OpenGlErrorHelper.CheckGlError();
        }

        public void AttachShader(int programHandle, int shaderHandle)
        {
            GraphicsContext.Assert();
            GL.AttachShader(programHandle, shaderHandle);
            OpenGlErrorHelper.CheckGlError();
        }

        public void DetachShader(int programHandle, int shaderHandle)
        {
            GraphicsContext.Assert();
            GL.DetachShader(programHandle, shaderHandle);
            OpenGlErrorHelper.CheckGlError();
        }

        public int GetProgram(int handle, GetProgramParameterName pname)
        {
            GraphicsContext.Assert();
            int @params;
            GL.GetProgram(handle, pname, out @params);
            OpenGlErrorHelper.CheckGlError();
            return @params;
        }

        public bool GetShaderStatus(int handle, ShaderParameter pname)
        {
            GraphicsContext.Assert();
            int @params;
            GL.GetShader(handle, pname, out @params);
            OpenGlErrorHelper.CheckGlError();

            return @params == 0 ? false : true;
        }

        public bool GetShaderProgramStatus(int handle, GetProgramParameterName pname)
        {
            GraphicsContext.Assert();
            int @params;
            GL.GetProgram(handle, pname, out @params);
            OpenGlErrorHelper.CheckGlError();

            return @params == 0 ? false : true;
        }

        public string GetShaderInfoLog(int handle)
        {
            GraphicsContext.Assert();
            string infoLog = string.Empty;
            GL.GetShaderInfoLog(handle, out infoLog);
            OpenGlErrorHelper.CheckGlError();
            return infoLog;
        }

        public string GetProgramInfoLog(int handle)
        {
            GraphicsContext.Assert();
            string infoLog = string.Empty;
            GL.GetProgramInfoLog(handle, out infoLog);
            OpenGlErrorHelper.CheckGlError();
            return infoLog;
        }

        public int GetAttribLocation(int handle, string name)
        {
            GraphicsContext.Assert();
            int location = GL.GetAttribLocation(handle, name);
            OpenGlErrorHelper.CheckGlError();
            return location;
        }

        public int GetUniformLocation(int handle, string name)
        {
            GraphicsContext.Assert();
            int location = GL.GetUniformLocation(handle, name);
            OpenGlErrorHelper.CheckGlError();
            return location;
        }

        public ShaderAttribute GetActiveAttrib(int handle, int index, int bufferSize)
        {
            GraphicsContext.Assert();
            var name = new StringBuilder();
            int length;
            int size;
            ActiveAttribType type;
            GL.GetActiveAttrib(handle, index, bufferSize, out length, out size, out type, name);
            OpenGlErrorHelper.CheckGlError();

            return new ShaderAttribute(name.ToString(), index, size, type);
        }

        public ShaderUniform GetActiveUniform(int handle, int index, int bufferSize)
        {
            GraphicsContext.Assert();
            var name = new StringBuilder();
            int length;
            int size;
            ActiveUniformType type;
            GL.GetActiveUniform(handle, index, bufferSize, out length, out size, out type, name);
            OpenGlErrorHelper.CheckGlError();

            return new ShaderUniform(name.ToString(), index, size, type);
        }

        public void UniformMatrix4(int location, bool transpose, ref Matrix4 matrix)
        {
            GraphicsContext.Assert();
            GL.UniformMatrix4(location, transpose, ref matrix);
            OpenGlErrorHelper.CheckGlError();
        }
    }
}