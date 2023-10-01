extends Node
class_name pg

export(Array,Curve) var ACCurve

var l4 = preload("res://Modules/Partical/Prefab/Line4.tscn")
var l12 = preload("res://Modules/Partical/Prefab/Line12.tscn")
var mp0 = preload("res://Modules/Partical/Prefab/mp0.tscn")
var mp1 = preload("res://Modules/Partical/Prefab/mp1.tscn")
var mp2 = preload("res://Modules/Partical/Prefab/mp2.tscn")
var mp3 = preload("res://Modules/Partical/Prefab/mp3.tscn")

var show1 = preload("res://Modules/INTBackground/Prefab/SlotAnimation/Show1.tres")
var hide1 = preload("res://Modules/INTBackground/Prefab/SlotAnimation/Hide1.tres")
var trans1 = preload("res://Modules/INTBackground/Prefab/SlotAnimation/Trans1.tres")
var trans2 = preload("res://Modules/INTBackground/Prefab/SlotAnimation/Trans2.tres")
var size0 = preload("res://Modules/INTBackground/Prefab/SlotAnimation/Size0.tres")
var size1 = preload("res://Modules/INTBackground/Prefab/SlotAnimation/Size1.tres")
var size2 = preload("res://Modules/INTBackground/Prefab/SlotAnimation/Size2.tres")
var rt = preload("res://Modules/INTBackground/Prefab/Trigger/Round1.tscn")
var _particalPrefabDic
var _slotDataDic

func _ready():
	_particalPrefabDic = Dictionary()
	_particalPrefabDic["l4"] = l4
	_particalPrefabDic["l12"] = l12
	_particalPrefabDic["mp0"] =  mp0
	_particalPrefabDic["mp1"] =  mp1
	_particalPrefabDic["mp2"] =  mp2
	_particalPrefabDic["mp3"] =  mp3
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
		
	
func MatrixBoom(p:Vector2,type :int,ten:int,pList:Array,matrix:ParticalMatrix):
	var td
	var ts
	if ten ==0:
		ts = "size0"
	elif ten == 1:
		ts = "size1"
	else:
		ts = "size2"
	if type == 0:
		td = TriggerGenerator.Trigger("full_round_ac_1")
	elif type == 1:
		td = TriggerGenerator.Trigger("full_diamond_ac_1")
	else : 
		td = TriggerGenerator.Trigger("full_rectangle_ac_1")
		
	var tem = _trigger(0,td.Duration,_curve(td.AnimationCurveName)
		,ts,"",td.EndSize,td.Type,td.revers,pList)
	tem.center = p
	matrix.add_child(tem)
	
	
func MatrixParticalChange(p:Vector2,type :int,particalName:String,pList:Array,matrix:ParticalMatrix):
	var td
	if type == 0:
		td = TriggerGenerator.Trigger("full_round_ac_1")
	elif type == 1:
		td = TriggerGenerator.Trigger("full_diamond_ac_1")
	else : 
		td = TriggerGenerator.Trigger("full_rectangle_ac_1")
	var tem = _trigger(0,td.Duration,_curve(td.AnimationCurveName)
		,"trans2",particalName,td.EndSize,td.Type,td.revers,pList)
	tem.center = p
	matrix.add_child(tem)
	
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
	tem.Init(size0 as SlotAnimationIns)
	_slotDataDic["size0"] = tem
	tem = SlotAnimationData.new()
	tem.Init(size1 as SlotAnimationIns)
	_slotDataDic["size1"] = tem
	tem = SlotAnimationData.new()
	tem.Init(size2 as SlotAnimationIns)
	_slotDataDic["size2"] = tem
	tem = SlotAnimationData.new()
	tem.Init(trans2 as SlotAnimationIns)
	_slotDataDic["trans2"] = tem
	tem = SlotAnimationData.new()
	
	
