class_name FloatPropertyData
extends Resource


export var Init:float
export var End:float
export var Duration:float
export var Delay:float
export var AnimationCurve:Curve


func Value(t:float)->float:
	var tem = t-Delay
	if tem<=0:
		return Init
	elif(tem<Duration):
		return UFM.Lerp(Init,End,AnimationCurve.interpolate(tem/Duration))
	else:
		return End
