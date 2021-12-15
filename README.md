# What is Cellua?
Cellua is a fast cell based script-able graphics engine for grid based graphics

# Usage
1. Download the latest binary
2. Add it to PATH
3. Clone [cellua project template](https://github.com/flew-software/CelluaProject)
4. Run Cellua in the directory

# Example
![](res/demo.gif)
```lua
window = Window.NewWindow(800, "Demo")
scene = window.GetScene()
ran = Random.NewRandomBool()

for x = 1, 799 do
    for y = 1, 799 do
        if ran.NextBoolean() then
            scene.SetColor(x, y, 255, 0, 0, 255)
        end
    end
    window.Clear()
    window.Display()
end

while window.IsOpen() do
    window.Clear()
    window.Display()
end

```
