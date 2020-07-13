// Extended fabric line class
var initCustomClasses = function() {

  fabric.LineArrow = fabric.util.createClass(fabric.Line, {

      type: 'lineArrow',
    
      initialize: function(points, options) {
        options || (options = {});
        this.callSuper('initialize', points, options);
        this.set('id', options.id || '');
        this.set('heads', options.heads || [1, 0]);
        this.set('toId', options.toId);
        this.set('fromId', options.fromId);

        this.hasControls = false;

        this.lockMovementY = this.lockMovementX = true;
        this.lockScalingX = this.lockScalingY = true;
      },
    
      toObject: function() {
        return fabric.util.object.extend(this.callSuper('toObject'), {
          id: this.get('id'),
          heads: this.get('heads'),
          toId: this.get('toId'),
          fromId: this.get('fromId')
        });
      },
    
      _render: function(ctx) {
        this.ctx = ctx;
        this.callSuper('_render', ctx);
        let p = this.calcLinePoints();
        let xDiff = this.x2 - this.x1;
        let yDiff = this.y2 - this.y1;
        let angle = Math.atan2(yDiff, xDiff);
        this.drawArrow(angle, p.x2, p.y2, this.heads[0]);
        ctx.save();
        xDiff = -this.x2 + this.x1;
        yDiff = -this.y2 + this.y1;
        angle = Math.atan2(yDiff, xDiff);
        this.drawArrow(angle, p.x1, p.y1,this.heads[1]);
      },
    
      drawArrow: function(angle, xPos, yPos, head) {
        this.ctx.save();
      
        if (head) {
            this.ctx.translate(xPos, yPos);
            this.ctx.rotate(angle);
          this.ctx.beginPath();
          // Move 5px in front of line to start the arrow so it does not have the square line end showing in front (0,0)
          this.ctx.moveTo(10, 0);
          this.ctx.lineTo(-15, 15);
          this.ctx.lineTo(-15, -15);
          this.ctx.closePath();
        }
        
        this.ctx.fillStyle = this.stroke;
        this.ctx.fill();
        this.ctx.restore();
      }
  });
  
  fabric.LineArrow.async = true;

  fabric.LineArrow.fromObject = function(object, callback) {
    let x1 = object.left + (object.x1 > 0 ? object.width : 0);
    let x2 = object.left + (object.x2 > 0 ? object.width: 0);
    let y1 = object.top + (object.y1 > 0 ? object.height : 0);
    let y2 = object.top + (object.y2 > 0 ? object.height : 0);

    let line = new fabric.LineArrow([x1, y1, x2, y2], object);

    //callback && callback(new fabric.LineArrow([object.x1, object.y1, object.x2, object.y2], object));
     fabric.util.enlivenObjects(object.objects, function (enlivenedObjects) {
       delete object.objects;
       callback && callback(line);
     });
  };

  fabric.Process = fabric.util.createClass(fabric.Rect, {

    type: 'process',

    initialize: function (options) {
        options || (options = {});

        this.callSuper('initialize', options);
        this.set('actions', options.actions || []);
        this.set('lineStarts', options.lineStarts || []);
        this.set('lineEnds', options.lineEnds || []);
    },

    toObject: function () {
        return fabric.util.object.extend(this.callSuper('toObject'), {
            lineStarts: this.get('lineStarts'),
            lineEnds: this.get('lineEnds')
        });
    },

    _render: function (ctx) {
        this.callSuper('_render', ctx);
    }
  });

  fabric.Process.fromObject = function (object, callback) {
      var rect = new fabric.Process(object);
      callback && callback(rect);
      return rect;
  };

  fabric.Event = fabric.util.createClass(fabric.Circle, {

      type: 'event',

      initialize: function (options) {
          options || (options = {});

          this.callSuper('initialize', options);
          this.set('probability', options.probability || 100);
          this.set('lineStarts', options.lineStarts || []);
          this.set('lineEnds', options.lineEnds || []);
      },

      toObject: function () {
          return fabric.util.object.extend(this.callSuper('toObject'), {
              lineStarts: this.get('lineStarts'),
              lineEnds: this.get('lineEnds')
          });
      },
      
      _render: function (ctx) {
          this.callSuper('_render', ctx);
      }
  });

  fabric.Event.fromObject = function (object, callback) {
      var circle = new fabric.Event(object);
      callback && callback(circle);
      return circle;
  };

};
  
export { initCustomClasses };