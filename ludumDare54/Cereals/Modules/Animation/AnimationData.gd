class_name AnimationData
extends Resource




export var Position: Resource
export var Rotation: Resource
export var Scale: Resource
export var MainColor: Resource
export var Apha: Resource


var _propertyDataList: Array = [Position,Rotation,Scale,MainColor,Apha]


func TotalTime()->float:
	var res:float = 0
	var tem:float
	for i in _propertyDataList:
		tem = _durationFromData(i)
		if(tem>res):
			res = tem
	
	return res

func _durationFromData(data:Resource)->float:
	if(data!=null):
		return (data as Vector2PropertyData).Duration
	else:
		return 0.0
