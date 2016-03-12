using NUnit.Framework;
using Shouldly;
using System.Collections.Generic;
using XogoEngine.Graphics;

namespace XogoEngine.Test.Graphics
{
    [TestFixture]
    internal sealed class Colour4Test
    {
        private Colour4 colour;
        private static float red = 2;
        private static float green = 3;
        private static float blue = 4;

        [SetUp]
        public void SetUp()
        {
            colour = new Colour4(red, green, blue);
        }

        [Test]
        public void ColourComponents_ShouldBeCorrectlySet_AfterConstruction()
        {
            colour.ShouldSatisfyAllConditions(
                () => colour.R.ShouldBe(2),
                () => colour.G.ShouldBe(3),
                () => colour.B.ShouldBe(4),
                () => colour.A.ShouldBe(1)
            );
        }

        [Test]
        public void ObjectEquals_ReturnsFalse_OnNullObject()
        {
            colour.Equals(null).ShouldBeFalse();
        }

        [Test, TestCaseSource(nameof(UnequalColours))]
        public void ObjectEquals_ReturnsFalse_OnUnequalColours(object other)
        {
            colour.Equals(other).ShouldBeFalse();
        }

        [Test, TestCaseSource(nameof(UnequalColours))]
        public void ValueEquals_ReturnsFalse_OnUnequalColours(Colour4 other)
        {
            colour.Equals(other).ShouldBeFalse();
        }

        [Test, TestCaseSource(nameof(EqualColour))]
        public void ObjectEquals_ReturnsTrue_OnEqualColour(object other)
        {
            colour.Equals(other).ShouldBeTrue();
        }

        [Test, TestCaseSource(nameof(EqualColour))]
        public void ValueEquals_ReturnsTrue_OnEqualColour(Colour4 other)
        {
            colour.Equals(other).ShouldBeTrue();
        }

        [Test, TestCaseSource(nameof(UnequalColours))]
        public void EqualsOperator_ReturnsFalse_OnUnequalColours(Colour4 other)
        {
            (colour == other).ShouldBeFalse();
        }

        [Test, TestCaseSource(nameof(EqualColour))]
        public void EqualsOperator_ReturnsTrue_OnEqualColour(Colour4 other)
        {
            (colour == other).ShouldBeTrue();
        }

        [Test, TestCaseSource(nameof(UnequalColours))]
        public void NotEqualsOperator_ReturnsTrue_OnUnequalColours(Colour4 other)
        {
            (colour != other).ShouldBeTrue();
        }

        [Test, TestCaseSource(nameof(EqualColour))]
        public void NotEqualsOperator_ReturnsFalse_OnEqualColour(Colour4 other)
        {
            (colour != other).ShouldBeFalse();
        }

        private IEnumerable<TestCaseData> UnequalColours
        {
            get
            {
                yield return new TestCaseData(new Colour4(200, green, blue));
                yield return new TestCaseData(new Colour4(red, 200, blue));
                yield return new TestCaseData(new Colour4(red, green, 200));
                yield return new TestCaseData(new Colour4(red, green, blue, 200));
            }
        }

        private static IEnumerable<TestCaseData> EqualColour
        {
            get
            {
                yield return new TestCaseData(new Colour4(red, green, blue));
            }
        }
    }
}
