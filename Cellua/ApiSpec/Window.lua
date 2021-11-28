Window = {}

--- Changes the title of the Cellua Window
---@param title string
function Window.SetTitle(title) end

--- Closes the Cellua window
function Window.Close() end

--- Clears the Cellua screen
function Window.Clear() end

--- Changes the framerate limit
---@param limit number
function Window.SetFramerateLimit(limit) end

--- Returns true if the window isOpened
---@return boolean
function Window.IsOpen() end

--- Displays the scene
function Window.Display() end