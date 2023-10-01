class_name InitItemsCountroler
extends Node


var _items


func Disapper():
	for i in _items:
		i.Start()
	TimeManager.DelayFunction(funcref(get_parent().get_parent(),"queue_free"),3)


func _ready():
	_items = get_children()


func _process(delta):
	pass
