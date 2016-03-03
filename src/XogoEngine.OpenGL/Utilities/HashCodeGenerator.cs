namespace XogoEngine.OpenGL.Utilities
{
    public struct HashCodeGenerator
    {
        private const int HashCodeIntialiser = 37;
        private const int HashCodeMultiplier = 17;

        private HashCodeGenerator(int value)
        {
            Value = value;
        }

        public int Value { get; }

        public static HashCodeGenerator Initialise() => new HashCodeGenerator(HashCodeIntialiser);
    }
}
