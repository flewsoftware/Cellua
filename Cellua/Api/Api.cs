using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using Cellua.Random;
using Cellua.Simulation;
using MoonSharp.Interpreter;
using SFML.Graphics;

namespace Cellua.Api
{
    public class SystemApi
    {
        public static void Sleep(int ms)
        {
            Thread.Sleep(ms);
        }
    }
    
    public class SceneApi
    {
        [MoonSharpHidden] public Scene Scene;

        public SceneApi(Scene scene)
        {
            Scene = scene;
        }

        public void SetColor(uint x, uint y, byte r, byte g, byte b, byte a)
        {
            Scene.Tiles.Data[x, y].Color.R = r;
            Scene.Tiles.Data[x, y].Color.G = g;
            Scene.Tiles.Data[x, y].Color.B = b;
            Scene.Tiles.Data[x, y].Color.A = a;
            Scene.ChangeMap.Data[x, y] = true;
        }

        public void SetTileTypeId(uint x, uint y, uint typeid)
        {
            Scene.Tiles.Data[x, y].TypeId = typeid;
        }

        public uint GetTileTypeId(uint x, uint y)
        {
            return Scene.Tiles.Data[x, y].TypeId;
        }
    }
    
    public class WindowApi
    {
        [MoonSharpHidden] public RenderWindow Window;

        [MoonSharpHidden] public Action DisplayFunc;

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
    
    public class RandomApi
    {
        public RandomBool NewRandomBool()
        {
            return new RandomBool();
        }
        public RandomBool NewRandomBool(int seed)
        {
            return new RandomBool(seed);
        }
    }

    public class ScriptManager
    {
        public Dictionary<string, string> Scripts;

        public readonly RandomApi RandomApi;
        public readonly WindowApi WindowApi;
        public readonly SystemApi SystemApi;
        public SceneApi SceneApi;

        public ScriptManager(Scene scene, RenderWindow window, Action renderFunc)
        {
            SceneApi = new SceneApi(scene);
            RandomApi = new RandomApi();
            WindowApi = new WindowApi(window, renderFunc);
            SystemApi = new SystemApi();
            Scripts = new Dictionary<string, string>();
        }
        
        public static void RegisterTypes()
        {
            UserData.RegisterType<RandomApi>();
            UserData.RegisterType<RandomBool>();
            UserData.RegisterType<WindowApi>();
            UserData.RegisterType<SceneApi>();
            UserData.RegisterType<SystemApi>();
        }
        
        public Script NewScriptWithGlobals()
        {
            return new Script
            {
                Globals =
                {
                    ["Random"] = RandomApi,
                    ["Window"] = WindowApi,
                    ["Scene"] = SceneApi,
                    ["System"] = SystemApi
                }
            };
        }

        public void LoadFromFolder(string path)
        {
            var files = Directory.GetFiles(path, "**.lua");
            foreach (var file in files)
            {
                var fileStream = File.ReadAllText(file);
                Scripts[Path.GetFileName(file)] = fileStream;
            }
        }

        public DynValue RunScript(Script s, string name)
        {
            return s.DoString(Scripts[name]);
        }
    }
}