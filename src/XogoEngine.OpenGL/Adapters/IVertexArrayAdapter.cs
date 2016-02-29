using OpenTK.Graphics.OpenGL4;

namespace XogoEngine.OpenGL.Adapters
{
    public interface IVertexArrayAdapter
    {
        int GenVertexArray();

        void BindVertexArray(int handle);
        void DeleteVertexArray(int handle);

        void EnableVertexAttribArray(int location);

        void VertexAttribPointer(
            int location,
            int numberOfComponents,
            VertexAttribPointerType type,
            bool normalised,
            int vertexStride,
            int offset);
    }
}
