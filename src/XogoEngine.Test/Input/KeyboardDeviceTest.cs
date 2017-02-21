using Moq;
using NUnit.Framework;
using XogoEngine.Input;

namespace XogoEngine.Test.Input
{
    [TestFixture, Ignore("Cannot test until a mockable OpenTK.Input.KeyboardDevice exists")]
    internal sealed class KeyboardDeviceTest
    {
#pragma warning disable 0169
        private KeyboardDevice keyboard;
        private Mock<OpenTK.Input.IInputDevice> tkKeyboard;
#pragma warning restore 0169
    }
}
