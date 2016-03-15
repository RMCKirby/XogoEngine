using OpenTK.Graphics.OpenGL4;

namespace XogoEngine.OpenGL.Adapters
{
    public interface IDrawAdapter
    {
        void DrawElements(BeginMode mode, int indiceCount, DrawElementsType elementType, int offset);
    }
}
