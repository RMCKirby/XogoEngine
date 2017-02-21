using NUnit.Framework;
using Shouldly;
using System;
using System.Collections.Generic;
using XogoEngine.Graphics;

namespace XogoEngine.Test.Graphics
{
    [TestFixture]
    internal sealed class FrameTest
    {
        private readonly TextureRegion region = new TextureRegion(0, 0, 50, 50);

        [Test]
        public void Constructor_ThrowsArgumentNullException_OnNullTextureRegion()
        {
            Action construct = () => new Frame(null, 0.5);
            construct.ShouldThrow<ArgumentNullException>().ParamName.ShouldBe("textureRegion");
        }

        [Test, TestCaseSource(nameof(InvalidDurations))]
        public void Constructor_ThrowsArgumentOutOfRangeException_OnInvalidDuration(double duration)
        {
            Action construct = () => new Frame(region, duration);
            construct.ShouldThrow<ArgumentOutOfRangeException>().ParamName.ShouldBe("duration");
        }

        private IEnumerable<TestCaseData> InvalidDurations
        {
            get
            {
                yield return new TestCaseData(0);
                yield return new TestCaseData(-1);
            }
        }

        [Test]
        public void Constructor_CorrectlyInitialises_Instance()
        {
            var frame = new Frame(region, 0.1);

            frame.ShouldSatisfyAllConditions(
                () => frame.TextureRegion.ShouldBe(region),
                () => frame.Duration.ShouldBe(0.1)
            );
        }

        [Test]
        public void ToString_ShouldReturnExpectedString_WhenInvoked() {
            var frame = new Frame(region, 0.1);

            frame.ToString().ShouldBe(
                $"[Frame: TextureRegion={region.ToString()}, Duration={frame.Duration}]"
            );
        }
    }
}
