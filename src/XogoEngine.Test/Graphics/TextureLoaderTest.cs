using Moq;
using NUnit.Framework;
using Shouldly;
using System;
using System.IO.Abstractions;
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

        [SetUp]
        public void SetUp()
        {
            adapter = new Mock<ITextureAdapter>();
            fileSystem = new Mock<IFileSystem>();
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
    }
}