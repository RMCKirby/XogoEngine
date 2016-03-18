using OpenTK.Graphics.OpenGL4;

namespace XogoEngine.OpenGL.Adapters
{
    public interface IDrawAdapter
    {
        void DrawArrays(PrimitiveType mode, int first, int count);
        void DrawElements(BeginMode mode, int indiceCount, DrawElementsType elementType, int offset);
    }
}
