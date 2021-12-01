using System;
using System.Collections.Generic;
using Cellua.Simulation;
using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace Cellua.Api.Common
{
    public class WindowObject
    {
        public readonly RenderWindow Window;
        public readonly Action<WindowObject> DisplayFunc;
        public double Dt;
        public readonly Clock Clock;
        public readonly SceneObject Scene;
        public List<TextObject> Texts;
        
        public WindowObject(RenderWindow renderWindow, Action<WindowObject> displayFunc)
        {
            Window = renderWindow;
            DisplayFunc = displayFunc;
            Window.Closed += (_, _) =>
            {
                Window.Close();
            };
            Clock = new();
            Scene = new SceneObject(new Scene(new SceneInfo(renderWindow.Size.X)));
            Texts = new List<TextObject>();
        }

        public SceneObject GetScene() => Scene;

        public bool IsOpen() => Window.IsOpen;

        public void SetTitle(string title) => Window.SetTitle(title);

        public void Display() => DisplayFunc(this);

        public void Close() => Window.Close();

        public void Clear() => Window.Clear();

        public void SetFramerateLimit(uint framerate) => Window.SetFramerateLimit(framerate);

        public double GetFramerate() => 1.0 / Dt;

        public void AddText(TextObject textObject)
        {
            Texts.Add(textObject);
        }

        public void RemoveText(TextObject textObject)
        {
            Texts.Remove(textObject);
        }
    }
    
    public class WindowApi
    {
        public RenderWindow Window;
        public readonly Action<WindowObject> DisplayFunc;

        public WindowApi(RenderWindow window, Action<WindowObject> displayFunc)
        {
            Window = window;
            DisplayFunc = displayFunc;
        }

        public WindowObject NewWindow(uint size, string title)
        {
            return new WindowObject(
                new RenderWindow(
                    new VideoMode(size, size),
                    title
                ),
                DisplayFunc
            );
        }
    }
}