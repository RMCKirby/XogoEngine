using NUnit.Framework;
using Shouldly;
using System.Collections.Generic;
using OpenTK;
using XogoEngine.OpenGL.Extensions;

namespace XogoEngine.OpenGL.Test.Extensions
{
    [TestFixture]
    internal sealed class VectorExtensionsTest
    {
        [Test, TestCaseSource(nameof(UnequalVector2s))]
        public void Hashcodes_AreNotEqual_ForUnequalVector2s(Vector2 left, Vector2 right)
        {
            left.GetHashCode().ShouldNotBe(right.GetHashCode());
        }

        private IEnumerable<TestCaseData> UnequalVector2s
        {
            get
            {
                yield return new TestCaseData(Vector2.Zero, Vector2.One);
                yield return new TestCaseData(Vector2.UnitX, Vector2.UnitY);
            }
        }
    }
}
