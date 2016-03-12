using NUnit.Framework;
using Shouldly;
using XogoEngine.Graphics;

namespace XogoEngine.Test.Graphics
{
    [TestFixture]
    internal sealed class TextureRegionTest
    {
        [Test]
        public void Constructor_CorrectlyInitialises_Instance()
        {
            var region = new TextureRegion(10, 2, 16, 24);
            region.ShouldSatisfyAllConditions(
                () => region.X.ShouldBe(10),
                () => region.Y.ShouldBe(2),
                () => region.Width.ShouldBe(16),
                () => region.Height.ShouldBe(24)
            );
        }
    }
}
