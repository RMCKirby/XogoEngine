using System;
using System.Linq;
using System.IO;

namespace XogoEngine.Graphics
{
    public sealed class SpriteSheet : ISpriteSheet
    {
        private TextureAtlas textureAtlas;
        private bool isDisposed = false;

        public SpriteSheet(ITexture texture, string dataFilePath)
            : this(texture, dataFilePath, new TexturePackerParser())
        {
        }

        internal SpriteSheet(ITexture texture, string dataFilePath, TexturePackerParser parser)
        {
            ValidateArguments(texture, dataFilePath);
            this.Texture = texture;
            this.textureAtlas = parser.Parse(dataFilePath);
        }

        public ITexture Texture { get; }
        public TextureRegion[] TextureRegions
        {
            get { return textureAtlas.TextureRegions.ToArray(); }
        }
        public bool IsDisposed => isDisposed;

        public TextureRegion GetRegion(int index)
        {
            if (index < 0 || index > TextureRegions.Length)
            {
                throw new IndexOutOfRangeException(nameof(index) + $" : {index}");
            }
            return TextureRegions[index];
        }

        public TextureRegion this[int index] => GetRegion(index);

        public void Dispose()
        {
            if (isDisposed)
            {
                return;
            }
            Texture?.Dispose();
            isDisposed = true;
            GC.SuppressFinalize(this);
        }

        private void ValidateArguments(ITexture texture, string dataFilePath)
        {
            texture.ThrowIfNull(nameof(texture));
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
