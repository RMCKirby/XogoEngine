using NUnit.Framework;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using XogoEngine.Graphics;

namespace XogoEngine.Test.Graphics
{
    [TestFixture]
    internal sealed class TextureAtlasTest
    {
        private TextureAtlas atlas;

        [Test]
        public void Constructor_CorrectlyInitialises_Instance()
        {
            atlas = new TextureAtlas(200, 400);
            atlas.ShouldSatisfyAllConditions(
                () => atlas.Width.ShouldBe(200),
                () => atlas.Height.ShouldBe(400)
            );
        }

        [Test]
        public void Add_ThrowsArgumentNullException_OnNullTextureRegion()
        {
            
        }
    }
}
