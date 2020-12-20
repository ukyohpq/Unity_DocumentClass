local poolKey = {}
local putFunc = function(self)
    table.insert(self.class[poolKey], self)
end

function poolClass(classname, super)
    local cls = class(classname, super)
    local pool = {}
    cls[poolKey] = pool
    local newFunc = cls.New
    cls.New = function(...)
        local instance = table.remove(pool, 1)
        if instance == nil then
            instance = newFunc(...)
        end
        return instance
    end
    cls.put = putFunc
    cls.__gc = putFunc
end