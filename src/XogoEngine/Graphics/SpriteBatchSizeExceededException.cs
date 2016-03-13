using System;

namespace XogoEngine.Graphics
{
    public sealed class SpriteBatchSizeExceededException : Exception
    {
        public SpriteBatchSizeExceededException(string message) : base(message) { }
    }
}
