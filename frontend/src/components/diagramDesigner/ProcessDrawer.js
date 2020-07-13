var ProcessDrawer = function(canvas) {
    var currentRect;
    var initialX = 0;
    var initialY = 0;

    this.start = function (x, y, id) {
        currentRect = new fabric.Process({
            left: x,
            top: y,
            width: 0,
            height: 0,

            fill: 'red',

            id: id,

            hasRotatingPoint: false,
            lockUniScaling: true,
            lineStarts: [],
            lineEnds: []
        });

        initialX = x;
        initialY = y;

        canvas.add(currentRect);
    };

    this.continue = function(x, y) {
        if (!currentRect) return;

        var width = x - initialX;
        var height = y - initialY;

        currentRect.set({ 'width': Math.abs(width), 'height': Math.abs(height) });

        if (width < 0) {
            currentRect.set({ 'left': x });
        } 

        if (height < 0) {
            currentRect.set({ 'top': y });
        }

        canvas.renderAll();
    };

    this.finish = function(x, y) {
        if (!currentRect) return;
        
        var width = x - initialX;
        var height = y - initialY;


        if (Math.abs(width) > 10 && Math.abs(height) > 10) {
            currentRect.set({ 'width': Math.abs(width), 'height': Math.abs(height) });

            if (width < 0) {
                currentRect.set({ 'left': x });
            }

            if (height < 0) {
                currentRect.set({ 'top': y });
            }

            currentRect.lockMovementX = true;
            currentRect.lockMovementY = true;
            currentRect.setCoords();
        } else {
            canvas.remove(currentRect);
        }

        currentRect = null;
    };
};

export {ProcessDrawer}