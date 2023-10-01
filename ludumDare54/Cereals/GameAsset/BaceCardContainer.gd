class_name BaseCardContainer
extends Node


export var FilpCurve : Curve
export var InitPosition : Vector2


export(Array,int) var NumValues
export(Array,int) var ColorValues
export var Discraible : String
export var FilpAnimation :String
export var Shed:String
export var Take:String
export var Tear:String
export var FlipEvent :String
export var BoomColor :Color 
export var DestoryColor :Color

var _genName :String

var HoverDisplay : Vector2 = Vector2(0,-10)

var _filped:bool = false
var _onFilp : int
var _filpCount : float
var _canvasLayer : CanvasLayer
var _mainSprite : Sprite
var _backSprite : Sprite
var _follow : Vector2FollowAnimation = Vector2FollowAnimation.new()
var _hoverFollow : Vector2FollowAnimation = Vector2FollowAnimation.new()
var _hover :bool
var _hoverDistant : Vector2
var _filpSpeed = 6
var _shadow : ShrinkDisapper
var _curPosition :Vector2
var _cardHolder  = null
var _sizeFollow : FloatFollowAnimation
var _roFollow :FloatFollowAnimation = FloatFollowAnimation.new()
var _defaultSpeed = 4
var _lastHover :bool
var _onDestory :bool
var _destoryCount = 0


func CardDestory():
	_onDestory  = true
	_destoryCount = 1
	if _cardHolder!=null:
		_cardHolder.PopCard(self)
		SParticalGenerater.RectBoom(_curPosition,["rp0"],Vector2(140,200),0.3,40,DestoryColor)
		SoundManager.Play("dc")
func _updateDestory(dt:float):
	_destoryCount-=dt*9
	if _destoryCount <=0:
		_destoryCount =0
		queue_free()
		_shadow.queue_free()
		
	if _filped:
		_setScale(0,false)
		_setScale(_destoryCount,true)
	else:
		_setScale(_destoryCount,false)
		_setScale(0,true)
	



func _hasSameIntValue(v:int,c)->bool:
	for i in c.NumValues:
		if i== v:
			return true
	return false
	
func _hasSameColorValue(v:int,c)->bool:
	for i in c.ColorValues:
		if i==v:
			return true
	return false
	
	
func Check(c)->bool:
	for i in NumValues:
		if _hasSameIntValue(i,c):
			return true
	for i in ColorValues:
		if _hasSameColorValue(i,c):
			return true
	return false


func Filter()->CardFilter:
	var tem = CardFilter.new()
	tem.IntValue = NumValues
	tem.ColorValue = ColorValues
	return tem


func ChangeCardBack(t:Texture):
	_backSprite = get_node("CL/back")
	_backSprite.texture = t


func _updateHover():
	_hover = MouseManager.CheckCardHover(self)
	if _hover!=_lastHover and _lastHover == false:
		SoundManager.Play("cardh")
	_lastHover = _hover


func CardPosition()->Vector2:
	return _curPosition


func CardZIndex()->int:
	return _canvasLayer.layer


func _deleteShadow():
	_shadow.Start()


func Position(p:Vector2,r:float = 0,ins:bool = false):
	if ins == false:
		_follow.Speed = _defaultSpeed
		_follow.End = p
		_roFollow.End =r
	else:
		_follow.End = p
		_follow.Cur = p
		_roFollow.End =r
		_roFollow.Cur =r
		_setPosition(p)


func _setScale(s:float,back:bool):
	if(s>1):
		s = 2-s
	if back:
		if s<0:
			s=0
		_backSprite.scale.x = s*_sizeFollow.Cur
	else:
		if s<=0:
			s=0
			if _filped == false:
				_onBack()
		else:
			if _filped:
				_deBack()
		
		_mainSprite.scale.x = s
	if s>0:
		_shadow.scale.x =s
		
		
func _rotate(r):
	
	_setScale(1-2*r,false)
	_setScale(2*r-1,true)


func _updateFilp(dt:float):
	if _onFilp == 1:
		_filpCount += dt*_filpSpeed
		if _filpCount >= 1:
			_filpCount =1
			_onFilp = 0
			_filped = true
	else:
		_filpCount -= dt*_filpSpeed
		if _filpCount <= 0:
			_filpCount =0
			_onFilp = 0
			_filped = false
	_rotate(FilpCurve.interpolate(_filpCount))

func Filp():
	if _onFilp == 0:
		if _filped:
			_onFilp = -1
		else:
			_onFilp = 1
	else:
		if _onFilp ==1:
			_onFilp = -1
		else:
			_onFilp = 1
	
	
func _onBack():
	pass
	
	
func _deBack():
	pass
	
	
func Destory():
	_deleteShadow()
	queue_free()
	
	
func Order(o : int):
	_canvasLayer.layer = o
	

func _updatePosition(dt:float):
	if _hover:
		_hoverFollow.End = HoverDisplay
		_hoverDistant = _hoverFollow.Shift(dt)
	else:
		_hoverFollow.End = Vector2.ZERO
		_hoverDistant = _hoverFollow.Shift(dt)
	_setPosition(_follow.Shift(dt))
	_setRo(_roFollow.Shift(dt))
	


func _setRo(r:float):
	_mainSprite.rotation=r
	_backSprite.rotation=r
	_shadow.rotation=r

func _setPosition(p:Vector2):
	_curPosition =p
	_mainSprite.position = p+_hoverDistant
	_backSprite.position = p+_hoverDistant
	_shadow.position = p-(_hoverDistant.y*Vector2(0.6,1)) + Vector2(5,5)
	



func Clicked():
	_sizeFollow.Cur = 1.2


func _updateSize(dt:float):
	var tem = _sizeFollow.Shift(dt)
	if(_filped):
		_backSprite.scale = Vector2(tem,tem)
	else:
		_mainSprite.scale = Vector2(tem,tem)

	_shadow.scale = Vector2(tem,tem)


func _myInit():
	_shadow = GameEffectManager.CardShadow()
	_canvasLayer = get_node("CL")
	_mainSprite = get_node("CL/font")
	_backSprite = get_node("CL/back")
	_follow.MinSpeed = 0.1
	_hoverFollow.MinSpeed = 0.1
	_hoverFollow.Speed = 10
	
	
	_filpCount =0
	_sizeFollow = FloatFollowAnimation.new()
	_sizeFollow.Cur = 1
	_sizeFollow.End = 1
	_sizeFollow.Speed = 5

	_roFollow.Cur = 0
	_roFollow.End = 0
	_roFollow.Speed = 13
	Position(InitPosition,0,true)
	
	

func _ready():
	_myInit()


func _process(delta):
	_updateSize(delta)
	_updateHover()
	_updatePosition(delta)
	if _onFilp!=0:
		_updateFilp(delta)
	if _onDestory:
		_updateDestory(delta)

	

