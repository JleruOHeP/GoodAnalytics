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

var calculateY = function (y, canvas) {
    var rect = canvas.getBoundingClientRect();
    return y - rect.top;
};

var calculateX = function (y, canvas) {
    var rect = canvas.getBoundingClientRect();
    return y - rect.left;
};

var deleteLineConnections = function (canvas, lineId) {
    canvas.forEachObject(function (obj) {
        if (obj.lineStarts) {
            var index = obj.lineStarts.indexOf(lineId);
            if (index > -1) {
                obj.lineStarts.splice(index, 1);
            }
        }

        if (obj.lineEnds) {
            var index = obj.lineEnds.indexOf(lineId);
            if (index > -1) {
                obj.lineEnds.splice(index, 1);
            }
        }
    });
};

var refreshConnectingLines = function(canvas, element){
    var bound = element.getBoundingRect();
    var centerX = bound.left + bound.width / 2;
    var centerY = bound.top + bound.height / 2;


    if (element.lineStarts) {
        for (var i = 0; i < element.lineStarts.length; i++) {
            var line = getObjectById(canvas, element.lineStarts[i]);
            line.set({ 'x1': centerX, 'y1': centerY });
            line.setCoords();
        }
    }

    if (element.lineEnds) {
        for (var i = 0; i < element.lineEnds.length; i++) {
            var line = getObjectById(canvas, element.lineEnds[i]);
            line.set({ 'x2': centerX, 'y2': centerY });
            line.setCoords();
        }
    }
};

var refreshAllLines = function (canvas) {
    canvas.forEachObject(function (obj) {
        if (obj.lineStarts || obj.lineEnds) {
            refreshConnectingLines(canvas, obj);
        }
    });
};

export {getObjectByCoordinates, getNextId,  getObjectById, calculateX, calculateY, deleteLineConnections, refreshConnectingLines, refreshAllLines}