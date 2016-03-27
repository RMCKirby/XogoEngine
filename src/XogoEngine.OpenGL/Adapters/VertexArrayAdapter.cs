using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL4;
using System.Diagnostics.CodeAnalysis;

namespace XogoEngine.OpenGL.Adapters
{
    [ExcludeFromCodeCoverage]
    public sealed class VertexArrayAdapter : IVertexArrayAdapter
    {
        public int GenVertexArray()
        {
            GraphicsContext.Assert();
            int vao = GL.GenVertexArray();
            OpenGlErrorHelper.CheckGlError();
            return vao;
        }

        public void BindVertexArray(int handle)
        {
            GraphicsContext.Assert();
            GL.BindVertexArray(handle);
            OpenGlErrorHelper.CheckGlError();
        }

        public void DeleteVertexArray(int handle)
        {
            GraphicsContext.Assert();
            GL.DeleteVertexArray(handle);
            OpenGlErrorHelper.CheckGlError();
        }

        public void EnableVertexAttribArray(int location)
        {
            GraphicsContext.Assert();
            GL.EnableVertexAttribArray(location);
            OpenGlErrorHelper.CheckGlError();
        }

        public void VertexAttribPointer(
            int location,
            int numberOfComponents,
            VertexAttribPointerType type,
            bool normalised,
            int vertexStride,
            int offset)
        {
            GraphicsContext.Assert();
            GL.VertexAttribPointer(location, numberOfComponents, type, normalised, vertexStride, offset);
            OpenGlErrorHelper.CheckGlError();
        }
    }
}
