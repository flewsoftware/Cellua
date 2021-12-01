using System.IO;
using Cellua.Api.CommonManager;
using SFML.Graphics;

namespace Cellua.Api.Common
{
    public class FontApi
    {
        public FontManger FontManger;
        public FontApi(FontManger fontManger)
        {
            FontManger = fontManger;
        }
        public FontObject LoadFont(string name) => new(FontManger.FontStorage[name]);
    }
}