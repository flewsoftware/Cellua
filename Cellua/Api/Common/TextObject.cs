using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Immutable;
using SFML.Graphics;
using SFML.System;

namespace Cellua.Api.Common
{
    public class TextObject
    {
        public readonly Text Text;

        public TextObject(Text text)
        {
            Text = text;
        }

        public void UseFont(FontObject font)
        {
            Text.Font = font.Font;
        }

        public IEnumerable<float> GetPosition() => new []{Text.Position.X, Text.Position.Y};

        public void SetPosition(float x, float y)
        {
            Text.Position = new Vector2f(x, y);
        }

        public void SetDisplayString(string s)
        {
            Text.DisplayedString = s;
        }

        public string GetDisplayString() => Text.DisplayedString;

        public void SetFillColor(byte r, byte g, byte b, byte a)
        {
            Text.FillColor = new Color(r, g, b, a);
        }

        public Tuple<byte, byte, byte, byte> GetFillColor()
        {
            return new Tuple<byte, byte, byte, byte>(Text.FillColor.R,
                Text.FillColor.G,
                Text.FillColor.B,
                Text.FillColor.A);
        }

    }
}