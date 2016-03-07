using Moq;
using NUnit.Framework;
using Shouldly;
using System;
using OpenTK;
using OpenTK.Platform;
using OpenTK.Graphics.OpenGL4;
using XogoEngine;
using XogoEngine.OpenGL.Adapters;

namespace XogoEngine.Test
{
    [TestFixture]
    internal sealed class XogoWindowTest
    {
        private TestWindow window;
        private Mock<IGameWindow> gameWindow;
        private Mock<IGladapter> adapter;

        [SetUp]
        public void SetUp()
        {
            gameWindow = new Mock<IGameWindow>();
            adapter = new Mock<IGladapter>();
            window = new TestWindow(gameWindow.Object, adapter.Object);
        }

        [Test]
        public void InternalConstructor_ThrowsArgumentNullException_OnNullWindow()
        {
            GameWindow nullWindow = null;
            Action construct = () => new XogoWindow(nullWindow, adapter.Object);

            construct.ShouldThrow<ArgumentException>();
        }

        [Test]
        public void InternalConstructor_ThrowsArgumentNullException_OnNullAdapter()
        {
            IGladapter nullAdapter = null;
            Action construct = () => new XogoWindow(gameWindow.Object, nullAdapter);

            construct.ShouldThrow<ArgumentNullException>();
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

        [Test]
        public void Update_IsInvoked_OnGameWindowUpdate()
        {
            bool invoked = false;
            window.UpdateAction = () => invoked = true;

            gameWindow.Raise(g => g.UpdateFrame += null, new FrameEventArgs());
            invoked.ShouldBeTrue();
        }

        [Test]
        public void Render_isInvoked_OnGameWindowRender()
        {
            bool invoked = false;
            window.RenderAction = () => invoked = true;

            gameWindow.Raise(g => g.RenderFrame += null, new FrameEventArgs());
            invoked.ShouldBeTrue();
        }

        [Test]
        public void AdapterClear_IsInvokedAsExpected_OnRender()
        {
            gameWindow.Raise(g => g.RenderFrame += null, new FrameEventArgs());
            adapter.Verify(a => a.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit));
        }

        [Test]
        public void GameWindowSwapBuffers_IsInvoked_OnRender()
        {
            gameWindow.Raise(g => g.RenderFrame += null, new FrameEventArgs());
            gameWindow.Verify(g => g.SwapBuffers());
        }

        [Test]
        public void Unload_isInvoked_OnGameWindow()
        {
            bool invoked = false;
            window.UnloadAction = () => invoked = true;

            gameWindow.Raise(g => g.Unload += null, EventArgs.Empty);
            invoked.ShouldBeTrue();
        }

        private sealed class TestWindow : XogoWindow
        {
            public Action LoadAction = delegate { };
            public Action UpdateAction = delegate { };
            public Action RenderAction = delegate { };
            public Action UnloadAction = delegate { };

            public TestWindow(IGameWindow window, IGladapter adapter)
                : base(window, adapter) { }

            protected override void Load()
            {
                LoadAction();
            }

            protected override void Update(double delta)
            {
                UpdateAction();
            }

            protected override void Render(double delta)
            {
                RenderAction();
            }

            protected override void Unload()
            {
                UnloadAction();
            }
        }
    }
}
