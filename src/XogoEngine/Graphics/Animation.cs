using System;
using System.Linq;

namespace XogoEngine.Graphics
{
    public sealed class Animation
    {
        private Frame[] frames;
        private Frame currentFrame;
        private int currentFrameIndex;
        private double elapsedTime;
        private double totalDuration;

        public Animation(params Frame[] frames)
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
        }

        public Frame CurrentFrame => currentFrame;
        public double TotalDuration => totalDuration;

        public void Reset()
        {
            currentFrameIndex = 0;
            currentFrame = frames[currentFrameIndex];
        }

        public void Update(double delta)
        {
            elapsedTime += delta;
            if (elapsedTime >= currentFrame.Duration)
            {
                currentFrameIndex++;
                currentFrame = frames[currentFrameIndex];
            }
        }
    }
}