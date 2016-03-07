using Moq;
using NUnit.Framework;
using Shouldly;
using System.IO.Abstractions;
using XogoEngine.Graphics;

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
            loader = new TextureLoader(adapter.Object, fileSystem.Object);
        }
    }
}
