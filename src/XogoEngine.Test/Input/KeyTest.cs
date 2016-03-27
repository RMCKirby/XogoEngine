using NUnit.Framework;
using Shouldly;
using System.Collections.Generic;
using XogoEngine.Input;

using tkKey = OpenTK.Input.Key;

namespace XogoEngine.Test.Input
{
    [TestFixture]
    internal sealed class KeyTest
    {
        [Test, TestCaseSource(nameof(ExpectedKeys))]
        public void WrappedKeys_Returns_ExpectedOpenTkKeys(Key wrapped, tkKey expected)
        {
            ((tkKey)wrapped).ShouldBe(expected);
        }

        private static IEnumerable<TestCaseData> ExpectedKeys
        {
            get
            {
                yield return new TestCaseData(Key.Left, tkKey.Left);
                yield return new TestCaseData(Key.Right, tkKey.Right);
                yield return new TestCaseData(Key.Up, tkKey.Up);
                yield return new TestCaseData(Key.Down, tkKey.Down);
                yield return new TestCaseData(Key.Space, tkKey.Space);
            }
        }
    }
}
