using System;
using System.IO;

namespace XogoEngine.Graphics
{
    public sealed class SpriteSheet
    {
        public SpriteSheet(ITexture texture, string dataFilePath)
        {
            if (texture == null)
            {
                throw new ArgumentNullException(nameof(texture));
            }
            if (texture.IsDisposed)
            {
                throw new ObjectDisposedException(texture.GetType().FullName);
            }
            if (!File.Exists(dataFilePath))
            {
                throw new FileNotFoundException(nameof(dataFilePath));
            }
        }
    }
}
