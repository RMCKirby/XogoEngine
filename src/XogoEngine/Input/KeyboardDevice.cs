using TkKeyboardDevice = OpenTK.Input.KeyboardDevice;

namespace XogoEngine.Input
{
    public sealed class KeyboardDevice
    {
        private readonly TkKeyboardDevice keyboardDevice;

        internal KeyboardDevice(TkKeyboardDevice keyboardDevice)
        {
            this.keyboardDevice = keyboardDevice;
        }

        public bool this[Key key] => keyboardDevice[(OpenTK.Input.Key)key];

        public int NumberOfKeys => keyboardDevice.NumberOfKeys;
        public int NumberOfLeds => keyboardDevice.NumberOfLeds;
        public string Description => keyboardDevice.Description;
    }
}
