using OpenTK.Graphics.OpenGL4;

namespace XogoEngine.OpenGL.Adapters
{
    public interface IGlAdapter :
        IBufferAdapter,
        IDrawAdapter,
        IShaderAdapter,
        ITextureAdapter,
        IVertexArrayAdapter
    {
        void Clear(ClearBufferMask mask);
        void ClearColor(float red, float green, float blue, float alpha);
    }
}
