extends Node


var _mouse:Sprite
var _mouseShadow:Sprite
var _mousePosition : Vector2
var _mouseOffset : Vector2 = Vector2(10,10)
var _hoverCard : BaseCardContainer = null
var _holdCard : BaseCardContainer = null
var _holdOffset : Vector2
var _onClick :bool = false
var _torPoint:bool = true

func CheckCardHover(c:BaseCardContainer)->bool:
	if UFM.PointInArea(_mousePosition,c.CardPosition(),Vector2(140,200)):
		if _hoverCard == null:
			_hoverCard = c
			return true
		else:
			if c.CardZIndex()>_hoverCard.CardZIndex():
				_hoverCard = c
				return true
			else:
				return false
	else:
		return false


func _setMousePosition(p:Vector2):
	_mouse.position = p+_mouseOffset
	_mouseShadow.position = p+Vector2(4,4)+_mouseOffset


func _updateMousePosition():
	_mousePosition = _mouse.get_global_mouse_position()
	_setMousePosition(_mousePosition)


func _updateHoldCard():
	if _holdCard!=null and _holdCard._cardHolder != null and _holdCard._cardHolder.Pickable:
		_holdCard.Position(_mousePosition+_holdOffset,0,14)
	else:
		_holdCard = null


func _input(event):
	if event is InputEventMouseButton:
		if event.button_index == BUTTON_LEFT  :
			if event.pressed:
				_mouseDown()
			else :
				_mouseUp()


func _mouseDown():
	_checkPetCharacter()
	_checkClickCharacter()
	_onClick = true
	_holdCard = _hoverCard
	if _hoverCard!=null:
		_clickCard(_hoverCard)
		_holdOffset = _holdCard.CardPosition()-_mousePosition
	else:	
		GameEffectManager.MatrixBoom(_mousePosition,0,0)
	if(_torPoint):
		SParticalGenerater.ParticalBoomR(_mousePosition,["rp0"],0.5,0.5,15,Color(0,0,0))
		
		
func _mouseUp():
	_onClick = false
	if _holdCard!=null:
		SoundManager.Play("cardd")
	_holdCard = null
	
	
func _clickCard(c:BaseCardContainer):
	c.Clicked()
	if c._cardHolder!=null:
		c._cardHolder.CardClick(c)
	

func _resetHover():
	_hoverCard = null
	
	
func _checkClickCharacter():
	if CharacterManager._onShow and UFM.PointInArea(_mousePosition,CharacterManager._characterBack.position,Vector2(200,200)):
		return
	else:
		DialogueManager.Click()
	
func _checkHoverCharacter():
	if CharacterManager._onShow:
		if UFM.PointInArea(_mousePosition,CharacterManager._characterBack.position,Vector2(200,200)):
			CharacterManager.HoverCharacter()
		else:
			CharacterManager.DeHoverCharacter()

func _checkPetCharacter():
	if CharacterManager._onShow:
		if UFM.PointInArea(_mousePosition,CharacterManager._characterBack.position,Vector2(250,250)):
			CharacterManager.PetCharacter()


func _myInit():
	_mouse = get_node("mouse")
	_mouseShadow = get_node("mouseShadow")
	GameEffectManager.RegUI(_mouse)
	GameEffectManager.RegShadow(_mouseShadow)


func _ready():
	_myInit()


func _process(delta):
	_updateMousePosition()
	_updateHoldCard()
	_checkHoverCharacter()
	#_resetHover()
