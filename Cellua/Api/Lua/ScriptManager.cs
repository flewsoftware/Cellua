using System;
using System.IO;
using Cellua.Api.Common;
using Cellua.Api.CommonManager;
using Cellua.Random;
using Cellua.Simulation;
using MoonSharp.Interpreter;
using MoonSharp.Interpreter.Loaders;

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
            UserData.RegisterType<FontApi>();
            UserData.RegisterType<FontObject>();
            UserData.RegisterType<TextApi>();
            UserData.RegisterType<TextObject>();
        }
    }

    public class ScriptManager
    {
        public string MainScript;

        public readonly RandomApi RandomApi;
        public readonly WindowApi WindowApi;
        public readonly SystemApi SystemApi;
        public readonly FontApi FontApi;
        public readonly TextApi TextApi;
        
        public ScriptManager(Scene scene, Action<WindowObject> renderFunc, FontManger fontManger)
        {
            RandomApi = new RandomApi();
            WindowApi = new WindowApi(renderFunc);
            SystemApi = new SystemApi();
            MainScript = "";
            FontApi = new FontApi(fontManger);
            TextApi = new TextApi();
        }
        
        public Script NewScriptWithGlobals(string[] modulePath)
        {
           var s = new Script
            {
                Globals =
                {
                    ["Random"] = RandomApi,
                    ["Window"] = WindowApi,
                    ["System"] = SystemApi,
                    ["Font"] = FontApi,
                    ["Text"] = TextApi
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