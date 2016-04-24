using Moq;
using NUnit.Framework;
using Shouldly;
using System;
using XogoEngine.Audio;
using XogoEngine.Audio.Adapters;

namespace XogoEngine.Audio
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
    }
}
