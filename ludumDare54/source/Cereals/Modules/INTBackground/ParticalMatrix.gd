class_name ParticalMatrix
extends Node2D


const sq2 = sqrt(2)
const sq3 = sqrt(3)

const ParticalSlotPrefab = preload("res://Modules/INTBackground/Prefab/ParticalSlot.tscn")
export var InitSize:float
export var ScreenSize:Vector2
export(int,"Diamond","Triangle") var MatrixType
export var SlotSizeBalance : float


var Slots:Array
var SlotList:Array

var _shiftCountroler:SlotShiftCountroler

func _genSlot(p:Vector2)->ParticalSlot:
	var tem = ParticalSlotPrefab.instance() as ParticalSlot
	tem.position = p
	add_child(tem)
	SlotList.append(tem)
	tem._defaultSize = SlotSizeBalance
	return tem
	
	
func _genSlotDiamond(s:float):
	var x:int  = ceil((ScreenSize.x-s)/2.0/s)+1
	var y:int  = ceil((ScreenSize.y-s)/2.0/s)+1
	Slots = []
	SlotList = []
	for i in (y*4+1):
		var tem = []
		for j in (x*2+1):
			tem.append(_genSlot(UFM.DiamondPosition(ScreenSize/2,s,i,j,x,y)))
		Slots.append(tem)
			

func _genSlotTriangle(s:float):
	var sx = s/1.2
	var sy = s*sq3/1.2
	var x:int  = ceil((ScreenSize.x-sx)/2.0/sx)+1
	var y:int  = ceil((ScreenSize.y-sy)/2.0/sy)+1
	Slots = []
	SlotList = []
	for i in (y*4+1):
		var tem = []
		for j in (x*2+1):
			tem.append(_genSlot(UFM.TrianglePosition(ScreenSize/2,s,i,j,x,y)))
		Slots.append(tem)
		

func _initShiftCountroler():
	_shiftCountroler = get_node("ShiftCountroler")
	_shiftCountroler.SlotType = MatrixType
	_shiftCountroler.Size = InitSize
	_shiftCountroler.Slots = Slots
	_shiftCountroler.ScreenSize = ScreenSize
	_shiftCountroler.Init()


func _ready():
	if(MatrixType == 0):
		_genSlotDiamond(InitSize)
	elif(MatrixType == 1):
		_genSlotTriangle(InitSize)
	_initShiftCountroler()
		
	#_test()


func _process(delta):
	pass


func _test():
	ParticalGenerator.Trigger(Vector2(480,270),"fullR_round_ac_1","show1","l4",0.1,SlotList,self)
	#ParticalGenerator.Trigger(Vector2(480,270),"full_round_ac_1","hide1","l12",2,SlotList,self)
	ParticalGenerator.Trigger(Vector2(1600,270),"full_round_ac_1","show1","l12",0.2,SlotList,self)
	#ParticalGenerator.Trigger(Vector2(480,270),"full_round_ac_1","hide1","l12",4,SlotList,self)
	#ParticalGenerator.Trigger(Vector2(480,270),"fullR_round_ac_1","show1","l12",5,SlotList,self)
	#ParticalGenerator.Trigger(Vector2(480,270),"full_round_ac_1","hide1","l12",6,SlotList,self)
	#ParticalGenerator.Trigger(Vector2(480,270),"fullR_round_ac_1","show1","l12",7,SlotList,self)
	_shiftCountroler.Start(0)


