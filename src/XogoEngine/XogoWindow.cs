using OpenTK;
using OpenTK.Platform;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL4;
using System;
using System.IO.Abstractions;
using XogoEngine.Graphics;
using XogoEngine.Input;
using XogoEngine.OpenGL.Adapters;

namespace XogoEngine
{
    public class XogoWindow : IDisposable
    {
        private readonly IGameWindow gameWindow;
        private readonly IGlAdapter adapter;
        private readonly TextureLoader textureLoader;

        private bool isDisposed = false;

        public XogoWindow(int width, int height, string title)
            : this(new GameWindow(width, height, GraphicsMode.Default, title), EngineCore.GlAdapter)
        {
        }

        internal XogoWindow(IGameWindow gameWindow, IGlAdapter adapter)
        {
            gameWindow.ThrowIfNull(nameof(gameWindow));
            adapter.ThrowIfNull(nameof(adapter));

            this.gameWindow = gameWindow;
            this.adapter = adapter;
            textureLoader = new TextureLoader(adapter, new FileSystem());
            AddEventHandles();
        }

        public int Width
        {
            get { return gameWindow.Width; }
            set { gameWindow.Width = value; }
        }
        public int Height
        {
            get { return gameWindow.Height; }
            set { gameWindow.Height = value; }
        }
        public string Title
        {
            get { return gameWindow.Title; }
            set { gameWindow.Title = value; }
        }
        public bool IsDisposed => isDisposed;
        public TextureLoader TextureLoader => textureLoader;
        public KeyboardDevice Keyboard
        {
            get
            {
#pragma warning disable 0612
                if (gameWindow.InputDriver.Keyboard.Count <= 0)
                {
                    return null;
                }
                var keyboard = gameWindow.InputDriver.Keyboard[0];
#pragma warning restore 0612
                return new KeyboardDevice(keyboard);
            }
        }
        public MouseDevice Mouse
        {
            get
            {
#pragma warning disable 0612
                if (gameWindow.InputDriver.Mouse.Count <= 0)
                {
                    return null;
                }
                var mouse = gameWindow.InputDriver.Mouse[0];
#pragma warning restore 0612
                return new MouseDevice(mouse);
            }
        }

        protected virtual void Load() { }
        protected virtual void Update(double delta) { }
        protected virtual void Render(double delta) { }
        protected virtual void KeyUp() { }
        protected virtual void KeyDown() { }
        protected virtual void Unload() { }
        protected virtual void Resize() { }

        public void Run()
        {
            ThrowIfDisposed();
            gameWindow.Run();
        }

        public void SetBackGroundColour(Colour4 colour)
        {
            ThrowIfDisposed();
            adapter.ClearColor(
                colour.R / 255,
                colour.G / 255,
                colour.B / 255,
                colour.A / 255
            );
        }

        public void Dispose()
        {
            if (isDisposed)
            {
                return;
            }
            Dispose(true);
            isDisposed = true;
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                gameWindow?.Dispose();
            }
        }

        private void AddEventHandles()
        {
            gameWindow.Load += (sender, e) => Load();
            gameWindow.Unload += (sender, e) => Unload();
            gameWindow.UpdateFrame += (sender, e) => Update(e.Time);
            gameWindow.RenderFrame += (sender, e) => {
                adapter.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
                Render(e.Time);
                gameWindow.SwapBuffers();
            };
            gameWindow.KeyUp += (sender, e) => KeyUp();
            gameWindow.KeyDown += (sender, e) => KeyDown();
            gameWindow.Resize += (sender, e) => Resize();
        }

        private void ThrowIfDisposed()
        {
            if (isDisposed)
            {
                throw new ObjectDisposedException(GetType().FullName);
            }
        }
    }
}
