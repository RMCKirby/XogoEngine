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
            sprites.Add(sprite);
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
    }
}
