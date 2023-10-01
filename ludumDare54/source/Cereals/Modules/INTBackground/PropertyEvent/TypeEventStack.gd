class_name TypeEventStack



var _curEvent : TypeEventData = null


func _updateCur(dt:float)->String:
	if _curEvent !=null:
		_curEvent.Duration-=dt
		if _curEvent.Duration <=0:
			var tem = _curEvent.Name
			_curEvent = null
			return tem
			
		else:
			return ""
	else:
		return ""

func AddEvent(e:TypeEventData):
	if(_curEvent == null):
		_curEvent = e
	elif(e.Priority>=_curEvent.Priority):
		_curEvent = e


func Update(dt:float)->String:
	return _updateCur(dt)


