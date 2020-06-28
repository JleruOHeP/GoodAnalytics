import { lineArrow } from './FabricClasses.js' 

var LineDrawer = function (canvas) {
    var currentLine;

    this.start = function (x, y, target) {
        var currentId = 1;
        canvas.forEachObject(function (obj) {
            if (obj.id && obj.id >= currentId) {
                currentId = obj.id + 1;
            }
        });
        
        currentLine = new fabric.LineArrow([x, y, x, y],
        {
            strokeWidth: 5,
            fill: 'black',
            stroke: 'black',
            originX: 'center',
            originY: 'center',
            hasBorders: false,
            hasRotatingPoint: false,
            perPixelTargetFind: true,
            heads: [1, 0],
            lockMovementX: true,
            lockMovementY: true,
            id: currentId
        });

        canvas.add(currentLine);

        if (target && target.lineStarts) {
            target.lineStarts.push(currentLine.id);

            var bound = target.getBoundingRect();
            var centerX = bound.left + bound.width / 2;
            var centerY = bound.top + bound.height / 2;

            currentLine.set({ 'x1': centerX, 'y1': centerY });
        }
    };

    this.continue = function(x, y) {
        if (!currentLine) return;

        currentLine.set({ 'x2': x, 'y2': y });

        canvas.renderAll();
    };

    this.finish = function(x, y, target) {
        if (!currentLine) return;
        
        if (Math.abs(currentLine.x2 - currentLine.x1) > 10 || Math.abs(currentLine.y2 - currentLine.y1) > 10) {
            currentLine.set({ 'x2': x, 'y2': y });

            if (target && target.lineEnds) {
                target.lineEnds.push(currentLine.id);

                var bound = target.getBoundingRect();
                var centerX = bound.left + bound.width / 2;
                var centerY = bound.top + bound.height / 2;

                currentLine.set({ 'x2': centerX, 'y2': centerY });
            }
            currentLine.setCoords();
        } else {
            canvas.remove(currentLine);
        }

        currentLine = null;
    };
};

export {LineDrawer}