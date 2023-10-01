extends Node


const c1 = preload("res://GameAsset/Character/art/C1.png")
const c2 = preload("res://GameAsset/Character/art/C2.png")
const c3 = preload("res://GameAsset/Character/art/C3.png")
const c4 = preload("res://GameAsset/Character/art/C4.png")
const c5 = preload("res://GameAsset/Character/art/C5.png")
const c6 = preload("res://GameAsset/Character/art/C6.png")
const c7 = preload("res://GameAsset/Character/art/C7.png")
const c8 = preload("res://GameAsset/Character/art/C8.png")
const c9 = preload("res://GameAsset/Character/art/C9.png")
const cb = preload("res://GameAsset/Character/art/CB.png")
var _characterDic:Dictionary

var _character : Sprite
var _characterBack : Sprite
var _characterShadow :Sprite

var _characterFollow : Vector2FollowAnimation
var _characterBackFollow : FloatFollowAnimation
var _onShow :bool = false


func _myInit():
	_character = get_node("character")
	_characterBack = get_node("characterBack")
	_characterShadow = get_node("characterShadow")
	_characterFollow = Vector2FollowAnimation.new()
	_characterFollow.Speed = 12
	_characterBackFollow = FloatFollowAnimation.new()
	_characterBackFollow.Speed = 11
	
	GameEffectManager.RegUI(_character)
	GameEffectManager.RegUI(_characterBack)
	GameEffectManager.RegBlurShadow(_characterShadow)
	
	
func CharacterPosition(p:Vector2):
	_character.position = p
	_characterBack.position = p
	_characterShadow.position = p+Vector2(6,6)


func _updateCharacter(dt:float):
	_characterFollow.Shift(dt)
	_characterBackFollow.Shift(dt)
	_character.scale = _characterFollow.Cur
	_characterShadow.scale = Vector2(_characterBackFollow.Cur,_characterBackFollow.Cur)
	_characterBack.scale = Vector2(_characterBackFollow.Cur,_characterBackFollow.Cur)
	
	
func ShowCharacter():
	_characterFollow.End = Vector2(1,1)
	
func HideCharacter():
	_characterFollow.End = Vector2(0,0)
	
func HideBack():
	_characterBackFollow.End = 0

	
func Show(name:String,p:Vector2):
	if _characterDic.has(name):
		_onShow = true
		var tem = _characterDic[name]
		_character.texture = tem
		CharacterPosition(p)
		_characterBackFollow.End = 0.95
		TimeManager.DelayFunction(funcref(self,"ShowCharacter"),0.2)
			
func Hide():
	_onShow = false
	HideCharacter()
	TimeManager.DelayFunction(funcref(self,"HideBack"),0.2)
	
func PetCharacter():
	SoundManager.Play("pet")
	_characterFollow.Cur = Vector2(1.2,1.2)
	
	
func HoverCharacter():
	
	_characterBackFollow.End = 1.05
	
	
func DeHoverCharacter():
	_characterBackFollow.End = 0.95

func _initCharacterSprite():
	_characterDic = Dictionary()
	_characterDic["c1"] = c1
	_characterDic["c2"] = c2
	_characterDic["c3"] = c3
	_characterDic["c4"] = c4
	_characterDic["c5"] = c5
	_characterDic["c6"] = c6
	_characterDic["c7"] = c7
	_characterDic["c8"] = c8
	_characterDic["c9"] = c9
	_characterDic["cb"] = cb


func Sprite(name:String):
	if _characterDic.has(name):
		_character.texture = _characterDic[name]
		PetCharacter()


func _ready():
	_initCharacterSprite()
	_myInit()


func _process(delta):
	_updateCharacter(delta)
