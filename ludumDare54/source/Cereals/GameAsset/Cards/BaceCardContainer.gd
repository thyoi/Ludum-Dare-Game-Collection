class_name BaseCardContainer
extends Node


export var FilpCurve : Curve

export var InitPosition : Vector2
export var HoverDisplay : Vector2

var _filped:bool = false
var _onFilp : int
var _filpCount : float
var _canvasLayer : CanvasLayer
var _mainSprite : Node2D
var _backSprite : Node2D
var _follow : Vector2FollowAnimation = Vector2FollowAnimation.new()
var _hoverFollow : Vector2FollowAnimation = Vector2FollowAnimation.new()
var _hover :bool
var _hoverDistant : Vector2
var _filpSpeed = 0.1
var _shadow : ShrinkDisapper

func _deleteShadow():
	_shadow.Start()


func Position(p:Vector2,speed:float = 5):
	if speed>0:
		_follow.Speed = speed
		_follow.End = p
	else:
		_follow.End = p
		_follow.Cur = p
		_setPosition(p)


func _setScale(s:float,back:bool):
	if(s>1):
		s = 2-s
	if back:
		if s<0:
			s=0
		_backSprite.scale.x = s
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
	else:
		_filpCount -= dt*_filpSpeed
		if _filpCount <= 0:
			_filpCount =0
			_onFilp = 0
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
	_canvasLayer.layer = 0
	

func _updatePosition(dt:float):
	if _hover:
		_hoverFollow.End = HoverDisplay
		_hoverDistant = _hoverFollow.Shift(dt)
	else:
		_hoverFollow.End = Vector2.ZERO
		_hoverDistant = _hoverFollow.Shift(dt)
	_setPosition(_follow.Shift(dt))


func _setPosition(p:Vector2):
	_mainSprite.position = p+_hoverDistant
	_backSprite.position = p+_hoverDistant
	_shadow.position = p-_hoverDistant + Vector2(5,5)


func _myInit():
	_shadow = GameEffectManager.CardShadow()
	_canvasLayer = get_node("CL")
	_mainSprite = get_node("CL/font")
	_backSprite = get_node("CL/back")
	_follow.MinSpeed = 0.1
	_hoverFollow.MinSpeed = 0.1
	_hoverFollow.Speed = 0
	
	Position(InitPosition,0)
	_filpCount =0
	
	
	

func _ready():
	_myInit()


func _process(delta):
	_updatePosition(delta)
	if _onFilp!=0:
		_updateFilp(delta)

	

