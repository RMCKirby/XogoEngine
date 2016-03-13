using System;
using System.Collections.Generic;
using System.Linq;

namespace XogoEngine.Graphics
{
    public sealed class SpriteBatch : IDisposable
    {
        private readonly ISpriteSheet spriteSheet;
        private List<Sprite> sprites = new List<Sprite>();
        private bool isDisposed = false;

        private const int BatchSize = 100;

        public SpriteBatch(ISpriteSheet spriteSheet)
        {
            if (spriteSheet == null)
            {
                throw new ArgumentNullException(nameof(spriteSheet));
            }
            this.spriteSheet = spriteSheet;
        }

        public ISpriteSheet SpriteSheet => spriteSheet;
        public IEnumerable<Sprite> Sprites => sprites;
        public bool IsDisposed => isDisposed;

        public void Add(params Sprite[] sprites)
        {
            foreach (var sprite in sprites)
            {
                Add(sprite);
            }
        }

        public void Add(Sprite sprite)
        {
            if (sprite == null)
            {
                throw new ArgumentNullException(nameof(sprite));
            }
            if (sprites.Contains(sprite))
            {
                throw new DuplicateSpriteException(
                    "The given sprite has already been added to this sprite batch"
                );
            }
            ValidateBatchSize();
            sprites.Add(sprite);
        }

        public void Remove(params Sprite[] sprites)
        {
            foreach (var sprite in sprites)
            {
                Remove(sprite);
            }
        }

        public void Remove(Sprite sprite)
        {
            if (!sprites.Contains(sprite))
            {
                throw new ArgumentException(
                    nameof(sprite) + " was not found in this sprite batch"
                );
            }
            sprites.Remove(sprite);
        }

        public void Dispose()
        {
            if (isDisposed)
            {
                return;
            }
            spriteSheet.Dispose();
            isDisposed = true;
            GC.SuppressFinalize(this);
        }

        private void ValidateBatchSize()
        {
            if (sprites.Count == BatchSize)
            {
                throw new SpriteBatchSizeExceededException(
                    "The sprite batch is full. No free slots."
                );
            }
        }
    }
}
