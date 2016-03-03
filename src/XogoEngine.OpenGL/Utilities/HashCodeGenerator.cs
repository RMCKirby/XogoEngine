namespace XogoEngine.OpenGL.Utilities
{
    public struct HashCodeGenerator
    {
        private const int HashCodeIntialiser = 37;
        private const int HashCodeMultiplier = 397;

        private HashCodeGenerator(int value)
        {
            Value = value;
        }

        public int Value { get; }

        public HashCodeGenerator Hash<T>(T value) where T : struct
        {
            unchecked
            {
                return new HashCodeGenerator(Value * HashCodeMultiplier + value.GetHashCode());
            }
        }

        public static HashCodeGenerator Initialise() => new HashCodeGenerator(HashCodeIntialiser);
    }
}
