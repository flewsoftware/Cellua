using System;
using System.Collections.Generic;
using System.IO;
using Cellua.Api.Common;
using Cellua.Random;
using Cellua.Simulation;
using MoonSharp.Interpreter;
using SFML.Graphics;

namespace Cellua.Api.Lua
{
    public static class ScriptManagerUtils
    {
        public static void RegisterTypes()
        {
            UserData.RegisterType<RandomApi>();
            UserData.RegisterType<RandomBool>();
            UserData.RegisterType<WindowApi>();
            UserData.RegisterType<SceneObject>();
            UserData.RegisterType<SystemApi>();
            UserData.RegisterType<WindowObject>();
        }
    }
    
    public class ScriptManager
    {
        public Dictionary<string, string> Scripts;

        public readonly RandomApi RandomApi;
        public readonly WindowApi WindowApi;
        public readonly SystemApi SystemApi;

        public ScriptManager(Scene scene, RenderWindow window, Action<WindowObject> renderFunc)
        {
            RandomApi = new RandomApi();
            WindowApi = new WindowApi(window, renderFunc);
            SystemApi = new SystemApi();
            Scripts = new Dictionary<string, string>();
        }
        
        public Script NewScriptWithGlobals()
        {
            return new Script
            {
                Globals =
                {
                    ["Random"] = RandomApi,
                    ["Window"] = WindowApi,
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