using NUnit.Framework;
using Shouldly;
using System;
using OpenTK;
using XogoEngine.Graphics;

namespace XogoEngine.Test.Graphics
{
    [TestFixture]
    internal sealed class SpriteTest
    {
        private Sprite sprite;
        private static TextureRegion textureRegion = new TextureRegion(2, 2, 15, 20);
        private static Vector4 colour = new Vector4(
            Colour4.White.R, Colour4.White.G, Colour4.White.B, Colour4.White.A
        );

        [SetUp]
        public void SetUp()
        {
            sprite = new Sprite(textureRegion, 40, 50);
        }

        [Test]
        public void Constructor_ThrowsArgumentNullException_OnNullTextureRegion()
        {
            TextureRegion region = null;
            Action construct = () => new Sprite(region, 10, 10);
            construct.ShouldThrow<ArgumentNullException>();
        }

        [Test]
        public void Constructor_CorrectlyInitialises_Instance()
        {
            sprite.ShouldSatisfyAllConditions(
                () => sprite.X.ShouldBe(40),
                () => sprite.Y.ShouldBe(50),
                () => sprite.Width.ShouldBe(15),
                () => sprite.Height.ShouldBe(20),
                () => sprite.Colour.ShouldBe(Colour4.White)
            );
        }

        [Test]
        public void Sprite_ShouldHave_FourVertices()
        {
            sprite.Vertices.Length.ShouldBe(4);
        }

        [Test]
        public void SpriteModifiedEvent_ShouldBeFired_OnSpriteModification()
        {
            bool invoked = false;
            Sprite.SpriteHandler action = (sender, e) => invoked = true;
            sprite.SpriteModified += action;

            sprite.Modify(s => {
                s.X = 50;
                s.Y = 60;
                s.Width = 100;
                s.Height = 200;
                s.Colour = Colour4.SkyBlue;
            });

            invoked.ShouldBeTrue();
            sprite.SpriteModified -= action;
        }

        [Test]
        public void SpriteModifiedEvent_ShouldNotBeFired_IfSpriteHasNotChanged()
        {
            /* for efficiency, we don't want to re-send vertices to the GPU
            * if the modified value(s) are the same as those previous */
            bool invoked = false;
            Sprite.SpriteHandler action = (sender, e) => invoked = true;
            sprite.SpriteModified += action;

            sprite.Modify(s => {
                s.X = 40;
                s.Y = 50;
                s.Width = textureRegion.Width;
                s.Height = textureRegion.Height;
                s.Colour = Colour4.White;
                s.TextureRegion = sprite.TextureRegion;
            });
            invoked.ShouldBeFalse();
            sprite.SpriteModified -= action;
        }

        [Test]
        public void SpriteModifiedEvent_ShouldBeFired_OnSwitchedTextureRegion()
        {
            bool invoked = false;
            Sprite.SpriteHandler action = (sender, e) => invoked = true;
            sprite.SpriteModified += action;

            sprite.Modify(s => s.TextureRegion = new TextureRegion(0, 0, 50, 50));
            invoked.ShouldBeTrue();
            sprite.SpriteModified -= action;
        }
    }
}
