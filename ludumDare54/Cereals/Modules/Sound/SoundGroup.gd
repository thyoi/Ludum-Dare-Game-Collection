class_name SoundGroup


var _sounds :Array
var _count:int


func Play():
	_count+=1
	if _count>=_sounds.size():
		_count = 0
	(_sounds[_count]as AudioStreamPlayer).play()
