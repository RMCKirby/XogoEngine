using tkMouseDevice = OpenTK.Input.MouseDevice;

namespace XogoEngine.Input
{
    public sealed class MouseDevice
    {
        private readonly tkMouseDevice mouseDevice;

        internal MouseDevice(tkMouseDevice mouseDevice)
        {
            this.mouseDevice = mouseDevice;
        }

        public string Description => mouseDevice.Description;
        public int X => mouseDevice.X;
        public int Y => mouseDevice.Y;
    }
}
