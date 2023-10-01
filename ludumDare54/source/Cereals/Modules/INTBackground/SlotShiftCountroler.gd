class_name SlotShiftCountroler
extends Node

enum Stype{
	Diamond,
	Triangle,
}
enum Shtype{
	Slice,
	MoveRD,
}

const sq2 = sqrt(2)
const sq3 = sqrt(3)


var SlotType
var ShiftType = -1
var Size
var Speed : float = 15
var Slots : Array
var ScreenSize : Vector2

var _onShift:bool = false
var _distant:float
var _duration:float
var _counter:float




func Init():
	if SlotType == Stype.Diamond:
		_distant = Size/2
	elif SlotType == Stype.Triangle:
		_distant = Size*1.18/2/sq2
	_duration = _distant/Speed
func _resetPosition():
	var x = (Slots[0].size()-1)/2
	var y = (Slots.size()-1)/4
	if(SlotType == Stype.Diamond):
		for i in Slots.size():
			for j in Slots[i].size():
				Slots[i][j].position = UFM.DiamondPosition(ScreenSize/2,Size,i,j,x,y)
	elif(SlotType == Stype.Triangle):
		for i in Slots.size():
			for j in Slots[i].size():
				Slots[i][j].position = UFM.TrianglePosition(ScreenSize/2,Size,i,j,x,y)

func _shiftPosition():
	if SlotType == Stype.Diamond or SlotType == Stype.Triangle:
		if ShiftType == Shtype.Slice:
			for i in _getK():
				if(i%2==1):
					_shiftK(i,false)
				else:
					_shiftK(i,true)
		else:
			for i in _getK():
				_shiftK(i,false)


func _shiftK(k:int,up:bool):
	if up:
		var lm = _getL(k)
		var tem = _getInKL(k,0)
		for l in lm:
			if l == lm-1:
				_setInKL(tem,k,l)
			else :
				_setInKL(_getInKL(k,l+1),k,l)
	else:
		var lm = _getL(k)
		var tem = _getInKL(k,lm-1)
		for ll in lm:
			var l = lm-ll-1
			if l == 0:
				_setInKL(tem,k,l)
			else :
				_setInKL(_getInKL(k,l-1),k,l)


func _getInKL(k:int,l:int)->ParticalSlot:
	return _setInKL(null,k,l)

	
func _setInKL(s:ParticalSlot,k:int,l:int)->ParticalSlot:
	var x = l/2
	var y = Slots.size()-2*k+l-1
	if(k>=_getMi()):
		y = l
		x+=(k-_getMi()+1)
	if(s!=null):
		Slots[y][x] = s
	return Slots[y][x]


func _getK()->int:
	var y = Slots.size()
	var x = Slots[0].size()
	return y/2+x


func _getL(k:int)->int:
	var x = Slots[0].size()
	var y = Slots.size()
	var mi = UFM.Less(x,((y+1)/2))
	var ma = UFM.Large(x,((y+1)/2))
	if k<mi:
		return 2*k+1
	elif k<ma:
		return mi*2-1
	else :
		return (mi*2)-2*(k-ma+1)
	
func _getMi()->int:
	var x = Slots[0].size()
	var y = Slots.size()
	return UFM.Less(x,((y+1)/2))
	
func _getMa()->int:
	var x = Slots[0].size()
	var y = Slots.size()
	return UFM.Large(x,((y+1)/2))
	
func _setPosition():
	var x = (Slots[0].size()-1)/2
	var y = (Slots.size()-1)/4
	if(SlotType == Stype.Diamond):
		for i in Slots.size():
			for j in Slots[i].size():
				Slots[i][j].position = UFM.DiamondPosition(ScreenSize/2,Size,i,j,x,y)+_moveStep(j,i)*(_counter/_duration)
	elif(SlotType == Stype.Triangle):
		for i in Slots.size():
			for j in Slots[i].size():
				Slots[i][j].position = UFM.TrianglePosition(ScreenSize/2,Size,i,j,x,y)+_moveStep(j,i)*(_counter/_duration)



func _moveStep(x,y)->Vector2:
	if SlotType == Stype.Diamond:
		if ShiftType == Shtype.Slice:
			var tl = (Slots.size()-y)/2+x
			if tl %2 == 1:
				return Vector2(1,1)*_distant
			else:
				return Vector2(-1,-1)*_distant
		else:
			return Vector2(1,1)*_distant
	elif SlotType == Stype.Triangle:
		if ShiftType == Shtype.Slice:
			var tl = (Slots.size()-y)/2+x
			if tl %2 == 1:
				return Vector2(1,sq3)*_distant
			else:
				return Vector2(-1,-sq3)*_distant
		else:
			return Vector2(1,sq3)*_distant
	else:
		return Vector2.ZERO

func Start(t:int):
	if t>=0:
		_onShift = true
		_counter = 0
		ShiftType = t
		_resetPosition()
	else:
		Stop()
	
func Stop():
	_onShift = false
	_resetPosition()

func _updateTime(delta:float):
	_counter +=delta
	if(_counter>_duration):
		_counter = 0
		_shiftPosition()

		
		
func _updatePosition():
	_setPosition()


func _process(delta):
	if(_onShift):
		_updateTime(delta)
		_updatePosition()
	
