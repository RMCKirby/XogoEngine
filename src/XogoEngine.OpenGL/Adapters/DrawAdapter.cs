using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL4;
using System.Diagnostics.CodeAnalysis;

namespace XogoEngine.OpenGL.Adapters
{
    [ExcludeFromCodeCoverage]
    public sealed class DrawAdapter : IDrawAdapter
    {
        public void DrawArrays(PrimitiveType mode, int first, int count)
        {
            GraphicsContext.Assert();
            GL.DrawArrays(mode, first, count);
            OpenGlErrorHelper.CheckGlError();
        }

        public void DrawElements(BeginMode mode, int indiceCount, DrawElementsType elementType, int offset)
        {
            GraphicsContext.Assert();
            GL.DrawElements(mode, indiceCount, elementType, offset);
            OpenGlErrorHelper.CheckGlError();
        }
    }
}
