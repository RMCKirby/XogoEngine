using Moq;
using NUnit.Framework;
using Shouldly;
using XogoEngine.Input;

namespace XogoEngine.Test.Input
{
    [TestFixture, Ignore("Cannot test until a mockable OpenTK.Input.KeyboardDevice exists")]
    internal sealed class KeyboardDeviceTest
    {
        private KeyboardDevice keyboard;
        private Mock<OpenTK.Input.IInputDevice> tkKeyboard;
    }
}
