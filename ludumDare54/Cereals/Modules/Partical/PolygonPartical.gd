class_name PolygonPartical
extends Polygon2D


var _size:float = 100
var _points:PoolVector2Array


func ToPoints(p:PoolVector2Array,s:float = 1):
	_points = p
	Size(s)


func ToRegularPolygon(n:int,s:float = 1, r:float = 0)->PoolVector2Array:
	_points = UFM.RegularPolygonPoints(n,r,100)
	Size(s)
	return _points
	

func Size(s:float):
	_size = s
	
	polygon = _scaledPoints(_points,_size)
	

func _scaledPoints(p:PoolVector2Array,s:float)->PoolVector2Array:
	var res = PoolVector2Array()
	for i in p:
		res.append(i*s)
	return res


#func _ready():
	#ToRegularPolygon(7)


#func _process(delta):
	#pass
