class_name DelayEvent
extends Node


var _function:FuncRef
var _duration:float



func Init(f:FuncRef,d:float):
	_function = f
	_duration = d



func _ready():
	pass


func _process(delta):
	_duration-=delta
	if(_duration<=0):
		_function.call_func()
		queue_free()
