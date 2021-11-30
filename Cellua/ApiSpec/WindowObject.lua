---@class WindowObject
WindowObject = {}

--- Changes the title of the window
---@param title string
function WindowObject.SetTitle(title) end

--- Closes the window
function WindowObject.Close() end

--- Clears the window screen
function WindowObject.Clear() end

--- Changes the framerate limit
---@param limit number
function WindowObject.SetFramerateLimit(limit) end

--- Returns true if the window isOpened
---@return boolean
function WindowObject.IsOpen() end

--- Displays the scene
function WindowObject.Display() end

--- Returns the framerate
---@return number
function WindowObject.GetFramerate() end

--- Returns a SceneObject
---@return SceneObject
function WindowObject.GetScene() end