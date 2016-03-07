using OpenTK.Graphics.OpenGL4;

namespace XogoEngine.OpenGL.Adapters
{
    public interface IGladapter
    {
        void Clear(ClearBufferMask mask);
    }
}
