class_name LinePartical
extends Line2D


var _size:float = 0
export var Points:PoolVector2Array
export var DefaultColor:Color
export(float,0,1) var DefaultApha
export var LineW:float
export var InitW : float
export var drawInStart : bool


func ToPoints(p:PoolVector2Array,s:float = 1):
	Points = p
	Size(s)


func ToRegularPolygon(n:int,s:float = 1, baseSize: float = 100,r:float = 0)->PoolVector2Array:
	Points = UFM.LoopPoints(UFM.RegularPolygonPoints(n,r,baseSize))
	Size(s)
	return Points
	

func Size(s:float):
	_size = s
	points = _scaledPoints(Points,_size)
	var tem = s*InitW
	if(tem<LineW):
		width = tem
	else:
		width = LineW
	

func _scaledPoints(p:PoolVector2Array,s:float)->PoolVector2Array:
	var res = PoolVector2Array()
	for i in p:
		res.append(i*s)
	return res


func _ready():
	if drawInStart:
		ToRegularPolygon(24,1,24,0)



#func _process(delta):
	#pass
