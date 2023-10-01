class_name SlotAnimationIns
extends Resource

export var HasType: bool
export var Type_Priority : int
export var Type_Name : String
export var Type_Duration:float


func PackType()->TypeEventData:
	if(HasType == false):
		return null
	var res = TypeEventData.new()
	res.Priority = Type_Priority
	res.Name = Type_Name
	res.Duration = Type_Duration
	return res
	


export var HasSize: bool
export var Size_Priority : int
export var Size_End : float
export var Size_Speed : float
export var Size_Duration : float

export var Size_PropertyData_Init:float
export var Size_PropertyData_End:float
export var Size_PropertyData_Duration:float
export var Size_PropertyData_Delay:float
export var Size_PropertyData_AnimationCurve:Curve

export var Size_ResetSpeed : float
export var Size_Lock : bool

func PackSize()->FloatEventData:
	if(HasSize == false):
		return null
	var res = FloatEventData.new()
	res.Priority = Size_Priority
	res.End = Size_End
	res.Speed = Size_Speed
	res.Duration = Size_Duration
	var tem = FloatPropertyData.new()
	tem.Init = Size_PropertyData_Init
	tem.End = Size_PropertyData_End
	tem.Duration = Size_PropertyData_Duration
	tem.Delay = 0
	tem.AnimationCurve = Size_PropertyData_AnimationCurve
	res.PropertyData = tem
	res.ResetSpeed = Size_ResetSpeed
	res.Lock = Size_Lock
	return res
