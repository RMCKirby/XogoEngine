using OpenTK.Platform;
using OpenTK.Graphics.OpenGL4;
using System;
using XogoEngine.OpenGL.Adapters;

namespace XogoEngine
{
    public class XogoWindow : IDisposable
    {
        private readonly IGameWindow gameWindow;
        private readonly IGladapter adapter;

        private bool isDisposed = false;

        internal XogoWindow(IGameWindow gameWindow, IGladapter adapter)
        {
            if (gameWindow == null)
            {
                throw new ArgumentNullException(nameof(gameWindow));
            }
            if (adapter == null)
            {
                throw new ArgumentNullException(nameof(adapter));
            }
            this.gameWindow = gameWindow;
            this.adapter = adapter;
            AddEventHandles();
        }

        public bool IsDisposed { get { return isDisposed; } }

        protected virtual void Load() { }
        protected virtual void Update(double delta) { }
        protected virtual void Render(double delta) { }
        protected virtual void Unload() { }

        public void Run()
        {
            gameWindow.Run();
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
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
        }
    }
}
