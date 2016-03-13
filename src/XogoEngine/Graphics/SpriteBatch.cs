using System;
using System.Collections.Generic;
using System.Linq;

namespace XogoEngine.Graphics
{
    public sealed class SpriteBatch : IDisposable
    {
        private readonly ISpriteSheet spriteSheet;
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
        public bool IsDisposed => isDisposed;

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
