using Moq;
using NUnit.Framework;
using Shouldly;
using System;
using OpenTK;
using OpenTK.Platform;
using XogoEngine;

namespace XogoEngine.Test
{
    [TestFixture]
    internal sealed class XogoWindowTest
    {
        private XogoWindow window;
        private Mock<IGameWindow> gameWindow;

        [SetUp]
        public void SetUp()
        {
            gameWindow = new Mock<IGameWindow>();
            window = new XogoWindow(gameWindow.Object);
        }

        [Test]
        public void InternalConstructor_ThrowsArgumentNullException_OnNullWindow()
        {
            GameWindow nullWindow = null;
            Action construct = () => new XogoWindow(nullWindow);

            construct.ShouldThrow<ArgumentException>();
        }

        [Test]
        public void GameWindowRun_isInvoked_OnRun()
        {
            window.Run();
            gameWindow.Verify(g => g.Run(), Times.Once);
        }

        private sealed class TestWindow : XogoWindow
        {
            public TestWindow(IGameWindow window) : base(window) { }
        }
    }
}
