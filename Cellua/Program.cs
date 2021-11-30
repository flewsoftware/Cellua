using Cellua.Api.Common;
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
sm.LoadFromFolder("./scripts");


var s = sm.NewScriptWithGlobals();
sm.RunScript(s, "main.lua");