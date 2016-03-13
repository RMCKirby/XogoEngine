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
    }
}
