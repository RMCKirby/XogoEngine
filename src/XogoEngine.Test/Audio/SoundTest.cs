using Moq;
using NUnit.Framework;
using Shouldly;
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
    }
}
