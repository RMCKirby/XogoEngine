using System;
using System.Collections.Generic;
using System.Linq;

namespace XogoEngine.Graphics
{
    public sealed class SpriteBatch
    {
        public SpriteBatch(SpriteSheet spriteSheet)
        {
            if (spriteSheet == null)
            {
                throw new ArgumentNullException(nameof(spriteSheet));
            }
        }
    }
}
