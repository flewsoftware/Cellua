using System;
using Cellua.Api.Common;
using Cellua.Api.CommonManager;
using Cellua.Core.PackageManager;
using Api = Cellua.Api;
using Cellua.Simulation;
using MoonSharp.Interpreter;
using SFML.Graphics;

var scene = new Scene(new SceneInfo(800));


Texture worldTexture = new(scene.SceneInfo.Size, scene.SceneInfo.Size);
Sprite worldSprite = new(worldTexture);


PackageManager pkgManger = new();
try
{
    pkgManger.LoadFromFile("./package.json");
}
catch (Exception e)
{
    Console.WriteLine("Unable to load package.json.", e);
    throw;
}

FontManger fontManger = new();
pkgManger.LoadFontsTo(fontManger);

void RenderFunc(WindowObject wo)
{
    wo.Window.DispatchEvents();

    wo.Scene.Scene.UpdatePixels(true);
    wo.Scene.Scene.UpdateTexture(worldTexture);

    wo.Window.Draw(worldSprite);
    foreach (var textObject in wo.Texts)
    {
        wo.Window.Draw(textObject.Text);
    }
    
    wo.Window.Display();
    wo.Dt = wo.Clock.Restart().AsSeconds();
}

Api.Lua.ScriptManagerUtils.RegisterTypes();
MoonSharp.Interpreter.Script.WarmUp();
Api.Lua.ScriptManager sm = new(scene, RenderFunc, fontManger);


try
{
    sm.LoadMainFile(pkgManger.Config.Main);
}
catch (Exception e)
{
    Console.WriteLine("Unable to load main file.", e);
    throw;
}


var s = sm.NewScriptWithGlobals(pkgManger.Config.ModulePaths);

try
{
    sm.RunMainScript(s);
}
catch (ScriptRuntimeException runtimeException)
{
    Console.WriteLine("An runtime exception occured!\n {0}", runtimeException.DecoratedMessage);
}
catch (SyntaxErrorException syntaxError)
{    
    Console.WriteLine("An syntax error was found\n {0}", syntaxError.DecoratedMessage);
}
