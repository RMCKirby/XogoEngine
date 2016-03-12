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

        [Test, TestCaseSource(nameof(EqualColour))]
        public void HashCodes_AreEqual_ForEqualColours(Colour4 other)
        {
            colour.GetHashCode().ShouldBe(other.GetHashCode());
        }

        [Test, TestCaseSource(nameof(UnequalColours))]
        public void HashCodes_AreNotEqual_ForUnequalColours(Colour4 other)
        {
            colour.GetHashCode().ShouldNotBe(other.GetHashCode());
        }

        [Test]
        public void ToString_ReturnsExpected_String()
        {
            string expected = $"[Colour4 : R={colour.R}, G={colour.G}, B={colour.B}, A={colour.A}]";
            colour.ToString().ShouldBe(expected);
        }

        [Test, TestCaseSource(nameof(ColourFieldTestCases))]
        public void StaticColours_ReturnExpectedColours(Colour4 expected, Colour4 actual)
        {
            expected.ShouldBe(actual);
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

        private static IEnumerable<TestCaseData> ColourFieldTestCases
        {
            get
            {
                yield return new TestCaseData(new Colour4(0, 0, 0, 0), Colour4.Black);
                yield return new TestCaseData(new Colour4(255, 255, 255), Colour4.White);
                yield return new TestCaseData(new Colour4(128, 128, 128), Colour4.Grey);
                yield return new TestCaseData(new Colour4(192, 192, 192), Colour4.Silver);
                yield return new TestCaseData(new Colour4(255, 0, 0), Colour4.Red);
                yield return new TestCaseData(new Colour4(0, 128, 0), Colour4.Green);
                yield return new TestCaseData(new Colour4(0, 0, 255), Colour4.Blue);
                yield return new TestCaseData(new Colour4(255, 255, 0), Colour4.Yellow);
                yield return new TestCaseData(new Colour4(255, 165, 0), Colour4.Orange);
                yield return new TestCaseData(new Colour4(128, 0, 128), Colour4.Purple);
                yield return new TestCaseData(new Colour4(75, 0, 130), Colour4.Indigo);
                yield return new TestCaseData(new Colour4(0, 255, 0), Colour4.Lime);
                yield return new TestCaseData(new Colour4(255, 0, 255), Colour4.Magenta);
                yield return new TestCaseData(new Colour4(128, 0, 0), Colour4.Maroon);
                yield return new TestCaseData(new Colour4(128, 128, 0), Colour4.Olive);
                yield return new TestCaseData(new Colour4(0, 128, 128), Colour4.Teal);
                yield return new TestCaseData(new Colour4(25, 25, 112), Colour4.MidnightBlue);
                yield return new TestCaseData(new Colour4(0, 0, 128), Colour4.Navy);
                yield return new TestCaseData(new Colour4(100, 149, 237), Colour4.CornFlowerBlue);
                yield return new TestCaseData(new Colour4(72, 61, 139), Colour4.DarkSlateBlue);
                yield return new TestCaseData(new Colour4(106, 90, 205), Colour4.SlateBlue);
                yield return new TestCaseData(new Colour4(123, 104, 238), Colour4.MediumSlateBlue);
                yield return new TestCaseData(new Colour4(132, 112, 255), Colour4.LightSlateBlue);
                yield return new TestCaseData(new Colour4(0, 0, 205), Colour4.MediumBlue);
                yield return new TestCaseData(new Colour4(65, 105, 225), Colour4.RoyalBlue);
                yield return new TestCaseData(new Colour4(30, 144, 255), Colour4.DodgerBlue);
                yield return new TestCaseData(new Colour4(0, 191, 255), Colour4.DeepSkyBlue);
                yield return new TestCaseData(new Colour4(135, 206, 235), Colour4.SkyBlue);
                yield return new TestCaseData(new Colour4(135, 196, 250), Colour4.LightSkyBlue);
                yield return new TestCaseData(new Colour4(70, 130, 180), Colour4.SteelBlue);
                yield return new TestCaseData(new Colour4(176, 196, 222), Colour4.LightSteelBlue);
                yield return new TestCaseData(new Colour4(173, 216, 230), Colour4.LightBlue);
                yield return new TestCaseData(new Colour4(176, 24, 230), Colour4.PowderBlue);
                yield return new TestCaseData(new Colour4(175, 238, 238), Colour4.PaleTurquoise);
                yield return new TestCaseData(new Colour4(0, 206, 209), Colour4.DarkTurquoise);
                yield return new TestCaseData(new Colour4(72, 209, 204), Colour4.MediumTurquoise);
                yield return new TestCaseData(new Colour4(64, 224, 208), Colour4.Turquoise);
                yield return new TestCaseData(new Colour4(0, 255, 255), Colour4.Cyan);
                yield return new TestCaseData(new Colour4(224, 255, 255), Colour4.LightCyan);
                yield return new TestCaseData(new Colour4(95, 158, 160), Colour4.CadetBlue);
                yield return new TestCaseData(new Colour4(102, 205, 170), Colour4.MediumAquamarine);
                yield return new TestCaseData(new Colour4(127, 255, 212), Colour4.Aquamarine);
                yield return new TestCaseData(new Colour4(0, 100, 0), Colour4.DarkGreen);
                yield return new TestCaseData(new Colour4(85, 107, 47), Colour4.DarkOliveGreen);
                yield return new TestCaseData(new Colour4(143, 188, 143), Colour4.DarkSeaGreen);
                yield return new TestCaseData(new Colour4(46, 139, 87), Colour4.SeaGreen);
                yield return new TestCaseData(new Colour4(32, 178, 170), Colour4.LightSeaGreen);
                yield return new TestCaseData(new Colour4(152, 251, 152), Colour4.PaleGreen);
                yield return new TestCaseData(new Colour4(0, 255, 127), Colour4.SpringGreen);
                yield return new TestCaseData(new Colour4(124, 252, 0), Colour4.LawnGreen);
                yield return new TestCaseData(new Colour4(127, 255, 0), Colour4.Chartreuse);
                yield return new TestCaseData(new Colour4(0, 250, 154), Colour4.MediumSpringGreen);
                yield return new TestCaseData(new Colour4(173, 255, 47), Colour4.GreenYellow);
                yield return new TestCaseData(new Colour4(50, 205, 50), Colour4.LimeGreen);
                yield return new TestCaseData(new Colour4(154, 205, 50), Colour4.YellowGreen);
                yield return new TestCaseData(new Colour4(34, 139, 34), Colour4.ForestGreen);
                yield return new TestCaseData(new Colour4(107, 142, 35), Colour4.OliveDrab);
                yield return new TestCaseData(new Colour4(189, 183, 107), Colour4.DarkKhaki);
                yield return new TestCaseData(new Colour4(240, 230, 140), Colour4.Khaki);
                yield return new TestCaseData(new Colour4(238, 232, 170), Colour4.PaleGoldenrod);
                yield return new TestCaseData(new Colour4(250, 250, 210), Colour4.LightGoldenrodYellow);
                yield return new TestCaseData(new Colour4(255, 255, 224), Colour4.LightYellow);
                yield return new TestCaseData(new Colour4(255, 215, 0), Colour4.Gold);
                yield return new TestCaseData(new Colour4(238, 221, 130), Colour4.LightGoldenrod);
                yield return new TestCaseData(new Colour4(218, 165, 32), Colour4.Goldenrod);
                yield return new TestCaseData(new Colour4(184, 134, 11), Colour4.DarkGoldenrod);
                yield return new TestCaseData(new Colour4(233, 150, 122), Colour4.DarkSalmon);
                yield return new TestCaseData(new Colour4(250, 128, 114), Colour4.Salmon);
                yield return new TestCaseData(new Colour4(255, 160, 122), Colour4.LightSalmon);
                yield return new TestCaseData(new Colour4(255, 140, 0), Colour4.DarkOrange);
                yield return new TestCaseData(new Colour4(255, 127, 80), Colour4.Coral);
                yield return new TestCaseData(new Colour4(240, 128, 128), Colour4.LightCoral);
                yield return new TestCaseData(new Colour4(255, 102, 0), Colour4.BlazeOrange);
                yield return new TestCaseData(new Colour4(204, 85, 0), Colour4.BurntOrange);
                yield return new TestCaseData(new Colour4(255, 99, 71), Colour4.Tomato);
                yield return new TestCaseData(new Colour4(255, 69, 0), Colour4.OrangeRed);
                yield return new TestCaseData(new Colour4(165, 42, 42), Colour4.Brown);
                yield return new TestCaseData(new Colour4(188, 143, 143), Colour4.RosyBrown);
                yield return new TestCaseData(new Colour4(205, 92, 92), Colour4.IndianRed);
                yield return new TestCaseData(new Colour4(139, 69, 19), Colour4.SaddleBrown);
                yield return new TestCaseData(new Colour4(160, 82, 45), Colour4.Sienna);
                yield return new TestCaseData(new Colour4(205, 133, 63), Colour4.Peru);
                yield return new TestCaseData(new Colour4(222, 184, 135), Colour4.Burlywood);
                yield return new TestCaseData(new Colour4(245, 245, 220), Colour4.Beige);
                yield return new TestCaseData(new Colour4(245, 222, 179), Colour4.Wheat);
                yield return new TestCaseData(new Colour4(244, 164, 96), Colour4.SandyBrown);
                yield return new TestCaseData(new Colour4(210, 180, 140), Colour4.Tan);
                yield return new TestCaseData(new Colour4(210, 105, 30), Colour4.Chocolate);
                yield return new TestCaseData(new Colour4(178, 34, 34), Colour4.Firebrick);
                yield return new TestCaseData(new Colour4(255, 192, 203), Colour4.Pink);
                yield return new TestCaseData(new Colour4(238, 130, 238), Colour4.Violet);
                yield return new TestCaseData(new Colour4(255, 105, 180), Colour4.HotPink);
                yield return new TestCaseData(new Colour4(255, 20, 147), Colour4.DeepPink);
                yield return new TestCaseData(new Colour4(255, 182, 193), Colour4.LightPink);
                yield return new TestCaseData(new Colour4(219, 112, 147), Colour4.PaleVioletRed);
                yield return new TestCaseData(new Colour4(199, 21, 133), Colour4.MediumVioletRed);
                yield return new TestCaseData(new Colour4(208, 32, 144), Colour4.VioletRed);
                yield return new TestCaseData(new Colour4(221, 160, 221), Colour4.Plum);
                yield return new TestCaseData(new Colour4(218, 112, 214), Colour4.Orchid);
                yield return new TestCaseData(new Colour4(186, 85, 211), Colour4.MediumOrchid);
                yield return new TestCaseData(new Colour4(153, 50, 204), Colour4.DarkOrchid);
                yield return new TestCaseData(new Colour4(148, 0, 211), Colour4.DarkViolet);
                yield return new TestCaseData(new Colour4(138, 43, 226), Colour4.BlueViolet);
                yield return new TestCaseData(new Colour4(147, 112, 219), Colour4.MediumPurple);
                yield return new TestCaseData(new Colour4(216, 191, 216), Colour4.Thistle);
                yield return new TestCaseData(new Colour4(255, 250, 250), Colour4.Snow);
                yield return new TestCaseData(new Colour4(248, 248, 255), Colour4.GhostWhite);
                yield return new TestCaseData(new Colour4(245, 245, 245), Colour4.WhiteSmoke);
                yield return new TestCaseData(new Colour4(220, 220, 220), Colour4.Gainsboro);
                yield return new TestCaseData(new Colour4(255, 250, 240), Colour4.FloralWhite);
                yield return new TestCaseData(new Colour4(253, 245, 230), Colour4.OldLace);
                yield return new TestCaseData(new Colour4(240, 240, 230), Colour4.Linen);
                yield return new TestCaseData(new Colour4(250, 235, 215), Colour4.AntiqueWhite);
                yield return new TestCaseData(new Colour4(255, 239, 213), Colour4.PapayaWhip);
                yield return new TestCaseData(new Colour4(255, 235, 205), Colour4.BlanchedAlmond);
                yield return new TestCaseData(new Colour4(255, 228, 196), Colour4.Bisque);
                yield return new TestCaseData(new Colour4(255, 218, 185), Colour4.PeachPuff);
                yield return new TestCaseData(new Colour4(255, 222, 173), Colour4.NavajoWhite);
                yield return new TestCaseData(new Colour4(255, 228, 181), Colour4.Moccasin);
                yield return new TestCaseData(new Colour4(255, 248, 220), Colour4.Cornsilk);
                yield return new TestCaseData(new Colour4(255, 255, 240), Colour4.Ivory);
                yield return new TestCaseData(new Colour4(255, 250, 205), Colour4.LemonChiffon);
                yield return new TestCaseData(new Colour4(255, 245, 238), Colour4.Seashell);
                yield return new TestCaseData(new Colour4(240, 255, 240), Colour4.HoneyDew);
                yield return new TestCaseData(new Colour4(245, 255, 250), Colour4.MintCream);
                yield return new TestCaseData(new Colour4(240, 255, 255), Colour4.Azure);
                yield return new TestCaseData(new Colour4(240, 248, 255), Colour4.AliceBlue);
                yield return new TestCaseData(new Colour4(230, 230, 250), Colour4.Lavender);
                yield return new TestCaseData(new Colour4(255, 240, 245), Colour4.LavenderBlush);
                yield return new TestCaseData(new Colour4(255, 228, 225), Colour4.MistyRose);
            }
        }
    }
}
