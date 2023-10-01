class_name BackgroundManager
extends CanvasLayer


const _ball = preload("res://GameAsset/prefab/BallM.tscn")


var _back:Sprite
var _viewPort : Viewport
var _sp:Sprite
var _color:Color


func ColorCallback():
	_back.material.set_shader_param("c",_color)

func Boom(c:Color):
	_sp.material.set_shader_param("c",c)
	_color = c
	TimeManager.DelayFunction(funcref(self,"ColorCallback"),1.9)
	var tem = _ball.instance() as ShrinkDisapper
	tem.Start()
	_viewPort.add_child(tem)


func _ready():
	_back = get_node("Sprite")
	_viewPort = get_node("show2/Viewport2")
	_sp = get_node("show2")


func _process(delta):
	pass
