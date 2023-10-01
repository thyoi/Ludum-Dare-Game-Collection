class_name Vector2FollowAnimation


var Speed: float = 0
var MinSpeed : float = 0
var Cur : Vector2
var End : Vector2


func Shift(dt: float)->Vector2:
	Cur = UFM.Vector2Shift(Cur,End,Speed*dt,MinSpeed)
	return Cur


