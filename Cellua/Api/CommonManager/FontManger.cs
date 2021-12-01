using System.Collections.Generic;
using System.IO;
using SFML.Graphics;

namespace Cellua.Api.CommonManager
{
    public class FontManger
    {
        public readonly Dictionary<string, Font> FontStorage;

        public FontManger()
        {
            FontStorage = new Dictionary<string, Font>();
        }

        public void LoadFont(string location, string fontName)
        {
            var fontData = File.ReadAllBytes(location);
            FontStorage[fontName] = new Font(fontData);
        }
    }
}