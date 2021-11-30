using System;
using SFML.Graphics;

namespace Cellua.Api.Common
{
    public class WindowApi
    {
        public RenderWindow Window;

        public Action DisplayFunc;

        public WindowApi(RenderWindow window, Action displayFunc)
        {
            Window = window;
            DisplayFunc = displayFunc;
        }

        public bool IsOpen()
        {
            return Window.IsOpen;
        }
        
        public void SetTitle(string title)
        {
            Window.SetTitle(title);
        }

        public void Display()
        {
            DisplayFunc();
        }
        public void Close()
        {
            Window.Close();
        }
        public void Clear()
        {
            Window.Clear();
        }
        public void SetFramerateLimit(uint framerate)
        {
            Window.SetFramerateLimit(framerate);
        }
    }
}