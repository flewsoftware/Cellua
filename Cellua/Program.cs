using System;
using Cellua.Api;
using Cellua.Simulation;
using SFML.Graphics;
using SFML.System;
using SFML.Window;

var scene = new Scene(new SceneInfo(800));

RenderWindow window = new(new VideoMode(scene.SceneInfo.Size, scene.SceneInfo.Size), "Cellua");
window.SetFramerateLimit(0);
window.Clear();

Texture worldTexture = new(scene.SceneInfo.Size, scene.SceneInfo.Size);
Sprite worldSprite = new(worldTexture);

Clock clock = new();
var dt = 0.0;

#region UI
var framerateText = new Text("", new Font("res/OpenSans-Regular.ttf"));
#endregion

#region Events
window.Closed += (_, _) => { window.Close(); };
#endregion

void RenderFunc()
{
    framerateText.DisplayedString = Math.Round(1.0 / dt) + " FPS";
    window.DispatchEvents();

    scene.UpdatePixels(true);
    scene.UpdateTexture(worldTexture);

    window.Draw(worldSprite);
    window.Draw(framerateText);

    window.Display();
    dt = clock.Restart().AsSeconds();
}

ScriptManager.RegisterTypes();
MoonSharp.Interpreter.Script.WarmUp();
ScriptManager sm = new(scene, window, RenderFunc);
sm.LoadFromFolder("./plugins");


var s = sm.NewScriptWithGlobals();
sm.RunScript(s, "main.lua");