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
        private static string dataFilePath = "assets/spritesheet-data.png";

        [SetUp]
        public void SetUp()
        {
            texture = new Mock<ITexture>();
            texture.SetupGet(t => t.IsDisposed).Returns(false);
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
    }
}
