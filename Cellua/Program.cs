using System;
using Cellua.Api.Common;
using Cellua.Core.PackageManager;
using Api = Cellua.Api;
using Cellua.Simulation;
using SFML.Graphics;
using SFML.Window;

var scene = new Scene(new SceneInfo(800));

RenderWindow window = new(new VideoMode(scene.SceneInfo.Size, scene.SceneInfo.Size), "Cellua");
window.SetFramerateLimit(0);
window.Clear();

Texture worldTexture = new(scene.SceneInfo.Size, scene.SceneInfo.Size);
Sprite worldSprite = new(worldTexture);


PackageManager pkgManger = new();
try
{
    pkgManger.LoadFromFile("./package.json");
}
catch (Exception e)
{
    Console.WriteLine("Unable to find package.json in the current directory", e);
    throw;
}

void RenderFunc(WindowObject wo)
{
    wo.Window.DispatchEvents();

    wo.Scene.Scene.UpdatePixels(true);
    wo.Scene.Scene.UpdateTexture(worldTexture);

    wo.Window.Draw(worldSprite);

    wo.Window.Display();
    wo.Dt = wo.Clock.Restart().AsSeconds();
}

Api.Lua.ScriptManagerUtils.RegisterTypes();
MoonSharp.Interpreter.Script.WarmUp();
Api.Lua.ScriptManager sm = new(scene, window, RenderFunc);

try
{
    sm.LoadMainFile(pkgManger.Config.Main);
}
catch (Exception e)
{
    Console.WriteLine("Unable to load main file", e);
    throw;
}


var s = sm.NewScriptWithGlobals(pkgManger.Config.ModulePaths);
sm.RunMainScript(s);