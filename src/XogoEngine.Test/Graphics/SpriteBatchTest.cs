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
            var sprite = new Sprite(spriteSheet.Object.GetRegion(0), 10, 10);
            spriteBatch.Add(sprite);
            spriteBatch.Sprites.ShouldContain(sprite);
        }

        [Test]
        public void Add_ThrowsDuplicateSpriteException_WhenAddingTheSameSpriteAgain()
        {
            var sprite = new Sprite(spriteSheet.Object.GetRegion(0), 10, 10);
            spriteBatch.Add(sprite);
            Action addAgain = () => spriteBatch.Add(sprite);

            addAgain.ShouldThrow<DuplicateSpriteException>().Message.ShouldContain(
                "The given sprite has already been added to this sprite batch"
            );
        }

        [Test]
        public void Remove_ThrowsArgumentException_WhenSpriteIsNotInBatch()
        {
            var sprite = new Sprite(spriteSheet.Object.GetRegion(0), 10, 10);
            Action remove = () => spriteBatch.Remove(sprite);

            remove.ShouldThrow<ArgumentException>();
        }

        [Test]
        public void Remove_RemovesGivenSprite_FromExistingSpriteList()
        {
            var sprite = new Sprite(spriteSheet.Object.GetRegion(0), 10, 10);
            spriteBatch.Add(sprite);

            spriteBatch.Sprites.ShouldContain(sprite);
            spriteBatch.Remove(sprite);
            spriteBatch.Sprites.ShouldNotContain(sprite);
        }

        [Test]
        public void Add_ThrowsBatchSizeExceededException_OnAddingTooManySprites()
        {
            const int batchSize = 100;
            for (int i = 0; i < batchSize; i++)
            {
                var sprite = new Sprite(spriteSheet.Object.GetRegion(0), i, i);
                spriteBatch.Add(sprite);
            }
            var illegalSprite = new Sprite(spriteSheet.Object.GetRegion(0), 101, 101);
            Action add = () => spriteBatch.Add(illegalSprite);

            add.ShouldThrow<SpriteBatchSizeExceededException>();
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
