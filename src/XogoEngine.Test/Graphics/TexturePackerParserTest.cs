using NUnit.Framework;
using Shouldly;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
        public void Parse_ReturnsExpected_TextureAtlas()
        {
            var atlas = parser.Parse(dataFilePath);
            atlas.Width.ShouldBe(111);
            atlas.Height.ShouldBe(29);
            
        }
    }
}
