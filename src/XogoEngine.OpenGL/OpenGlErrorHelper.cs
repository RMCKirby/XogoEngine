using OpenTK.Graphics.OpenGL4;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;

namespace XogoEngine.OpenGL
{
    [ExcludeFromCodeCoverage]
    internal static class OpenGlErrorHelper
    {
        [Conditional("DEBUG")]
        public static void CheckGlError()
        {
            var error = GL.GetError();
            if (error != ErrorCode.NoError)
            {
                throw new OpenGlException($"OpenGL error : {error.ToString()}");
            }
        }
    }
}
