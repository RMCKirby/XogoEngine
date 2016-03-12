using NUnit.Framework;
using Shouldly;
using System;
using System.Collections.Generic;
using System.IO;
using XogoEngine.Graphics;

namespace XogoEngine.Test.Graphics
{
    [TestFixture]
    internal sealed class TexturePackerParserTest
    {
        private TexturePackerParser parser;

        private static string dataFilePath = "assets/spritesheet-data.xml";

        [SetUp]
        public void SetUp()
        {
            parser = new TexturePackerParser();
        }

        [Test]
        public void Parse_ThrowsFileNotFoundException_OnMissingDataFile()
        {
            Action parse = () => parser.Parse("bad-file-path");
            parse.ShouldThrow<FileNotFoundException>();
        }

        [Test]
        public void Parse_ReturnsExpected_TextureAtlasDimensions()
        {
            var atlas = parser.Parse(dataFilePath);
            atlas.Width.ShouldBe(111);
            atlas.Height.ShouldBe(29);
        }

        [Test, TestCaseSource(nameof(ExpectedTextureRegions))]
        public void Parse_ReturnsExpected_TextureRegions(TextureRegion expected)
        {
            var atlas = parser.Parse(dataFilePath);
            atlas.TextureRegions.ShouldContain(r =>
                r.X == expected.X &&
                r.Y == expected.Y &&
                r.Width == expected.Width &&
                r.Height == expected.Height
            );
        }

        /* Should probably remove these tests now that we fully test through the public
        * API of XogoEngine.Graphics.SpriteSheet */
        private IEnumerable<TestCaseData> ExpectedTextureRegions
        {
            get
            {
                yield return new TestCaseData(new TextureRegion(2, 2, 16, 24));
                yield return new TestCaseData(new TextureRegion(20, 2, 16, 24));
                yield return new TestCaseData(new TextureRegion(38, 2, 17, 24));
                yield return new TestCaseData(new TextureRegion(57, 2, 16, 24));
                yield return new TestCaseData(new TextureRegion(75, 2, 16, 25));
                yield return new TestCaseData(new TextureRegion(93, 2, 16, 25));
            }
        }
    }
}
