using System.IO;
using System.Linq;
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
                        X = (int)s.Attribute("x"),
                        Y = (int)s.Attribute("y"),
                        Width = (int)s.Attribute("w"),
                        Height = (int)s.Attribute("h")
                    }
            };

            var textureAtlas = new TextureAtlas(atlasDetails.Width, atlasDetails.Height);
            foreach (var region in atlasDetails.TextureRegions)
            {
                textureAtlas.Add(new TextureRegion(region.X, region.Y, region.Width, region.Height));
            }
            return textureAtlas;
        }
    }
}
