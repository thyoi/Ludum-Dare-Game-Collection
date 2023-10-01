class_name FloatFollowAnimation


var Speed: float = 0
var MinSpeed : float = 0
var Cur : float
var End : float


func Shift(dt: float)->float:
	Cur = UFM.FloatShift(Cur,End,Speed*dt,MinSpeed)
	return Cur


