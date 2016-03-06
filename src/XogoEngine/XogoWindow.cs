using OpenTK.Platform;
using System;

namespace XogoEngine
{
    public class XogoWindow
    {
        private readonly IGameWindow gameWindow;

        internal XogoWindow(IGameWindow gameWindow)
        {
            if (gameWindow == null)
            {
                throw new ArgumentNullException(nameof(gameWindow));
            }
            this.gameWindow = gameWindow;
            AddEventHandles();
        }

        protected virtual void Load() { }

        public void Run()
        {
            gameWindow.Run();
        }

        private void AddEventHandles()
        {
            gameWindow.Load += (sender, e) => Load();
        }
    }
}
