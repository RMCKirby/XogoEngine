using Moq;
using NUnit.Framework;
using Shouldly;
using System;
using OpenTK.Graphics.OpenGL4;
using XogoEngine.Graphics;
using XogoEngine.OpenGL.Adapters;

namespace XogoEngine.Test.Graphics
{
    [TestFixture]
    internal sealed class TextureTest
    {
        private Texture texture;
        private Mock<ITextureAdapter> adapter;

        private static int handle = 1;
        private static int width = 50;
        private static int height = 100;

        [SetUp]
        public void SetUp()
        {
            adapter = new Mock<ITextureAdapter>();
            texture = new Texture(adapter.Object, handle, width, height);
        }

        [Test]
        public void Constructor_ThrowsArgumentNullException_OnNullAdapter()
        {
            Action construct = () => new Texture(null, handle, width, height);
            construct.ShouldThrow<ArgumentNullException>();
        }

        [Test]
        public void Constructor_CorrectlyInitialises_Instance()
        {
            texture.ShouldSatisfyAllConditions(
                () => texture.Handle.ShouldBe(handle),
                () => texture.Width.ShouldBe(width),
                () => texture.Height.ShouldBe(height),
                () => texture.IsDisposed.ShouldBeFalse()
            );
        }

        [Test]
        public void AdapterDeleteTexture_ShouldBeInvokedOnce_OnDisposal()
        {
            texture.Dispose();
            texture.Dispose();
            adapter.Verify(a => a.DeleteTexture(texture.Handle), Times.Once);
        }
    }
}
