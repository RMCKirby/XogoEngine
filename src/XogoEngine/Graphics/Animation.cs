using System;
using System.Linq;

namespace XogoEngine.Graphics
{
    public sealed class Animation
    {
        private Frame[] frames;
        private Frame currentFrame;
        private int currentFrameIndex;
        private double totalElapsedTime;
        private double totalDuration;
        private bool loop;

        public Animation(params Frame[] frames) : this(false, frames)
        {
        }

        public Animation(bool loop, params Frame[] frames)
        {
            frames.ThrowIfNull(nameof(frames));
            if (frames.Length <= 0)
            {
                throw new ArgumentException(
                    "An animation requires at least one frame. Zero found"
                );
            }
            foreach (var frame in frames)
            {
                frame.ThrowIfNull(nameof(frame));
            }
            this.frames = frames;
            this.currentFrame = frames[0];
            this.totalDuration = frames.Sum(f => f.Duration);
            this.loop = loop;
        }

        public Frame CurrentFrame => currentFrame;
        public double TotalDuration => totalDuration;
        public double TotalElapsedTime => totalElapsedTime;
        public bool Loop => loop;

        public void Reset()
        {
            totalElapsedTime = 0;
            currentFrameIndex = 0;
            currentFrame = frames[currentFrameIndex];
        }

        public void Update(double delta)
        {
            totalElapsedTime += delta;
            // get the point in the animation at which the current frame ends
            double currentAnimationPoint = frames.Take(currentFrameIndex + 1)
                                                 .Sum(f => f.Duration);

            if (totalElapsedTime >= currentAnimationPoint && currentFrameIndex < frames.Length - 1)
            {
                currentFrameIndex++;
                currentFrame = frames[currentFrameIndex];
            }
            currentFrame = frames[currentFrameIndex];
        }
    }
}
