using NUnit.Framework;
using Shouldly;
using System;
using System.IO;
using XogoEngine.Audio;

namespace XogoEngine.Test.Audio
{
    [TestFixture]
    internal sealed class SoundLoaderTest
    {
        [Test]
        public void Load_ThrowsFileNotFoundException_OnUnknownFile()
        {
            var soundLoader = new SoundLoader();
            Action load = () => soundLoader.Load("bad-file-path/sound.wav");

            load.ShouldThrow<FileNotFoundException>();
        }
    }
}
