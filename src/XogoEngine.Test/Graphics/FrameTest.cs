using NUnit.Framework;
using Shouldly;
using System;
using XogoEngine.Graphics;

namespace XogoEngine.Test.Graphics
{
    [TestFixture]
    internal sealed class FrameTest
    {
        [Test]
        public void Constructor_ThrowsArgumentNullException_OnNullTextureRegion()
        {
            Action construct = () => new Frame(null, 0.5);
            construct.ShouldThrow<ArgumentNullException>().ParamName.ShouldBe("textureRegion");
        }

        [Test]
        public void Constructor_CorrectlyInitialises_Instance()
        {
            var region = new TextureRegion(0, 0, 50, 50);
            var frame = new Frame(region, 0.1);

            frame.ShouldSatisfyAllConditions(
                () => frame.TextureRegion.ShouldBe(region),
                () => frame.Duration.ShouldBe(0.1)
            );
        }

        [Test]
        public void ToString_ShouldReturnExpectedString_WhenInvoked() {
            var region = new TextureRegion(0, 0, 50, 50);
            var frame = new Frame(region, 0.1);

            frame.ToString().ShouldBe(
                $"[Frame: TextureRegion={region.ToString()}, Duration={frame.Duration}]"
            );
        }
    }
}
