class_name MyButton
extends Polygon2D


var _line:LinePartical
var _sprite:Sprite
var _sizeFollow : FloatFollowAnimation
var _points :PoolVector2Array
var _pointss:PoolVector2Array
var _onShow :bool = false
var _clickCallBack :FuncRef


func Show():
	_onShow = true
	_sizeFollow.Cur = 0
	_sizeFollow.End = 1;
	z_index = 10
	
func Hide():
	_onShow = false
	_sizeFollow.End = -0.4
	z_index = 0




func _setSize(s:float):
	if s<0:
		s=0
	_sprite.scale = Vector2(s,s)
	if(s>1):
		_line.Size(s*1.2)
		_line.rotation = 2*PI*(s-1.1)*10
		
		rotation = PI*(s-1.1)*-5
		_sprite.rotation = PI*(s-1.1)*5
		s+=(s-1)*5
	else:
		_line.Size(s)
		_line.rotation = 3*PI*(s)
	for i in _points.size():
		_pointss[i] = s*_points[i]
	polygon  = _pointss


func _input(event):
	if event is InputEventMouseButton:
		if event.button_index == BUTTON_LEFT  :
			if event.pressed:
				if _onShow:
					if UFM.PointInArea(MouseManager._mousePosition,position,Vector2(80,80)):
						if _clickCallBack !=null:
							_clickCallBack.call_func()
							SParticalGenerater.ParticalBoomR(position,["rp0"],0.7,1.4,30,color)
							SoundManager.Play("bb")


func _checkHover():
	if _onShow:
		if UFM.PointInArea(MouseManager._mousePosition,position,Vector2(80,80)):
			_sizeFollow.End = 1.1
		else:
			_sizeFollow.End = 1



func _ready():
	_sizeFollow = FloatFollowAnimation.new()
	_sizeFollow.Speed = 14
	_line = get_node("Line2D")
	_sprite = get_node("Sprite")
	_points = PoolVector2Array()
	var tem = 55
	_points.append(Vector2(tem,0))
	_points.append(Vector2(0,tem))
	_points.append(Vector2(-tem,0))
	_points.append(Vector2(0,-tem))
	_pointss = PoolVector2Array()
	_pointss.append_array(_points)
	
	
	


func _process(delta):
	_setSize(_sizeFollow.Shift(delta))
	_checkHover()
