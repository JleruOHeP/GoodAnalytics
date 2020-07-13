var EventDrawer = function (canvas){
    var currentCircle;

    var initialX = 0;
    var initialY = 0;

    this.start = function (x, y, id) {
        currentCircle = new fabric.Event({
            left: x,
            top: y,
            radius: 0,

            hasRotatingPoint: false,
            originX: 'center', originY: 'center',

            id: id,
            
            lineStarts: [],
            lineEnds: [],

            fill: 'green',
        });

        initialX = x;
        initialY = y;

        canvas.add(currentCircle);
    };

    this.continue = function(x, y) {
        if (!currentCircle) return;

        var width = x - initialX;
        var height = y - initialY;

        var radius = Math.pow( Math.pow(width, 2) + Math.pow(height, 2), 0.5) / 2;
        
        currentCircle.set({ 'radius': radius });

        if (width < 0) {
            currentCircle.set({ 'left': x });
        } 

        if (height < 0) {
            currentCircle.set({ 'top': y });
        }

        canvas.renderAll();
    };

    this.finish = function(x, y) {
        if (!currentCircle) return;
        
        var width = x - initialX;
        var height = y - initialY;
        var radius = Math.pow(Math.pow(width, 2) + Math.pow(height, 2), 0.5) / 2;

        if (radius > 5) {
            currentCircle.set({ 'radius': radius });

            if (width < 0) {
                currentCircle.set({ 'left': x });
            }

            if (height < 0) {
                currentCircle.set({ 'top': y });
            }

            currentCircle.lockMovementX = true;
            currentCircle.lockMovementY = true;
            currentCircle.setCoords();
        } else {
            canvas.remove(currentCircle);
        }

        currentCircle = null;
    };
};

export { EventDrawer };