using Moq;
using NUnit.Framework;
using Shouldly;
using System;
using XogoEngine.Graphics;

namespace XogoEngine.Test.Graphics
{
    [TestFixture]
    internal sealed class SpriteBatchTest
    {
        private SpriteBatch spriteBatch;
        private Mock<ISpriteSheet> spriteSheet;

        [SetUp]
        public void SetUp()
        {
            spriteSheet = new Mock<ISpriteSheet>();
            spriteSheet.Setup(s => s.GetRegion(It.IsAny<int>()))
                       .Returns(new TextureRegion(2, 2, 15, 20));

            spriteBatch = new SpriteBatch(spriteSheet.Object);
        }

        [Test]
        public void Constructor_ThrowsArgumentNullException_OnNullSpriteSheet()
        {
            SpriteSheet nullSheet = null;
            Action construct = () => new SpriteBatch(nullSheet);
            construct.ShouldThrow<ArgumentNullException>();
        }

        [Test]
        public void Constructor_CorrectlyInitialises_Instance()
        {
            spriteBatch.ShouldSatisfyAllConditions(
                () => spriteBatch.SpriteSheet.ShouldBe(spriteSheet.Object)
            );
        }

        [Test]
        public void Add_ThrowsArgumentNullException_OnNullSprite()
        {
            Sprite nullSprite = null;
            Action add = () => spriteBatch.Add(nullSprite);
            add.ShouldThrow<ArgumentNullException>().ParamName.ShouldBe("sprite");
        }

        [Test]
        public void Add_AddsGivenSprite_ToSpriteList()
        {
            Sprite sprite = new Sprite(spriteSheet.Object.GetRegion(0), 10, 10);
            spriteBatch.Add(sprite);
            spriteBatch.Sprites.ShouldContain(sprite);
        }

        [Test]
        public void Spritebatch_IsNotDisposed_AfterConstruction()
        {
            spriteBatch.IsDisposed.ShouldBeFalse();
        }

        [Test]
        public void SpriteBatch_IsDisposed_AfterDisposal()
        {
            spriteBatch.Dispose();
            spriteBatch.IsDisposed.ShouldBeTrue();
        }

        [Test]
        public void SpriteSheet_IsDisposed_OnDisposal()
        {
            spriteBatch.Dispose();
            spriteSheet.Verify(s => s.Dispose());
        }
    }
}
