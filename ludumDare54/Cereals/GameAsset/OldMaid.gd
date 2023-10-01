class_name OldMaid
extends Node


export var ThioriCurve : Curve

var _thioriDuration : float = 2
var _thioriCardNum :int =15
var _ThioriCount:float
var _onThiori:bool = false
var _lastCardT :int

var ChoosedCard:int


var _center : CardHolder
var _player: CardHolder
var _thiori: CardHolder
var _trash : CardHolder


var _shed : MyButton
var _take :   MyButton
var _tear :   MyButton

var _state : int

var _centerCard : BaseCardContainer

var _torShed0 :bool = false
var _torShed1 :bool = false
var _torShed2 :bool = false
var _torShed3 :bool = true
var _initer 


var _deathCard :BaseCardContainer
var _death : DeathCard


var _curPlayer
var _shedCards :Array = []


func _startThiori():
	_onThiori = true
	_ThioriCount
	_lastCardT = -1
	

func _updateThiori(dt:float):
	_ThioriCount+=dt
	if(_ThioriCount>_thioriDuration):
		_onThiori = false
		_ThioriCount  =0
		_ThioriDrawTOCentwe()
	var tem = floor(ThioriCurve.interpolate(_ThioriCount/_thioriDuration)*_thioriCardNum)
	if tem>_lastCardT:
		SoundManager.Play("pick")
	_lastCardT = tem
	_player.ThioriPick = ChoosedCard+20-tem
	


func _ThioriDrawTOCentwe():
	_player.CenterPosition = Vector2(480,600)
	_thiori.CenterPosition = Vector2(480,30)
	_centerCard = _player._cards[ChoosedCard]
	_center.AddCard(_player.PopCardAt(ChoosedCard))
	_player.ThioriPick = -1
	_curPlayer = _thiori
	TimeManager.DelayFunction(funcref(self,"_thioriDeside"),1)
	
	
func _thioriDeside():
	if(_centerCard.Tear!=""):
		Tear()
	else:
		var sh = -1
		for i in _thiori._cards.size():
			if _centerCard.Check(_thiori._cards[i]):
				sh = i
		if sh == -1:
			Take()
		else:
			_thiori._cards[sh].Filp()
			_center.AddCard(_thiori.PopCardAt(sh))
			TimeManager.DelayFunction(funcref(self,"Shed"),1)
	_thiori.CenterPosition = Vector2(480,-120)





func DestroyCard(c):
	_trash.AddCard(c)


func _shedF(name:String):
	pass
func _takeF(name:String):
	pass
func _tearF(name:String):
	pass
func _flipF(name:String):
	if name == "":
		GameEffectManager.MatrixBoom(_centerCard._curPosition,1,2)
	elif name == "p0":
		GameEffectManager.MatrixBoom(_centerCard._curPosition,1,2)
		GameEffectManager.MatrixParticalChange(_centerCard._curPosition,1,"mp0","l00")
	elif name == "p1":
		GameEffectManager.MatrixBoom(_centerCard._curPosition,1,2)
		GameEffectManager.MatrixParticalChange(_centerCard._curPosition,1,"mp1","l01")
	elif name == "p2":
		GameEffectManager.MatrixBoom(_centerCard._curPosition,1,2)
		GameEffectManager.MatrixParticalChange(_centerCard._curPosition,1,"mp2","l10")
	elif name == "p3":
		GameEffectManager.MatrixBoom(_centerCard._curPosition,1,2)
		GameEffectManager.MatrixParticalChange(_centerCard._curPosition,1,"mp3","l11")
func _boomF(c:Color):
	c.a = 1
	SParticalGenerater.ParticalBoom(_centerCard._curPosition,["rp0"],1,0.5,60,c,true)

func _cleanCallBack():
	_center._clickFunc = null
	_player._clickFunc = null
	_thiori._clickFunc = null

func ActiveButton(b:int):
	if b == 0 :
		_shed.Show()
		_take.Hide()
		_tear.Hide()
	elif b ==1:
		_shed.Hide()
		_take.Show()
		_tear.Hide()
	elif b ==2:
		_shed.Hide()
		_take.Hide()
		_tear.Show()
	else:
		_shed.Hide()
		_take.Hide()
		_tear.Hide()


func Shed():
	_torShed1 = true
	_shedF(_centerCard.Shed)
	if _curPlayer == _player:
		_shedCards.append(_center._cards[0]._genName)
		_shedCards.append(_center._cards[1]._genName)
	DestroyCard(_center.PopCardAt(1))
	while _center._cards.size()>0:
		DestroyCard(_center.PopCardAt(0))
	ReadyToDrawed()
	_player._filter = null
	ActiveButton(-1)
	_cleanCallBack()
	
	
func Take():
	if _torShed1:
		_takeF(_centerCard.Take)
		_curPlayer.AddCard(_center.PopCard(_centerCard))
		ReadyToDrawed()
		_player._filter = null
		ActiveButton(-1)
		_cleanCallBack()
	
func Tear():
	_tearF(_centerCard.Tear)
	DestroyCard(_center.PopCardAt(0))
	ReadyToDrawed()
	_player._filter = null
	ActiveButton(-1)
	_cleanCallBack()

func _centerTakeBack(c):
	if _center._cards.size()>1:
		_curPlayer.AddCard(_center.PopCardAt(1))
		ActiveButton(1)
	else:
		Take()
		
func _tryShed(c):
	if _centerCard.Check(c):
		if _center._cards.size()>1:
			_curPlayer.AddCard(_center.PopCardAt(1))
			_center.AddCard(_curPlayer.PopCard(c))
		else:
			_center.AddCard(_curPlayer.PopCard(c))
		ActiveButton(0)


func DecideCenterCard():
	if _torShed0 == false:
		_torShed0 = true
		_initer._StartConverSation2_0(funcref(self,"DecideCenterCard"))
		
	else:
		_cleanCallBack()
		if _centerCard.Tear!="":
			ActiveButton(2)
		else:
			_player._filter = _centerCard.Filter()
			ActiveButton(1)
			_center._clickFunc = funcref(self,"_centerTakeBack")
			_player._clickFunc = funcref(self,"_tryShed")

func _flipCenterCard():

	_flipF(_centerCard.FlipEvent)
	_boomF(_centerCard.BoomColor)
	SoundManager.Play("gc")
	_centerCard.Clicked()

func _drawFromThiori(c):
	if _torShed3:
		var tem = 0
		var tem2 = 0
		for i in _thiori._cards.size():
			if c == _thiori._cards[i]:
				tem = i
		for i in _thiori._cardsAnime.size():
			if c == _thiori._cardsAnime[i]:
				tem2 = i
		_deathCard.Position(_thiori._cards[tem]._curPosition,-1)
		_thiori._cards[tem] = _deathCard
		_thiori._cardsAnime[tem2] = DeathCard
		_player.CenterPosition = Vector2(480,540)
		_thiori.CenterPosition = Vector2(480,-120)
		_center.AddCard(_thiori.PopCardAt(tem))
		_centerCard = _deathCard
		TimeManager.DelayFunction(funcref(self,"_flipCenterCard"),0.6)
		TimeManager.DelayFunction(funcref(c,"Filp"),0.4)
		TimeManager.DelayFunction(funcref(self,"DecideCenterCard"),1)
	else:
		
		_cleanCallBack()
		_player.CenterPosition = Vector2(480,540)
		_thiori.CenterPosition = Vector2(480,-120)
		_center.AddCard(_thiori.PopCard(c))
		_centerCard = c
		TimeManager.DelayFunction(funcref(self,"_flipCenterCard"),0.6)
		TimeManager.DelayFunction(funcref(c,"Filp"),0.4)
		TimeManager.DelayFunction(funcref(self,"DecideCenterCard"),1)

func DrawFromThiori():
	_cleanCallBack()
	_curPlayer = _player
	_player.CenterPosition = Vector2(480,620)
	_thiori.CenterPosition = Vector2(480,100)
	_thiori._clickFunc = funcref(self,"_drawFromThiori")
	_thiori._shuffle()



func _myInit():
	_shed = get_node("buttons/shed")
	_take = get_node("buttons/take")
	_tear = get_node("buttons/tear")
	
	_shed._clickCallBack = funcref(self,"Shed")
	_take._clickCallBack = funcref(self,"Take")
	_tear._clickCallBack = funcref(self,"Tear")
	
	_player = get_node("player")
	_center = get_node("center")
	_thiori = get_node("thiori")
	_trash = get_node("trash")
	_initer = get_parent().get_node("Initer")
	_death = get_node("death")
	_deathCard = get_node("death/de") 
	
	
func ReadyToDrawed():
	if _curPlayer == _player:
		TimeManager.DelayFunction(funcref(self,"_readyToDrawed"),0.7)
	if _curPlayer == _thiori:
		TimeManager.DelayFunction(funcref(self,"DrawFromThiori"),0.7)
	

func _readyToDrawed():
	_player.CenterPosition = Vector2(480,410)
	_cleanCallBack()
	_player.Pickable = false
	if _torShed2 == false:
		_torShed2 = true
		_torShed3 = false
		_initer._StartConverSation3_0(funcref(self,"_thioriJudge"))
	else:
		_thioriJudge()
		
func _thioriJudge():
	if _death._inPlayer && _death._deathCount<3:
		if randf()>0.5:
			for i in _player._cards.size():
				if _player._cards[i] == _deathCard:
					ThioriPickAt(i)
	ThioriPickAt(UFM.RandomInt(0,_player._cards.size()-1))
			
func ThioriPickAt(n:int):
	ChoosedCard = n
	_startThiori()


func _ready():
	_myInit()


func _process(delta):
	if(_onThiori):
		_updateThiori(delta)
