class_name ShrinkDisapperAdvance
extends Node2D


export var StartPosition:Vector2
export var EndPosition:Vector2


export var RotateMax:float
export var AnimationCurve:Curve
export var PositionCurve:Curve
export var RotationCurve:Curve
export var AphaCurve:Curve
export var MaxSize:float  = 1

export var Duration:float
export var Keep:bool = false
export var InitSize:float

export(int,"line","polygon","sprite") var rType

var _onStart : bool = false
var _item : Node2D
var _count : float = 0


func Start():
	_onStart = true
	
	
func _updateAnimation(dt:float):
	_count +=dt
	if(_count>=Duration):
		_count = dt
		queue_free()
	_setSize(AnimationCurve.interpolate(_count/Duration)*MaxSize)
	position = UFM.Vector2Lerp(StartPosition,EndPosition,PositionCurve.interpolate(_count/Duration))
	rotation = RotationCurve.interpolate(_count/Duration)*RotateMax
	#_setApha(AphaCurve.interpolate(_count/Duration))
	
func _setApha(a:float):
	if rType==0:
		_item.default_color.a =a
	elif rType==1:
		_item.color.a =a
	else:
		(_item as Sprite).modulate.a = a
		

func _setColor(c:Color):
	if rType==0:
		_item.default_color =c
	elif rType==1:
		_item.color=c
	else:
		(_item as Sprite).modulate = c
		


func _setSize(s:float):
	if _item.has_method("Size"):
		_item.Size(s)
	else:
		scale = Vector2(s,s)


func _ready():
	_item = get_child(0)
	Start()


func _process(delta):
	if(_onStart):
		_updateAnimation(delta)
