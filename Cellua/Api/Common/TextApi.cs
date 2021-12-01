using SFML.Graphics;

namespace Cellua.Api.Common
{
    public class TextApi
    {
        public static TextObject CreateText() => new(new Text());
    }
}