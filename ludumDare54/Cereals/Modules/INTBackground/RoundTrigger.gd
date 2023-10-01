class_name RoundTrigger
extends Node2D


enum TriggerType {
	Round,
	Diamond,
	TriangleUp,
	TriangleDown
	Rectangular,
	Order,
}
const sq3 = sqrt(3)


export var Delay:float
export var Duration:float
export var AnimationCurve:Curve
export var ParticalAnimation:String
export var ParticalType:String
export var EndSize : float
export(int,"Round","Diamond","TriangleUp","TriangleDown","Rectangular","Order") var Type
export var center :Vector2
export var reverse : bool 

var Particals:Array
var _particalsData : Array
var _pCount:int = 0
var _distant : float = 0
var _timeCount : float = 0
var _value : float = 0
var _inited : bool = false


func _delete():
	queue_free()


func _slotAnimation()->SlotAnimationData:
	return ParticalGenerator.SlotAnimation(ParticalAnimation,ParticalType)


func _calTime(delta:float):
	_timeCount +=delta
	if _timeCount>Duration:
		_timeCount = Duration


func _calDistant():
	_distant = AnimationCurve.interpolate(_timeCount/Duration)*EndSize
	if reverse:
		_distant = AnimationCurve.interpolate((Duration-_timeCount)/Duration)*EndSize
	
	
func _calValue():
	if Type == TriggerType.Round:
		_value = _distant*_distant
	else :
		_value = _distant


func _triggrt(p:ParticalSlot,d:SlotAnimationData):
	p.AddEvent(d)


func _triggerAll():
	if reverse:
		while(_pCount>=0 and _value<=_particalsData[_pCount][0]):
			_triggrt(_particalsData[_pCount][1],_slotAnimation())
			_pCount -=1
		if _pCount <0:
			_timeCount = Duration
	else:
		while(_pCount<_particalsData.size() and _value>_particalsData[_pCount][0]):
			_triggrt(_particalsData[_pCount][1],_slotAnimation())
			_pCount +=1
		if _pCount >= _particalsData.size():
			_timeCount = Duration


func _checkDelete():
	if _timeCount >= Duration:
		_delete()

func _calculate(delta:float):
	_calTime(delta)
	_calDistant()
	_calValue()
	_triggerAll()
	_checkDelete()
	pass


func Init():
	_inited = true
	_pCount = 0
	if reverse :
		_pCount = Particals.size()-1
	
	if Type == TriggerType.Round:
		var pair 
		for i in Particals:
			pair = []
			pair.append(UFM.DistantSquare(center,i.position))
			pair.append(i)
			_particalsData.append(pair)
		_particalsData.sort_custom(self,"_compair")
	elif Type == TriggerType.Order:
		var pair 
		var count = 0
		var total = Particals.size()
		for i in Particals:
			pair = []
			pair.append(count/total)
			pair.append(i)
			_particalsData.append(pair)
			count+=1
		_particalsData.sort_custom(self,"_compair")
	elif Type == TriggerType.Diamond:
		var pair 
		for i in Particals:
			pair = []
			pair.append(UFM.DiamondDistant(center,i.position))
			pair.append(i)
			_particalsData.append(pair)
		_particalsData.sort_custom(self,"_compair")
	elif Type == TriggerType.Rectangular:
		var pair 
		for i in Particals:
			pair = []
			pair.append(UFM.RectangleDistant(center,i.position))
			pair.append(i)
			_particalsData.append(pair)
		_particalsData.sort_custom(self,"_compair")
		


func _compair(p1,p2):
	return p2[0]>p1[0]


func _ready():
	pass

func _process(delta):
	if Delay>0:
		Delay -= delta
	else:
		if(!_inited):
			Init()
		_calculate(delta)
