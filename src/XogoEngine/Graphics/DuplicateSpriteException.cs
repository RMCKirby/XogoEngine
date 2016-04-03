using System;

namespace XogoEngine.Graphics
{
    [Serializable]
    public sealed class DuplicateSpriteException : Exception
    {
        public DuplicateSpriteException(string message) : base(message) { }
    }
}
