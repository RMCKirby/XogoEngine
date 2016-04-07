using NUnit.Framework;
using Shouldly;
using System;
using System.Linq;
using XogoEngine.Graphics;

namespace XogoEngine.Test.Graphics
{
    [TestFixture]
    internal sealed class AnimationTest
    {
        private Animation animation;
        private TextureRegion region;

        [SetUp]
        public void SetUp()
        {
            region = new TextureRegion(0, 0, 20, 25);
        }

        [Test]
        public void Constructor_ThrowsArgumentNullException_OnNullFrames()
        {
            Frame[] frames = null;
            Action construct = () => new Animation(frames);

            construct.ShouldThrow<ArgumentNullException>().ParamName.ShouldBe("frames");
        }

        [Test]
        public void Constructor_ThrowsArgumentException_OnEmptyFrames()
        {
            var frames = new Frame[] { };
            Action construct = () => new Animation(frames);

            construct.ShouldThrow<ArgumentException>().Message.ShouldContain(
                "An animation requires at least one frame. Zero found"
            );
        }

        [Test]
        public void Constructor_ThrowsArgumentNullException_OnNullFrame()
        {
            Frame firstFrame = new Frame(region, 0.1);
            Frame secondFrame = null;
            var frames = new Frame[] { firstFrame, secondFrame };
            Action construct = () => new Animation(frames);

            construct.ShouldThrow<ArgumentNullException>().ParamName.ShouldBe("frame");
        }

        [Test]
        public void CurrentFrame_IsFirstFrameGiven_AfterConstruction()
        {
            var frames = new Frame[] { new Frame(region, 0.1), new Frame(region, 0.2) };
            animation = new Animation(frames);

            animation.CurrentFrame.ShouldBe(frames[0]);
        }

        [Test]
        public void TotalAnimationDuration_ShouldBeTheSum_OfAllFrameDurations()
        {
            var frames = new Frame[] { new Frame(region, 0.1), new Frame(region, 0.2) };
            animation = new Animation(frames);

            double expected = frames.Sum(f => f.Duration);
            animation.TotalDuration.ShouldBe(expected);
        }

        [Test]
        public void CurrentFrame_IsInitialFrame_AfterReset()
        {
            var frames = new Frame[] { new Frame(region, 0.1), new Frame(region, 0.2) };
            animation = new Animation(frames);

            animation.Update(delta: 0.15);
            animation.Reset();
            animation.CurrentFrame.ShouldBe(frames[0]);
        }

        [Test]
        public void CurrentFrame_IsSwitchedToNextFrame_OnceFrameTimeHasElapsed()
        {
            var frames = new Frame[] { new Frame(region, 0.1), new Frame(region, 0.2) };
            animation = new Animation(frames);

            animation.Update(0.1);
            animation.CurrentFrame.ShouldBe(frames[1]);
        }
    }
}
