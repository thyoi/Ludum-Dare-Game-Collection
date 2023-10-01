class_name ParticalMatrixManager
extends Node



var _matrixList : Array
var _curMatrix : ParticalMatrix
var _positionChoice : Array


func To(matrixIndex : int,triggerName:String, particalName:String,moveType:int,p:int,delay:float):
	if _curMatrix!=null:
		ParticalGenerator.Trigger(_positionChoice[p],triggerName,"hide1","l12",delay,_curMatrix.SlotList,_curMatrix)
		_curMatrix._shiftCountroler.Start(moveType)
	if matrixIndex>=0:
		_curMatrix = _matrixList[matrixIndex]
		ParticalGenerator.Trigger(_positionChoice[p],triggerName,"show1",particalName,delay+0.4,_curMatrix.SlotList,_curMatrix)
		_curMatrix._shiftCountroler.Start(moveType)
	else:
		_curMatrix = null



func _initMatrixList():
	_matrixList = []
	_matrixList.append(get_node("Matrix1"))
	_matrixList.append(get_node("Matrix2"))
	_matrixList.append(get_node("Matrix3"))
	_matrixList.append(get_node("Matrix4"))
	_positionChoice = []
	_positionChoice.append(Vector2(480,270))
	_positionChoice.append(Vector2(480,-100))
	_positionChoice.append(Vector2(480,640))
	_positionChoice.append(Vector2(1060,270))
	_positionChoice.append(Vector2(-100,270))
	_positionChoice.append(Vector2(-100,-100))
	_positionChoice.append(Vector2(-100,640))
	_positionChoice.append(Vector2(1060,-100))
	_positionChoice.append(Vector2(1060,640))


func _ready():
	_initMatrixList()
	


func _process(delta):
	pass
	
	
func _test():
	To(1,"full_round_ac_1","l4",0,0,1.5)
	To(0,"full_diamond_ac_1","l12",-1,1,3)
	To(3,"fullR_round_ac_1","l4",1,6,4.5)
	To(1,"fullR_rectangle_ac_1","l12",0,0,6)
	To(2,"full_round_ac_1","l4",-1,0,7.5)
	To(1,"full_round_ac_1","l4",0,2,9)
	To(0,"full_diamond_ac_1","l12",-1,4,10.5)
	To(3,"fullR_round_ac_1","l4",1,0,12)
	To(2,"full_round_ac_1","l4",-1,3,13.5)
	To(1,"full_round_ac_1","l4",0,0,15)
	To(0,"full_diamond_ac_1","l12",-1,0,16.5)
	To(3,"fullR_round_ac_1","l4",1,0,18)
	To(1,"fullR_rectangle_ac_1","l12",0,0,19.5)
	To(2,"full_round_ac_1","l4",-1,0,21)
	To(1,"fullR_rectangle_ac_1","l12",0,0,22.5)
	To(1,"full_round_ac_1","l4",0,0,24)
	To(0,"full_diamond_ac_1","l12",-1,0,25.5)
	To(3,"fullR_round_ac_1","l4",1,0,27)
	To(1,"fullR_rectangle_ac_1","l12",0,0,28.5)
	To(2,"full_round_ac_1","l4",-1,0,30)

