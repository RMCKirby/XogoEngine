using NUnit.Framework;
using Shouldly;
using XogoEngine.OpenGL.Utilities;

namespace XogoEngine.OpenGL.Test.Utilities
{
    [TestFixture]
    internal sealed class HashCodeGeneratorTest
    {
        private HashCodeGenerator generator;

        [SetUp]
        public void SetUp()
        {
            generator = HashCodeGenerator.Initialise();
        }

        [Test]
        public void Initialise_ReturnsExpected_InitialValue()
        {
            generator.Value.ShouldBe(37);
        }

        [Test]
        public void HashCodes_AreEqual_ForEqualValues()
        {
            int left = 1, right = 1;
            generator.Hash(left).Value.ShouldBe(generator.Hash(right).Value);
        }

        [Test]
        public void HashCodes_AreNotEqual_ForUnequalValues()
        {
            int left = 1, right = 2;
            generator.Hash(left).Value.ShouldNotBe(generator.Hash(right).Value);
        }
    }
}
