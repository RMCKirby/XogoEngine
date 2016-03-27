using NUnit.Framework;
using Shouldly;
using XogoEngine.OpenGL.Adapters;

namespace XogoEngine.Test
{
    [TestFixture]
    internal sealed class EngineCoreTest
    {
        [Test]
        public void OpenGlAdapter_ShouldNot_BeNull()
        {
            EngineCore.GlAdapter.ShouldNotBeNull();
        }

        [Test]
        public void OpenGlAdapter_ShouldBe_OfExpectedType()
        {
            EngineCore.GlAdapter.ShouldBeOfType<GlAdapter>();
        }
    }
}
