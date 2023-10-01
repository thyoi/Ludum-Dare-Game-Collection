class_name ShrinkDisapper
extends Node2D


export var AnimationCurve:Curve
export var Duration:float
export var Keep:bool = false
export var InitSize:float


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
	_setSize(AnimationCurve.interpolate(_count/Duration))

func _setSize(s:float):
	if _item.has_method("Size"):
		_item.Size(s)
	else:
		scale = Vector2(s,s)


func _ready():
	_item = get_child(0)


func _process(delta):
	if(_onStart):
		_updateAnimation(delta)
