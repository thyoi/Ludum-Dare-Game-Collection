class_name RoundPartical
extends Line2D



export var MaxSize:float = 200
export var MaxLine:float = 20
export var SizeCurve : Curve
export var LineCurve : Curve
export var Duration :float = 0.5


var _points : PoolVector2Array 
var _count = 0

func _setPoint(s:float):
	var tem = PoolVector2Array()
	for i in _points:
		tem.append(i*s)
	points = tem


func Init():
	_points = PoolVector2Array()
	for i in 41:
		_points.append(Vector2(sin(i*PI/20),cos(i*PI/20))*MaxSize)


func _ready():
	pass


func _process(delta):
	_count +=delta
	if _count>=Duration:
		queue_free()
		_count = Duration
	_setPoint(SizeCurve.interpolate(_count/Duration))
	width = LineCurve.interpolate(_count/Duration)*MaxLine
	
