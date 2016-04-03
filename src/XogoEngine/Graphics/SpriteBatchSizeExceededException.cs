using System;

namespace XogoEngine.Graphics
{
    [Serializable]
    public sealed class SpriteBatchSizeExceededException : Exception
    {
        public SpriteBatchSizeExceededException(string message) : base(message) { }
    }
}
