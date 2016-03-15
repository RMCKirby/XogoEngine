using System;

namespace XogoEngine.Graphics
{
    public sealed class DuplicateSpriteException : Exception
    {
        public DuplicateSpriteException(string message) : base(message) { }
    }
}
