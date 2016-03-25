using NUnit.Framework;
using Shouldly;
using XogoEngine.OpenGL.Adapters;

namespace XogoEngine.Test
{
    [TestFixture]
    internal sealed class EngineCoreTest
    {
        [Test, Ignore("Concrete adapter not yet in place")]
        public void OpenGlAdapter_ShouldNot_BeNull()
        {
            EngineCore.GlAdapter.ShouldNotBeNull();
        }

        [Test, Ignore("Concrete adapter not yet in place")]
        public void OpenGlAdapter_ShouldBe_OfExpectedType()
        {
            EngineCore.GlAdapter.ShouldBeOfType<IGladapter>();
        }
    }
}
