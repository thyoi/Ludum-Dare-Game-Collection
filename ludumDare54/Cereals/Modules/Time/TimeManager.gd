extends Node


enum TimeGroup {default,main_menu}
enum TimeScaleEvent {pause_to_main_menu}


const delayItem = preload("res://Modules/Time/prefab/DelayItem.tscn")




func DelayFunction(f,d:float):
	var tem = delayItem.instance()
	tem.Init(f,d)
	add_child(tem)



const TimeGroupNum = 2


var _timeScaleList : Array
var _timeScaleEventList : Array
var _timeScaleEventFlagList : Array
var _deltaTime : float


func _genTimeScaleEventFromNums(nums:Array)->Array:
	var res:Array = []
	for i in TimeGroupNum:
		res.append(0)
	for i in nums:
		res[i] = 1	
	return res
		
		
func _genTimeScaleEventFromNum(n:int)->Array:
	var res:Array = []
	for i in TimeGroupNum:
		res.append(0)
	res[n] = 1
	return res


func _initTimeScaleList():
	_timeScaleEventList.append(_genTimeScaleEventFromNum((TimeGroup.main_menu)))


func _initData():
	_timeScaleList = []
	for i in TimeGroupNum:
		_timeScaleList.append(1)
	_initTimeScaleList()
	_timeScaleEventFlagList = []
	for i in _timeScaleEventList.size():
		_timeScaleEventFlagList.append(false)


func _calTimeScale():
	_timeScaleList = [1,1]
	for i in _timeScaleEventList.size():
		if _timeScaleEventFlagList[i]:
			for j in _timeScaleEventList[i].size():
				_timeScaleList[j]*=_timeScaleEventList[i][j]

func TimeEvent(id : int, flag : bool):
	if flag != _timeScaleEventFlagList[id]:
		if flag:
			_timeScaleEventFlagList[id] = flag
			for i in _timeScaleEventFlagList[id].size():
				_timeScaleList[i]*=_timeScaleEventFlagList[id][i]
		else:
			_timeScaleEventFlagList[id] = flag
			_calTimeScale()


func time(group : int = TimeGroup.default, dt : float = -1)->float:
	if dt>=0:
		return _timeScaleList[group]*dt
	else:
		return _timeScaleList[group]*_deltaTime


func _ready():
	_initData()


func _process(delta):
	_deltaTime = delta
