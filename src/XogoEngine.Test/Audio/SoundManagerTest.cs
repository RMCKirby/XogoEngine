using Moq;
using NUnit.Framework;
using Shouldly;
using System;
using XogoEngine.Audio;
using XogoEngine.OpenAL.Adapters;

namespace XogoEngine.Test.Audio
{
    [TestFixture]
    internal sealed class SoundManagerTest
    {
        private SoundManager manager;
        private Mock<IOpenAlAdapter> adapter;

        [SetUp]
        public void SetUp()
        {
            manager = new SoundManager();
            adapter = new Mock<IOpenAlAdapter>();
        }

        [Test]
        public void Add_ThrowsArgumentException_OnExistingSound()
        {
            var existing = new Sound(adapter.Object, 1, 1);
            Action add = () => manager.Add(existing, "sound1");

            add();
            // now adding a second time should throw...
            add.ShouldThrow<ArgumentException>().Message.ShouldContain(
                "The given sound is already in the sound manager"
            );
        }

        [Test]
        public void Add_ThrowsArgumentException_OnExistingName()
        {
            var first = new Sound(adapter.Object, 1, 1);
            var second = new Sound(adapter.Object, 2, 2);
            Action<Sound> add = (s) => manager.Add(s, "sound1");

            add(first);
            // now adding a second time should throw...
            Assert.Throws<ArgumentException>(() => add(second)).Message.ShouldContain(
                "The given name: sound1 is already in use in the sound manager"
            );
        }

        [Test]
        public void Get_ThrowsArgumentException_OnNameNotInSounds()
        {
            Action get = () => manager.Get("bad");
            get.ShouldThrow<ArgumentException>().Message.ShouldContain(
                "not in the sound manager"
            );
        }

        [Test]
        public void IndexedGet_ThrowsArgumentException_OnNameNotInSounds()
        {
            Action get = delegate { var s = manager["bad"]; };
            get.ShouldThrow<ArgumentException>().Message.ShouldContain(
                "not in the sound manager"
            );
        }

        [Test]
        public void Get_ReturnsExpected_Sound()
        {
            var sound = new Sound(adapter.Object, 1, 1);
            manager.Add(sound, "sound-effect");

            manager.Get("sound-effect").ShouldBeSameAs(sound);
        }

        [Test]
        public void IndexedGet_ReturnsExpected_Sound()
        {
            var sound = new Sound(adapter.Object, 1, 1);
            manager.Add(sound, "sound-effect");

            manager["sound-effect"].ShouldBeSameAs(sound);
        }
    }
}
