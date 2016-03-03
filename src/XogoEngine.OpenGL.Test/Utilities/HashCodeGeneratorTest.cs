using NUnit.Framework;
using Shouldly;
using XogoEngine.OpenGL.Utilities;

namespace XogoEngine.OpenGL.Test.Utilities
{
    [TestFixture]
    internal sealed class HashCodeGeneratorTest
    {
        [Test]
        public void Initialise_ReturnsExpected_InitialValue()
        {
            var generator = HashCodeGenerator.Initialise();
            generator.Value.ShouldBe(37);
        }
    }
}
