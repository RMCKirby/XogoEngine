using OpenTK.Graphics.OpenGL4;

namespace XogoEngine.OpenGL.Adapters
{
    public interface ITextureAdapter
    {
        int GenTexture();
        void Bind(TextureTarget target, int handle);
        void DeleteTexture(int handle);
    }
}
