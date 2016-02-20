using OpenTK.Graphics.OpenGL4;

namespace XogoEngine.OpenGL.Adapters
{
    public interface IBufferAdapter
    {
        int GenBuffer();

        void BindBuffer(BufferTarget target, int handle);
        void DeleteBuffer(int handle);
    }
}
