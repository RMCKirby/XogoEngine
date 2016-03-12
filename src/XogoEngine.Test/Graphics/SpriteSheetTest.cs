using Moq;
using NUnit.Framework;
using Shouldly;
using System;
using System.Collections.Generic;
using System.IO;
using XogoEngine.Graphics;

namespace XogoEngine.Test.Graphics
{
    [TestFixture]
    internal sealed class SpriteSheetTest
    {
        private SpriteSheet spriteSheet;
        private Mock<ITexture> texture;

        private static string spriteSheetPath = "assets/spritesheet.png";
        private static string dataFilePath = "assets/spritesheet-data.xml";

        [SetUp]
        public void SetUp()
        {
            texture = new Mock<ITexture>();
            texture.SetupGet(t => t.IsDisposed).Returns(false);

            spriteSheet = new SpriteSheet(texture.Object, dataFilePath);
        }

        [Test]
        public void Constructor_ThrowsArgumentNullException_OnNullTexture()
        {
            Action construct = () => new SpriteSheet(null, dataFilePath);
            construct.ShouldThrow<ArgumentNullException>()
                     .ParamName.ShouldBe("texture");
        }

        [Test]
        public void Constructor_ThrowsFileNotFoundException_OnMissingDataFile()
        {
            Action construct = () => new SpriteSheet(texture.Object, "bad-file-path");
            construct.ShouldThrow<FileNotFoundException>();
        }

        [Test]
        public void Constructor_ThrowsObjectDisposedException_OnDisposedTexture()
        {
            texture.SetupGet(t => t.IsDisposed).Returns(true);
            Action construct = () => new SpriteSheet(texture.Object, dataFilePath);
            construct.ShouldThrow<ObjectDisposedException>();
        }

        [Test]
        public void GivenValidArguments_ConstructorCorrectlyInitialises_Instance()
        {
            spriteSheet.ShouldSatisfyAllConditions(
                () => spriteSheet.Texture.ShouldBe(texture.Object)
            );
        }

        [Test, TestCaseSource(nameof(ExpectedTextureRegions))]
        public void TextureRegions_Contain_ExpectedRegions(TextureRegion expected)
        {
            spriteSheet.TextureRegions.Length.ShouldBe(6);
            spriteSheet.TextureRegions.ShouldContain(r =>
                r.X == expected.X &&
                r.Y == expected.Y &&
                r.Width == expected.Width &&
                r.Height == expected.Height
            );
        }

        private IEnumerable<TestCaseData> ExpectedTextureRegions
        {
            get
            {
                yield return new TestCaseData(new TextureRegion(2, 2, 16, 24));
                yield return new TestCaseData(new TextureRegion(20, 2, 16, 24));
                yield return new TestCaseData(new TextureRegion(38, 2, 17, 24));
                yield return new TestCaseData(new TextureRegion(57, 2, 16, 24));
                yield return new TestCaseData(new TextureRegion(75, 2, 16, 25));
                yield return new TestCaseData(new TextureRegion(93, 2, 16, 25));
            }
        }

        [Test, TestCaseSource(nameof(InvalidIndexes))]
        public void GetRegion_ThrowsIndexOutOfRangeException_OnInvalidIndex(int index)
        {
            Action get = () => spriteSheet.GetRegion(index);
            get.ShouldThrow<IndexOutOfRangeException>();
        }

        [Test, TestCaseSource(nameof(InvalidIndexes))]
        public void GetRegionIndexer_ThrowsIndexOutOfRangeException_OnInvalidIndex(int index)
        {
            Action get = () => { var region = spriteSheet[index]; };
            get.ShouldThrow<IndexOutOfRangeException>();
        }

        private IEnumerable<TestCaseData> InvalidIndexes
        {
            get
            {
                yield return new TestCaseData(-1);
                yield return new TestCaseData(100000);
            }
        }

        [Test]
        public void GetRegion_Returns_ExpectedRegion()
        {
            var region = spriteSheet.GetRegion(0);
            AssertExpectedRegion(region);
        }

        [Test]
        public void GetRegionIndexer_Returns_ExpectedRegion()
        {
            var region = spriteSheet[0];
            AssertExpectedRegion(region);
        }

        private void AssertExpectedRegion(TextureRegion region)
        {
            region.ShouldSatisfyAllConditions(
                () => region.X.ShouldBe(2),
                () => region.Y.ShouldBe(2),
                () => region.Width.ShouldBe(16),
                () => region.Height.ShouldBe(24)
            );
        }

        [Test]
        public void SpriteSheet_isDisposed_AfterDisposal()
        {
            spriteSheet.Dispose();
            spriteSheet.IsDisposed.ShouldBeTrue();
        }

        [Test]
        public void Texture_isDisposed_OnDisposal()
        {
            spriteSheet.Dispose();
            texture.Verify(t => t.Dispose());
        }
    }
}
