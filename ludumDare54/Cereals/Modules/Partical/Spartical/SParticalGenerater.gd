extends Node


const roundP = preload("res://Modules/Partical/Spartical/prefab/RoundPartical.tscn")

const rp0 = preload("res://Modules/Partical/Spartical/prefab/RP0.tscn")


var _particalDic :Dictionary







func RoundBoom(position:Vector2, size:float,color:Color,back:bool = false):
	var tem = roundP.instance() as RoundPartical
	tem.position = position
	tem.MaxSize*=size
	tem.MaxLine*=size
	tem.default_color = color
	tem.Init()
	GameEffectManager.RegPartical(tem,back)
	

func ParticalBoomR(position:Vector2,names:Array,Size:float,ParticalSize:float,num:int,color:Color,back:bool= false):
	for i in num:
		_splash(position,names,Size,ParticalSize,color,back)
	RoundBoom(position,Size,color,back)

func ParticalBoom(position:Vector2,names:Array,Size:float,ParticalSize:float,num:int,color:Color,back:bool= false):
	for i in num:
		_splash(position,names,Size,ParticalSize,color,back)

func _splash(position:Vector2,names:Array,Size:float,ParticalSize:float,color:Color,back:bool= false):
	var mainR = randf()
	var dri = randf()*2*PI
	var dis = (0+(mainR*0.7+randf()*0.3)*1) * Size*200
	var st =  randf()*0.1 * Size*200 * Vector2(sin(dri),cos(dri))+ position
	var sc = (0.4+((1-mainR)*0.3+randf()*0.7)*0.6) * ParticalSize
	var dur = 0.5 + 0.5*((mainR)*0.3+randf()*0.7) 
	var ro = (0.1+0.9*(mainR*0.7+randf()*0.3) )*9
	
	_partical(names[UFM.RandomInt(0,names.size()-1)],back,dur,dis,dri,st,sc,ro,color)

func RectBoom(position:Vector2, names:Array,size:Vector2,ParticalSize:float,num:int,c:Color,back:bool = false):
	for i in num:
		_rect(position,names,size,ParticalSize,c,back)


func _rect(position:Vector2, names:Array,size:Vector2,ParticalSize:float,c:Color,back:bool = false):
	var mainR = randf()
	var dri = ((randi()%2)-0.5)*PI
	var dis = (mainR*0.2 + randf()*0.8)*size.x/2
	var st = Vector2(0,randf()*size.y-size.y/2)+position
	var sc = (0.7+0.3*(0.6*randf()+0.4*mainR))* ParticalSize
	var dur = 0.3 + 0.7*(0.6*randf()+0.4*mainR)
	var ro = (0.1+0.9*(mainR*0.7+randf()*0.3) )*9
	
	_partical(names[UFM.RandomInt(0,names.size()-1)],back,dur,dis,dri,st,sc,ro,c)


func _partical(name:String,back:bool,duration:float,distant:float,dri:float,startP:Vector2,
	Scale:float,rotation:float,color:Color):
	if _particalDic.has(name):
		var tem = SPartical(name,back)
		tem.Duration = duration
		tem.StartPosition = startP
		tem.EndPosition = startP + distant*Vector2(sin(dri),cos(dri))
		tem.MaxSize = Scale
		tem.RotateMax = rotation
		tem._setColor(color)
		tem.Start()



func SPartical(name:String,back:bool = false)->ShrinkDisapperAdvance:
	if _particalDic.has(name):
		var tem = (_particalDic[name].instance() as ShrinkDisapperAdvance)
		GameEffectManager.RegPartical(tem,back)
		return tem
	else:
		return null

func _myInit():
	_particalDic = Dictionary()
	_particalDic["rp0"] = rp0

func _ready():
	_myInit()


func _process(delta):
	pass
