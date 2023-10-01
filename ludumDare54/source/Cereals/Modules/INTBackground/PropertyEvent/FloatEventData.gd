class_name FloatEventData
extends Resource


export var Priority : int
export var End : float
export var Speed : float
export var Duration : float
export var PropertyData : Resource
export var ResetSpeed : float
export var Lock : bool


func Value()->float:
	return (PropertyData as FloatPropertyData).Value((PropertyData as FloatPropertyData).Duration-Duration)


func Copy(d:FloatEventData):
	Priority = d.Priority
	End = d.End
	Speed = d.Speed
	Duration = d.Duration
	PropertyData = d.PropertyData
	ResetSpeed = d.ResetSpeed
	Lock = d.Lock
