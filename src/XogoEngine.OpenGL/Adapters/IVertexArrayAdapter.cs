namespace XogoEngine.OpenGL.Adapters
{
    public interface IVertexArrayAdapter
    {
        int GenVertexArray();

        void BindVertexArray(int handle);
        void DeleteVertexArray(int handle);
    }
}
