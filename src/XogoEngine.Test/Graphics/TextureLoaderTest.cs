using Moq;
using NUnit.Framework;
using Shouldly;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Abstractions;
using OpenTK.Graphics.OpenGL4;
using XogoEngine.Graphics;
using XogoEngine.OpenGL.Adapters;

namespace XogoEngine.Test.Graphics
{
    [TestFixture]
    internal sealed class TextureLoaderTest
    {
        private TextureLoader loader;
        private Mock<ITextureAdapter> adapter;
        private Mock<IFileSystem> fileSystem;

        private static string texturePath = "assets/link-sprite.png";

        [SetUp]
        public void SetUp()
        {
            adapter = new Mock<ITextureAdapter>();
            fileSystem = new Mock<IFileSystem>();
            fileSystem.Setup(f => f.File.Exists(It.IsAny<string>()))
                      .Returns(true);

            loader = new TextureLoader(adapter.Object, fileSystem.Object);
        }

        [Test]
        public void ConstructorThrowsArgumentNullException_OnNullAdapter()
        {
            Action construct = () => new TextureLoader(null, fileSystem.Object);
            construct.ShouldThrow<ArgumentNullException>();
        }

        [Test]
        public void ConstructorThrowsArgumentNullException_OnNullFileSystem()
        {
            Action construct = () => new TextureLoader(adapter.Object, null);
            construct.ShouldThrow<ArgumentNullException>();
        }

        [Test, TestCaseSource(nameof(InvalidPathArguments))]
        public void Load_ThrowsArgumentException_OnInvalidPathArgument(string path)
        {
            Assert.Throws<ArgumentException>(() => loader.Load(path));
        }

        private IEnumerable<TestCaseData> InvalidPathArguments
        {
            get
            {
                yield return new TestCaseData("");
                yield return new TestCaseData("    ");
                yield return new TestCaseData(null);
            }
        }

        [Test]
        public void Load_ThrowsFileNotFoundException_OnMissingFile()
        {
            fileSystem.Setup(f => f.File.Exists(It.IsAny<string>()))
                      .Returns(false);
            Assert.Throws<FileNotFoundException>(() => loader.Load(texturePath));
        }

        [Test]
        public void AdapterCreateTexture_IsInvoked_OnLoad()
        {
            loader.Load(texturePath);
            adapter.Verify(a => a.GenTexture(), Times.Once);
        }

        [Test]
        public void AdapterBindTexture_IsInvoked_OnLoad()
        {
            int handle = 1;
            adapter.Setup(a => a.GenTexture()).Returns(handle);

            loader.Load(texturePath);
            adapter.Verify(a => a.Bind(TextureTarget.Texture2D, handle), Times.Once);
        }

        [Test]
        public void Load_ReturnsExpected_Texture()
        {
            var texture = loader.Load(texturePath);
            texture.ShouldSatisfyAllConditions(
                () => texture.Width.ShouldBe(16),
                () => texture.Height.ShouldBe(22)
            );
        }
    }
}
