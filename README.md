# What is Cellua?
Cellua is a Cellular automata engine that can be scripted with lua to quickly prototype and experiment with different rules.

# How do I use it?
Cellua is currently WIP so the API might change without warning but feel free to experiment with it.   
You will have to build binaries and run it inside a directory that has ``./plugins/main.lua``.   
Cellua Lua API is fully documented inside the ApiSpec directory, it can be copied to ``./plugins`` when programming in an IDE for code auto-completion(Its recommended to use EmmyLua).


# Examples
````lua
-- changes x:500 y:500 cell's color to red
Scene.SetColor(500, 500, 255, 0, 0, 255)
````

````lua
--- draw a line across the screen
for i = 1, 799 do
    Scene.SetColor(i, i, 255, 0, 0, 255)
end
````
````lua
--- draw a line across the screen but only draw the pixel if the random boolean is true
b = Random.NewRandomBool()
for i = 1, 799 do
    if b.NextBoolean() then
        Scene.SetColor(i, i, 255, 0, 0, 255)
    end
end
````