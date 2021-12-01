using System;
using System.IO;
using Cellua.Api.Common;
using Cellua.Random;
using Cellua.Simulation;
using MoonSharp.Interpreter;
using MoonSharp.Interpreter.Loaders;
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
        public string MainScript;

        public readonly RandomApi RandomApi;
        public readonly WindowApi WindowApi;
        public readonly SystemApi SystemApi;

        public ScriptManager(Scene scene, RenderWindow window, Action<WindowObject> renderFunc)
        {
            RandomApi = new RandomApi();
            WindowApi = new WindowApi(window, renderFunc);
            SystemApi = new SystemApi();
            MainScript = "";
        }
        
        public Script NewScriptWithGlobals(string[] modulePath)
        {
           var s = new Script
            {
                Globals =
                {
                    ["Random"] = RandomApi,
                    ["Window"] = WindowApi,
                    ["System"] = SystemApi
                }
            };
           var fileSystemLoader = new FileSystemScriptLoader
           {
               ModulePaths = modulePath
           };
           s.Options.ScriptLoader = fileSystemLoader;
           return s;
        }
        
        public void LoadMainFile(string file)
        {
            var fileStream = File.ReadAllText(file);
            MainScript = fileStream;
        }
        
        public DynValue RunMainScript(Script s)
        {
            return s.DoString(MainScript);
        }
    }
}