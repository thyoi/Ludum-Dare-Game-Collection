class_name DeathCard
extends Node



var _deathCard : BaseCardContainer

var _deathMarks : Array
var _deathTotal = 10
var _deathCount = 10

var _inPlayer:bool  = false
var _inThiori:bool = false


func _showMark(n:int):
	for i in 10:
		if i<n:
			(_deathMarks[i] as Sprite).visible = true
		else:
			 (_deathMarks[i] as Sprite).visible = false



func _initMark():
	_deathMarks = []
	_deathMarks.append(get_node("BaseCardContainer/CL/font/0"))
	_deathMarks.append(get_node("BaseCardContainer/CL/font/1"))
	_deathMarks.append(get_node("BaseCardContainer/CL/font/2"))
	_deathMarks.append(get_node("BaseCardContainer/CL/font/3"))
	_deathMarks.append(get_node("BaseCardContainer/CL/font/4"))
	_deathMarks.append(get_node("BaseCardContainer/CL/font/5"))
	_deathMarks.append(get_node("BaseCardContainer/CL/font/6"))
	_deathMarks.append(get_node("BaseCardContainer/CL/font/7"))
	_deathMarks.append(get_node("BaseCardContainer/CL/font/8"))
	_deathMarks.append(get_node("BaseCardContainer/CL/font/9"))


func _myInit():
	_initMark()
	_deathCard = get_node("BaseCardContainer")


func _ready():
	_myInit()


func _process(delta):
	pass
