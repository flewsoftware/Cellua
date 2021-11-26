using System;
using Cellua.Api;
using Cellua.Simulation;
using SFML.Graphics;
using SFML.System;
using SFML.Window;

var scene = new Scene(new SceneInfo(800));

RenderWindow window = new(new VideoMode(scene.SceneInfo.Size, scene.SceneInfo.Size), "test");
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


ScriptManager.RegisterTypes();
MoonSharp.Interpreter.Script.WarmUp();
ScriptManager sm = new(scene);
sm.LoadFromFolder("./plugins");


#region ApiEvents
sm.WindowApi.TitleChangedE += (_, newTitle) =>
{
    window.SetTitle(newTitle);
};
sm.WindowApi.ClearE += (_, _) =>
{
    window.Clear();
};
sm.WindowApi.CloseE += (_, _) =>
{
    window.Close();
};
sm.WindowApi.FramerateLimitChangedE += (_, limit) =>
{
    window.SetFramerateLimit(limit);
};
#endregion

var s = sm.NewScriptWithGlobals();
sm.RunScript(s, "main.lua");

while (window.IsOpen)
{
    window.Clear();
    
    framerateText.DisplayedString = Math.Round(1.0/dt) + " FPS";
    window.DispatchEvents();
    

    scene.UpdatePixels(true);
    scene.UpdateTexture(worldTexture);
    
    window.Draw(worldSprite);
    window.Draw(framerateText);
    
    window.Display();
    dt = clock.Restart().AsSeconds();
}
