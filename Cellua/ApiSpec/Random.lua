Random = {}

--- Returns a new RandomBool Object
---@overload fun(seed: number):RandomBool
---@return RandomBool
function Random.NewRandomBool() end

---
---@class RandomBool
RandomBool = {}

--- Returns a random boolean
---@return boolean
function RandomBool.NextBoolean() end
