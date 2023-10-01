extends Node


const c = preload("res://Modules/Sound/asset/Speak_base.wav")
const card = preload("res://Modules/Sound/asset/card.wav")
const cardh = preload("res://Modules/Sound/asset/cardh.wav")
const cardd = preload("res://Modules/Sound/asset/cardd.wav")
const pet = preload("res://Modules/Sound/asset/pet.wav")
const start  = preload("res://Modules/Sound/asset/start.wav")
const dc = preload("res://Modules/Sound/asset/dc.wav")
const gc = preload("res://Modules/Sound/asset/gc.wav")
const sp = preload("res://Modules/Sound/asset/sp.wav")
const bb = preload("res://Modules/Sound/asset/bb.wav")
const pick = preload("res://Modules/Sound/asset/pick.wav")
var _soundDic



func AddSound(name:String,resource, n:int , volum:float):
	var res = SoundGroup.new()
	for i in n:
		var tem = AudioStreamPlayer.new()
		tem.stream = resource
		res._sounds.append(tem)
		add_child(tem)
		tem.volume_db = volum
	_soundDic[name] = res
	
	
	
	
func Play(name:String):
	if _soundDic.has(name):
		_soundDic[name].Play()


func _ready():
	_soundDic = Dictionary()
	AddSound("c",c,5,-3)
	AddSound("card",card,5,-5)
	AddSound("cardh",cardh,5,-5)
	AddSound("cardd",cardd,5,-10)
	AddSound("start",start,1,-15)
	AddSound("pet",pet,5,-10)
	AddSound("gc",gc,5,-15)
	AddSound("dc",dc,5,-12)
	AddSound("sp",sp,5,-10)
	AddSound("bb",bb,5,-20)
	AddSound("pick",pick,5,-20)

func _process(delta):
	pass
