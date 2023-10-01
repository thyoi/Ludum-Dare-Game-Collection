class_name RotateCountroler
extends Node




var _item : ShrinkDisapper

export var _speed:float = 0.06
var _center:Vector2 = Vector2(480,270)
var _r:float
var _l:float
export var _rotateSpeed : float = 0.24
var _onStart:bool = true


func Start():
	_item.Start()
	_onStart = false


func _ready():
	_item = get_child(0)
	var tem = _item.position - _center
	_l = tem.length()
	_r = UFM.VectorRadin(tem/_l)


func _process(delta):
	if(_onStart):
		_r+=_speed*delta
		if(_r>2*PI):
			_r-=2*PI
		_item.position = UFM.RadinVector(_r)*_l+_center
	
		var tem = _item.rotation + _rotateSpeed*delta
		if(tem>2*PI):
			tem-=2*PI
		_item.rotation = tem
