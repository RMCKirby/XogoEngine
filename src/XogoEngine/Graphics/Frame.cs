namespace XogoEngine.Graphics
{
    public sealed class Frame
    {
        public Frame(TextureRegion textureRegion, double duration)
        {
            textureRegion.ThrowIfNull(nameof(textureRegion));
            this.TextureRegion = textureRegion;
            Duration = duration;
        }

        public TextureRegion TextureRegion { get; }
        public double Duration { get; }

        public override string ToString() => ($"[Frame: TextureRegion={TextureRegion}, Duration={Duration}]");
    }
}
