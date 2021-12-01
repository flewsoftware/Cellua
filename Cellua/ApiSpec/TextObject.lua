---@class TextObject
TextObject = {}


--- Changes the text font
---@param font FontObject
function TextObject.UseFont(font) end


--- Returns the position of the text in screen
---@return table x & y positions
function TextObject.GetPosition() end


--- Changes the position of the text
---@param x number x position of the text
---@param y number y position of the text
function TextObject.SetPosition(x, y) end


--- Changes the display string of the text
---@param s string string to display
function TextObject.SetDisplayString(s) end

--- Returns the display string of the text
---@return string display string
function TextObject.GetDisplayString() end


--- Changes the text fill color
---@param r number red
---@param g number green
---@param b number blue
---@param a number alpha
function TextObject.SetFillColor(r,g,b,a) end


--- Returns the text fill color
---@return number,number,number,number rgba
function TextObject.GetFillColor() end