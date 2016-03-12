using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Linq;

namespace XogoEngine.Graphics
{
    internal class TexturePackerParser
    {
        public TextureAtlas Parse(string dataFilePath)
        {
            if (!File.Exists(dataFilePath))
            {
                throw new FileNotFoundException(nameof(dataFilePath) + $" : {dataFilePath}");
            }
            var xml = XDocument.Load(dataFilePath);
            var atlasDetails = new {
                Width = (int)xml.Element("TextureAtlas").Attribute("width"),
                Height = (int)xml.Element("TextureAtlas").Attribute("height"),
                TextureRegions = from s in xml.Root.Descendants("sprite")
                    select new {
                        X = s.Attribute("x"),
                        Y = s.Attribute("y")
                    }
            };

            var textureAtlas = new TextureAtlas(atlasDetails.Width, atlasDetails.Height);

            return new TextureAtlas(atlasDetails.Width, atlasDetails.Height);
        }
    }
}
