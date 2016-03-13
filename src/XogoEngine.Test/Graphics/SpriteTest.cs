using NUnit.Framework;
using Shouldly;
using System;
using XogoEngine.Graphics;

namespace XogoEngine.Test.Graphics
{
    [TestFixture]
    internal sealed class SpriteTest
    {
        [Test]
        public void Constructor_ThrowsArgumentNullException_OnNullTextureRegion()
        {
            TextureRegion region = null;
            Action construct = () => new Sprite(region, 10, 10);
            construct.ShouldThrow<ArgumentNullException>();
        }

        [Test]
        public void Constructor_CorrectlyInitialises_Instance()
        {
            var region = new TextureRegion(2, 2, 15, 20);
            var sprite = new Sprite(region, 40, 50);

            sprite.ShouldSatisfyAllConditions(
                () => sprite.X.ShouldBe(40),
                () => sprite.Y.ShouldBe(50),
                () => sprite.Width.ShouldBe(15),
                () => sprite.Height.ShouldBe(20)
            );
        }
    }
}
