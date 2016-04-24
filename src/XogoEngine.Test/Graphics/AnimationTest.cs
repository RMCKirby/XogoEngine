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
        private Animation loopingAnimation;
        private Frame[] frames;
        private TextureRegion region;

        [SetUp]
        public void SetUp()
        {
            region = new TextureRegion(0, 0, 20, 25);
            frames = new Frame[]
            {
                new Frame(region, 0.1),
                new Frame(region, 0.2)
            };
            animation = new Animation(frames);
            loopingAnimation = new Animation(true, frames);
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
            animation.CurrentFrame.ShouldBe(frames[0]);
        }

        [Test]
        public void TotalElapsedTime_IsZero_AfterConstruction()
        {
            animation.TotalElapsedTime.ShouldBe(0);
        }

        [Test]
        public void NonLoopingAnimation_IsSetToNotLoop_AfterConstruction()
        {
            animation.Loop.ShouldBeFalse();
        }

        [Test]
        public void LoopingAnimation_IsSetToLoop_AfterConstruction()
        {
            loopingAnimation.Loop.ShouldBeTrue();
        }

        [Test]
        public void TotalAnimationDuration_ShouldBeTheSum_OfAllFrameDurations()
        {
            double expected = frames.Sum(f => f.Duration);
            animation.TotalDuration.ShouldBe(expected);
        }

        [Test]
        public void CurrentFrame_IsInitialFrame_AfterReset()
        {
            animation.Update(delta: 0.15);
            animation.Reset();
            animation.CurrentFrame.ShouldBe(frames[0]);
        }

        [Test]
        public void TotalElapsedTime_IsZero_AfterReset()
        {
            animation.Update(0.25);
            animation.TotalElapsedTime.ShouldBe(0.25);
            animation.Reset();
            animation.TotalElapsedTime.ShouldBe(0);
        }

        [Test]
        public void CurrentFrame_IsSwitchedToNextFrame_OnceFrameTimeHasElapsed()
        {
            animation.Update(0.1);
            animation.CurrentFrame.ShouldBe(frames[1]);
        }

        [Test]
        public void Update_DoesNotOverflowFrames_OnEndOfAnimation()
        {
            animation.Update(0.1);
            animation.Update(0.3);
            animation.CurrentFrame.ShouldBe(frames[1]);
        }

        [Test]
        public void LoopingAnimation_ShouldBeResetOnUpdate_OnceTotalDurationHasElapsed()
        {
            loopingAnimation.CurrentFrame.ShouldBe(frames[0]);
            loopingAnimation.Update(frames[0].Duration);
            loopingAnimation.CurrentFrame.ShouldBe(frames[1]);
            loopingAnimation.Update(frames[1].Duration);
            // we should now be back to the first frame
            loopingAnimation.CurrentFrame.ShouldBe(frames[0]);
        }
    }
}
