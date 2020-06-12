var getObjectByCoordinates = function (canvas, x, y) {
    var object = null,
        objects = canvas.getObjects();

    for (var i = 0, len = canvas.size() ; i < len; i++) {
        if (objects[i].type !== 'lineArrow') {
            var bound = objects[i].getBoundingRect();
            if (bound.left < x && x < (bound.left + bound.width) && 
                bound.top < y && y < (bound.top + bound.height)){
                    object = objects[i];
                    break;
                }
        }
    }

    return object;
};

var getNextId = function(canvas) {
    var currentId = 1;
    canvas.forEachObject(function (obj) {
        if (obj.id && obj.id >= currentId) {
            currentId = obj.id + 1;
        }
    });

    return currentId;
};

var getObjectById = function (canvas, id) {
    var object = null,
        objects = canvas.getObjects();

    for (var i = 0, len = canvas.size() ; i < len; i++) {
        if (objects[i].id && objects[i].id === id) {
            object = objects[i];
            break;
        }
    }

    return object;
};


export {getObjectByCoordinates, getNextId,  getObjectById}