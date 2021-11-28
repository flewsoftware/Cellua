using System;
using System.Collections.Generic;
using System.IO;
using Cellua.Random;
using Cellua.Simulation;
using MoonSharp.Interpreter;

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
        [MoonSharpHidden] public event EventHandler<uint>? FramerateLimitChangedE;
        [MoonSharpHidden] public event EventHandler<string>? TitleChangedE;
        [MoonSharpHidden] public event EventHandler? ClearE;
        [MoonSharpHidden] public event EventHandler? CloseE; 

        public void SetTitle(string title)
        {
            TitleChangedE?.Invoke(this, title);
        }

        public void Close()
        {
            CloseE?.Invoke(this, EventArgs.Empty);
        }
        public void Clear()
        {
            ClearE?.Invoke(this, EventArgs.Empty);
        }
        public void SetFramerateLimit(uint framerate)
        {
            FramerateLimitChangedE?.Invoke(this, framerate);
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
        public Dictionary<string, string> Scripts = new();

        public readonly RandomApi RandomApi;
        public readonly WindowApi WindowApi;
        public SceneApi SceneApi;

        public ScriptManager(Scene scene)
        {
            SceneApi = new SceneApi(scene);
            RandomApi = new RandomApi();
            WindowApi = new WindowApi();
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