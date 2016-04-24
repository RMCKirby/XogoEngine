using Moq;
using NUnit.Framework;
using Shouldly;
using System;
using OpenTK.Audio.OpenAL;
using XogoEngine.Audio;
using XogoEngine.Audio.Adapters;

namespace XogoEngine.Test.Audio
{
    [TestFixture]
    internal sealed class SoundTest
    {
        private Sound sound;
        private Mock<IOpenAlAdapter> adapter;

        [SetUp]
        public void SetUp()
        {
            adapter = new Mock<IOpenAlAdapter>();
            adapter.Setup(a => a.GenSource()).Returns(1);
            adapter.Setup(a => a.GenBuffer()).Returns(4);
            adapter.Setup(a => a.GetGain(It.IsAny<int>())).Returns(1.0f);

            sound = new Sound(adapter.Object, 1, 4);
        }

        [Test]
        public void InternalConstructor_CorrectlyInitialises_Instance()
        {
            sound.ShouldSatisfyAllConditions(
                () => sound.SourceHandle.ShouldBe(1),
                () => sound.BufferHandle.ShouldBe(4),
                () => sound.IsDisposed.ShouldBeFalse()
            );
        }

        [Test]
        public void SoundGain_IsExpectedValue_AfterConstruction()
        {
            sound.Gain.ShouldBe(1);
        }

        [Test]
        public void SetGain_InvokedAdapter_AsExpected()
        {
            sound.Gain = 0.5f;
            adapter.Verify(a => a.Source(sound.SourceHandle, ALSourcef.Gain, 0.5f), Times.Once);
        }

        [Test]
        public void GetGain_ThrowsObjectDisposedException_OnDisposedSound()
        {
            Action getGain = delegate { var g = sound.Gain; };
            sound.Dispose();
            getGain.ShouldThrow<ObjectDisposedException>();
        }

        [Test]
        public void SetGain_ThrowsObjectDisposedException_OnDisposedSound()
        {
            Action setGain = delegate { sound.Gain = 0.5f; };
            sound.Dispose();
            setGain.ShouldThrow<ObjectDisposedException>();
        }

        [Test]
        public void Playing_ShouldBeFalse_ByDefault()
        {
            sound.Playing.ShouldBeFalse();
        }

        [Test]
        public void GetPlaying_ThrowsObjectDisposedException_OnDisposedSound()
        {
            Action getPlaying = delegate { var p = sound.Playing; };
            sound.Dispose();
            getPlaying.ShouldThrow<ObjectDisposedException>();
        }

        [Test]
        public void Sound_ShouldBeDisposed_AfterDisposal()
        {
            sound.Dispose();
            sound.IsDisposed.ShouldBeTrue();
        }

        [Test]
        public void AdapterDeleteSource_IsInvoked_OnDispose()
        {
            sound.Dispose();
            sound.Dispose();
            adapter.Verify(a => a.DeleteSource(sound.SourceHandle), Times.Once);
        }

        [Test]
        public void AdapterDeleteBuffer_IsInvoked_OnDispose()
        {
            sound.Dispose();
            sound.Dispose();
            adapter.Verify(a => a.DeleteBuffer(sound.BufferHandle), Times.Once);
        }

        [Test]
        public void Gain_ThrowsObjectDisposedException_OnDisposedSound()
        {
            sound.Dispose();

        }

        [Test]
        public void Play_ThrowsObjectDisposedException_OnDisposedSound()
        {
            Action play = () => sound.Play();

            sound.Dispose();
            play.ShouldThrow<ObjectDisposedException>();
        }

        [Test]
        public void Pause_ThrowsObjectDisposedException_OnDisposedSound()
        {
            Action pause = () => sound.Pause();

            sound.Dispose();
            pause.ShouldThrow<ObjectDisposedException>();
        }

        [Test]
        public void Stop_ThrowsObjectDisposedException_OnDisposedSound()
        {
            Action stop = () => sound.Stop();

            sound.Dispose();
            stop.ShouldThrow<ObjectDisposedException>();
        }

        [Test]
        public void AdapterSourcePlay_isInvoked_OnPlay()
        {
            sound.Play();
            adapter.Verify(a => a.SourcePlay(sound.SourceHandle), Times.Once);
        }

        [Test]
        public void AdapterSourcePause_isInvoked_OnPause()
        {
            sound.Pause();
            adapter.Verify(a => a.SourcePause(sound.SourceHandle), Times.Once);
        }

        [Test]
        public void AdapterSourceStop_isInvoked_OnStop()
        {
            sound.Stop();
            adapter.Verify(a => a.SourceStop(sound.SourceHandle), Times.Once);
        }
    }
}
