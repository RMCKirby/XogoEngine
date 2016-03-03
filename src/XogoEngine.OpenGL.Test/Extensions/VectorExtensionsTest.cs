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
            left.GetFixedHashCode().ShouldNotBe(right.GetFixedHashCode());
        }

        private IEnumerable<TestCaseData> UnequalVector2s
        {
            get
            {
                yield return new TestCaseData(Vector2.Zero, Vector2.One);
                yield return new TestCaseData(Vector2.UnitX, Vector2.UnitY);
            }
        }

        [Test, TestCaseSource(nameof(UnequalVector3s))]
        public void HashCodes_AreNotEqual_ForUnequalVector3s(Vector3 left, Vector3 right)
        {
            left.GetFixedHashCode().ShouldNotBe(right.GetFixedHashCode());
        }

        private IEnumerable<TestCaseData> UnequalVector3s
        {
            get
            {
                yield return new TestCaseData(Vector3.Zero, Vector3.One);
                yield return new TestCaseData(Vector3.UnitX, Vector3.UnitY);
                yield return new TestCaseData(Vector3.UnitX, Vector3.UnitZ);
                yield return new TestCaseData(Vector3.UnitY, Vector3.UnitZ);
            }
        }

        [Test, TestCaseSource(nameof(UnequalVector4s))]
        public void HashCodes_AreNotEqual_ForUnequalVector4s(Vector4 left, Vector4 right)
        {
            left.GetFixedHashCode().ShouldNotBe(right.GetFixedHashCode());
        }

        private IEnumerable<TestCaseData> UnequalVector4s
        {
            get
            {
                yield return new TestCaseData(Vector4.Zero, Vector4.One);
                yield return new TestCaseData(Vector4.UnitX, Vector4.UnitY);
                yield return new TestCaseData(Vector4.UnitX, Vector4.UnitZ);
                yield return new TestCaseData(Vector4.UnitX, Vector4.UnitW);
                yield return new TestCaseData(Vector4.UnitY, Vector4.UnitZ);
                yield return new TestCaseData(Vector4.UnitY, Vector4.UnitW);
                yield return new TestCaseData(Vector4.UnitZ, Vector4.UnitW);
            }
        }
    }
}
