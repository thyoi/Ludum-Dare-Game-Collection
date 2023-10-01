class_name SizeEventStack


var DefaultSize:float = 1
var DefaultSpeed:float  = 7

var _cur : float = 0
var _eventList : Array = []
var _follow: FloatFollowAnimation = FloatFollowAnimation.new()
var _curEvent : FloatEventData = null
var _lock:int = -1


func _findCurEvent()->FloatEventData:
	if _eventList.size()<=0:
		return null
	else:
		var tem: FloatEventData = _eventList[0]
		for i in _eventList:
			if i.Priority >= tem.Priority:
				tem = i
		return tem


func _changeEvent(e:FloatEventData):
	if e == null:
		_defaultEvent()
	elif e.Priority<0:
		_follow.Speed = e.Speed
		_follow.Cur = _cur
		_follow.End = e.End
	else:
		if(e.ResetSpeed<=0):
			_follow.Cur = 0
			_follow.End = 0
		else:
			_follow.Cur = _cur-e.Value()
			_follow.End = 0
			_follow.Speed = e.ResetSpeed
	_curEvent = e


func _updateCurEvent():
	var tem:FloatEventData = _findCurEvent()
	if(tem!=_curEvent):
		_changeEvent(tem)
	#if (tem !=null and tem.Priority>=_lock):
		#_lock = -1



func _updateCur(dt:float):
	if _curEvent ==null:
		_cur = _follow.Shift(dt)
	elif _curEvent.Priority<0:
		_cur = _follow.Shift(dt)
	else:
		_cur = _curEvent.Value()+_follow.Shift(dt)
		

func _updateEventList(dt:float):
	for i in _eventList:
		i.Duration-=dt
		if(i.Duration<0):
			i.Duration = 0

		
func _cleanEventList():
	var dc : int = 0
	for i in _eventList.size():
		if _eventList[i-dc].Duration<=0:
			if _eventList[i-dc].Lock:
				_lock = _eventList[i-dc].Priority
			_eventList.remove(i-dc)
			dc+=1


func _defaultEvent():
	_follow.Speed = DefaultSpeed
	_follow.Cur = _cur
	_follow.End = DefaultSize


func Init():
	_defaultEvent()
	
	
func AddEvent(e:FloatEventData):
	if(e.Priority>=_lock):
		_lock = -1
	_eventList.append(e)


func Update(dt:float)->float:
	_updateEventList(dt)
	_updateCurEvent()
	if _lock<0:
		
		_updateCur(dt)
	_cleanEventList()
	return _cur


