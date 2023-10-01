class_name ParticalSlot
extends Node2D


var _typeStack: TypeEventStack = TypeEventStack.new()
var _sizeStack: SizeEventStack = SizeEventStack.new()
var _partical
var _particalParent : Node2D
var _defaultSize = 1


func _updateAll(dt :float):
	if(_partical != null):
		_sizeUpdate(dt)
		_typeUpdate(dt)
	
	
func _typeUpdate(dt:float):
	var tem:String = _typeStack.Update(dt)
	if(tem != ""):
		ChangePartical(tem)


func _sizeUpdate(dt:float):	
	_sizeSet(_sizeStack.Update((dt)))
	
	
func _sizeSet(s:float):
	_partical.Size(s*_defaultSize)


func ChangePartical(name:String):
	_partical.queue_free()
	_partical = ParticalGenerator.Partical(name)
	if(_partical != null):
		_particalParent.add_child(_partical)
	_resetDefault()
	
	
func _resetDefault():
	_sizeStack.DefaultSize = 1


func AddEvent(e:SlotAnimationData):
	if(e == null):
		return
	if(e.Type!=null):
		_typeStack.AddEvent(e.Type)
	if(e.Size!=null):
		_sizeStack.AddEvent(e.Size)
	

func _ready():
	_partical = get_node("n1/n2/partical")
	_particalParent = get_node("n1/n2")
	_sizeStack.Init()


func _process(delta):
	_updateAll(delta)

