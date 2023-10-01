class_name DialogueBox
extends CanvasLayer

export var MaxSize:Vector2


var _line :Line2D
var _back : Polygon2D
var _backLine : Line2D
var _yOffset : float



var _sizeFollow : Vector2FollowAnimation
var _points : PoolVector2Array
var _curPoint :PoolVector2Array
var _label : Label


func TextYOffset(o:float):
	_yOffset = o


func Disapper():
	Scale(Vector2.ZERO)
	SetText("")


func Position(p:Vector2):
	offset = p
	
func Scale(s:Vector2):
	_sizeFollow.End = s
	


func SetText(s:String):
	_label.text = s


func _initPoint(s:Vector2,r:float,c:int):
	var c1 = PoolVector2Array()
	var c2 = PoolVector2Array()
	var c3 = PoolVector2Array()
	var c4 = PoolVector2Array()
	var angle = PI/2/(c+1)
	var ainit = -PI + angle
	var o1 = Vector2(-s.x/2+r,s.y/2-r)
	var o2 = Vector2(s.x/2-r,s.y/2-r)
	var o3 = Vector2(s.x/2-r,-s.y/2+r)
	var o4 = Vector2(-s.x/2+r,-s.y/2+r)
	for i in c:
		ainit += angle
		c1.append(Vector2(cos(ainit),-sin(ainit))*r+o1)
	ainit += angle	
	for i in c:
		ainit += angle
		c2.append(Vector2(cos(ainit),-sin(ainit))*r+o2)
	ainit += angle	
	for i in c:
		ainit += angle
		c3.append(Vector2(cos(ainit),-sin(ainit))*r+o3)
	ainit += angle	
	for i in c:
		ainit += angle
		c4.append(Vector2(cos(ainit),-sin(ainit))*r+o4)

	_points = PoolVector2Array()
	_points.append(Vector2(-s.x/2,s.y/2-r))
	_points.append_array(c1)
	_points.append(Vector2(s.x/2-r,s.y/2))
	_points.append_array(c2)
	_points.append(Vector2(s.x/2,-s.y/2+r))
	_points.append_array(c3)
	_points.append(Vector2(-s.x/2+r,-s.y/2))
	_points.append_array(c4)
	_points.append(Vector2(-s.x/2,-s.y/2+r))
	_points.append(Vector2(-s.x/2,s.y/2-r))
	


func _myInit():
	_back = get_node("Polygon2D")
	_backLine = get_node("Line2D")
	_line = get_node("DIalogueBox")
	_label = get_node("Label")
	_sizeFollow = Vector2FollowAnimation.new()
	_sizeFollow.Speed = 13

	_initPoint(MaxSize,30,10)
	_curPoint = PoolVector2Array()
	_curPoint.append_array(_points)



	

func _updatePoints(dt:float):
	_setSize(_sizeFollow.Shift(dt))
	
func _setSize(s:Vector2):
	for i in _points.size():
		_curPoint[i] = Vector2(_points[i].x*s.x,_points[i].y*s.y)
	_back.polygon = _curPoint
	_line.points = _curPoint
	_backLine.points = _curPoint
	_label.rect_position = Vector2(-MaxSize.x/2+30,-9+_yOffset)
	if s.y*MaxSize.y<20:
		_backLine.width = s.y*MaxSize.y
	else:
		_backLine.width = 20
	if s.y*MaxSize.y<5:
		_line.width = s.y*MaxSize.y
	else:
		_line.width = 5
	
func _ready():
	_myInit()


func _process(delta):
	_updatePoints(delta)
