using NUnit.Framework;
using Shouldly;
using System;
using XogoEngine.Graphics;

namespace XogoEngine.Test.Graphics
{
    [TestFixture]
    internal sealed class TextureAtlasTest
    {
        private TextureAtlas atlas;

        [SetUp]
        public void SetUp()
        {
            atlas = new TextureAtlas(200, 400);
        }

        [Test]
        public void Constructor_CorrectlyInitialises_Instance()
        {
            atlas.ShouldSatisfyAllConditions(
                () => atlas.Width.ShouldBe(200),
                () => atlas.Height.ShouldBe(400)
            );
        }

        [Test]
        public void Add_ThrowsArgumentNullException_OnNullTextureRegion()
        {
            Action add = () => atlas.Add(null);
            add.ShouldThrow<ArgumentNullException>();
        }

        [Test]
        public void Add_InsertsTextureRegion_ToInternalArray()
        {
            atlas.Add(new TextureRegion(2, 3, 20, 40));
            atlas.TextureRegions.ShouldContain(
                r => r.X == 2 && r.Y == 3 && r.Width == 20 && r.Height == 40
            );
        }
    }
}
