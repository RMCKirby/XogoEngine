using OpenTK.Graphics.OpenGL4;

namespace XogoEngine.OpenGL.Adapters
{
    public interface ITextureAdapter
    {
        int CreateTexture();
        void Bind(TextureTarget target, int handle);
    }
}
