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
        private TestWindow window;
        private Mock<IGameWindow> gameWindow;

        [SetUp]
        public void SetUp()
        {
            gameWindow = new Mock<IGameWindow>();
            window = new TestWindow(gameWindow.Object);
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

        [Test]
        public void Load_IsInvoked_OnGameWindowLoadEvent()
        {
            bool invoked = false;
            window.LoadAction = () => invoked = true;

            gameWindow.Raise(g => g.Load += null, EventArgs.Empty);
            invoked.ShouldBeTrue();
        }

        private sealed class TestWindow : XogoWindow
        {
            public Action LoadAction = delegate { };

            public TestWindow(IGameWindow window) : base(window) { }

            protected override void Load()
            {
                LoadAction();
            }
        }
    }
}
