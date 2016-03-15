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
        private static byte[] data = new byte[] { 1, 2 };

        [SetUp]
        public void SetUp()
        {
            adapter = new Mock<ITextureAdapter>();
            texture = new Texture(adapter.Object, handle, width, height, data);
        }

        [Test]
        public void Constructor_ThrowsArgumentNullException_OnNullAdapter()
        {
            Action construct = () => new Texture(null, handle, width, height, data);
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
        public void TextureTarget_ShouldBe_TextureTarget2D()
        {
            texture.Target.ShouldBe(TextureTarget.Texture2D);
        }

        [Test]
        public void TextureData_ShouldBe_CorrectlySet()
        {
            texture.Data.ShouldBe(data);
        }

        [Test]
        public void DataProperty_ReturnsCopy_RatherThanActualField()
        {
            texture.Data.ShouldNotBeSameAs(data);
        }

        [Test]
        public void AdapterBind_isInvoked_OnBind()
        {
            texture.Bind();
            adapter.Verify(a => a.Bind(texture.Target, texture.Handle));
        }

        [Test]
        public void AdapterDeleteTexture_ShouldBeInvokedOnce_OnDisposal()
        {
            texture.Dispose();
            texture.Dispose();
            adapter.Verify(a => a.DeleteTexture(texture.Handle), Times.Once);
        }

        [Test]
        public void Texture_IsDisposed_OnDisposal()
        {
            texture.IsDisposed.ShouldBeFalse();
            texture.Dispose();
            texture.IsDisposed.ShouldBeTrue();
        }
    }
}
