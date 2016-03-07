using NUnit.Framework;
using Shouldly;

namespace XogoEngine.Test.Graphics
{
    [TestFixture]
    internal sealed class TextureLoaderTest
    {
        private TextureLoader loader;

        [SetUp]
        public void SetUp()
        {
            loader = new TextureLoader(adapter.Object, fileSystem.Object);
        }
    }
}
