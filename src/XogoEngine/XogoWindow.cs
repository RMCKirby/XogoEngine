using OpenTK.Platform;
using System;

namespace XogoEngine
{
    public class XogoWindow
    {
        private readonly IGameWindow window;

        internal XogoWindow(IGameWindow window)
        {
            if (window == null)
            {
                throw new ArgumentNullException(nameof(window));
            }
            this.window = window;
        }

        public void Run()
        {
            window.Run();
        }
    }
}
