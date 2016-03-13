using System;

namespace XogoEngine.Graphics
{
    public interface ISpriteSheet : IDisposable
    {
        ITexture Texture { get; }
        TextureRegion[] TextureRegions { get; }
        bool IsDisposed { get; }

        TextureRegion GetRegion(int index);
        TextureRegion this[int index] { get; }
    }
}
