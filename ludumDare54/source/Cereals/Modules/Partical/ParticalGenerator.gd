extends Node
class_name pg

export(Array,Curve) var ACCurve

var l4 = preload("res://Modules/Partical/Prefab/Line4.tscn")
var l12 = preload("res://Modules/Partical/Prefab/Line12.tscn")
var show1 = preload("res://Modules/INTBackground/Prefab/SlotAnimation/Show1.tres")
var hide1 = preload("res://Modules/INTBackground/Prefab/SlotAnimation/Hide1.tres")
var trans1 = preload("res://Modules/INTBackground/Prefab/SlotAnimation/Trans1.tres")
var rt = preload("res://Modules/INTBackground/Prefab/Trigger/Round1.tscn")
var _particalPrefabDic
var _slotDataDic

func _ready():
	_particalPrefabDic = Dictionary()
	_particalPrefabDic["l4"] = l4
	_particalPrefabDic["l12"] = l12
	_initSlotDataDic()



func Partical(name:String)->Node2D:
	if _particalPrefabDic.has(name):
		return _particalPrefabDic[name].instance() as Node2D
	else:
		return null


func SlotAnimation(name:String,type:String = "")->SlotAnimationData:
	if _slotDataDic.has(name):
		var res = SlotAnimationData.new()
		res.Copy(_slotDataDic[name])
		if type != "" and res.Type !=null:
			res.Type.Name = type
		return res
	else:
		return null


func _trigger(delay:float,duration:float,ac:Curve,name:String,particalType:String,endSize:float,type:int,revers:bool,pList:Array)->Node2D:
	var tem : RoundTrigger = rt.instance()
	tem.Delay = delay
	tem.Duration = duration
	tem.AnimationCurve = ac
	tem.ParticalAnimation = name
	tem.ParticalType=  particalType
	tem.EndSize = endSize
	tem.Type = type
	tem.Particals = pList
	tem.reverse = revers
	return tem
	
	
func Trigger(p :Vector2,name:String,animationType:String,particalType:String,delay:float,pList:Array,matrix:ParticalMatrix)->bool:
	var td = TriggerGenerator.Trigger(name)
	if(td==null):
		return false
	var tem = _trigger(delay,td.Duration,_curve(td.AnimationCurveName)
		,animationType,particalType,td.EndSize,td.Type,td.revers,pList)
	matrix.add_child(tem)
	tem.center = p
	return true
	
func _curve(name:String)->Curve:
	if(name == "ac0"):
		return ACCurve[0]
		
	else:
		return null


func _initSlotDataDic():
	_slotDataDic = Dictionary()
	var tem = SlotAnimationData.new()
	tem.Init(show1 as SlotAnimationIns)
	_slotDataDic["show1"] = tem
	tem = SlotAnimationData.new()
	tem.Init(trans1 as SlotAnimationIns)
	_slotDataDic["trans1"] = tem
	tem = SlotAnimationData.new()
	tem.Init(hide1 as SlotAnimationIns)
	_slotDataDic["hide1"] = tem
	
	
	
