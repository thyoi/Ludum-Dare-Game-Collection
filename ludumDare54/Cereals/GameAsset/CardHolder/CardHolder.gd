class_name CardHolder
extends CanvasLayer


export var CenterPosition:Vector2
export var StepPosition:Vector2
export var Pickable:bool
export var RoStep :float
export var RoYScale : float
export var Filped :bool
export var Trash : bool

var _clickFunc : FuncRef

var _filter:CardFilter
var _cards :Array = []
var _cardsAnime : Array = []
var _addWait : Array = []
export var _addDuration : float = 0.15
var _addCount = 0


var ThioriPick : int = -1




func PopCard(c :BaseCardContainer)->BaseCardContainer:
	for i in _cards.size():
		if _cards[i] == c:
			return PopCardAt(i)
			break
	return null



func PopCardAt(index:int)->BaseCardContainer:
	if(index>=0 and index<_cards.size()):
		var res = _cards[index]
		_cards.pop_at(index)
		for i in _cardsAnime.size():
			if _cardsAnime[i] == res:
				_cardsAnime.pop_at(i)
				break
		for i in _addWait.size():
			if _addWait[i] == res:
				_addWait.pop_at(i)
				break
		return res
	else:
		return null
				
				




func CardClick(c:BaseCardContainer):
	if _clickFunc!=null:
		_clickFunc.call_func(c)

func AddCardByNamesIns(names:Array,back:int = 0):
	for i in names:
		AddCardByNameIns(i,back)


func AddCardByNameIns(name:String,back:int = 0):
	AddCardIns(CardGenerater.Card(name,back))


func AddCardIns(c:BaseCardContainer):
	if c!=null:
		c._cardHolder = self
		_cards.append(c)
		var tem = c.get_parent()
		if tem!=null:
			tem.remove_child(c)
		add_child(c)
		_cardsAnime.append(c)
		c.Position(CenterPosition,0,true)
		if Filped:
			c.Filp()


func AddCardByNames(names:Array,delay:float = 0,back:int = 0):
	for i in names:
		AddCardByName(i,delay,back)


func AddCardByName(name:String,delay:float = 0,back:int = 0):
	AddCard(CardGenerater.Card(name,back),delay)



func AddCard(c:BaseCardContainer,delay:float = 0):
	if delay>0:
		_addCount = delay
	if c!=null:
		c._cardHolder = self
		_cards.append(c)
		var tem = c.get_parent()
		if tem!=null:
			tem.remove_child(c)
		add_child(c)
		_tryAdd(c)
		if Filped:
			c.Filp()

func _tryAdd(c:BaseCardContainer):
	if _addCount<=0:
		_animeAdd(c)
		if Trash:
			TimeManager.DelayFunction(funcref(c,"CardDestory"),0.9)
	else:
		_addWait.append(c)


func _animeAdd(c:BaseCardContainer):
	SoundManager.Play("card")
	_cardsAnime.append(c)
	_addCount = _addDuration
	
	
func _updateAdd(dt:float):
	if _addCount>0:
		_addCount-=dt
	else:
		if _addWait.size()>0:
			if Trash:
				TimeManager.DelayFunction(funcref(_addWait[0],"CardDestory"),0.9)
			_animeAdd(_addWait.pop_front())


func _updatePosition():
	var tem = CenterPosition - (_cardsAnime.size()-1)*StepPosition/2
	var ic = 0
	var initR = -(_cardsAnime.size()-1)*RoStep/2
	var t = -1
	if ThioriPick>=0:
		t = ThioriPick%_cards.size()
	for i in _cardsAnime:
		if i != MouseManager._holdCard:

			var ri = initR+ic*RoStep
			var offset = Vector2(0,(tem.x+ic*StepPosition.x-CenterPosition.x)*sin(ri))*RoYScale
			if _filter == null:
				if ic == t:
					i.Position(tem+ic*StepPosition+offset+Vector2(0,-20),ri)
				else:
					i.Position(tem+ic*StepPosition+offset,ri)
			else:
				if _filter.Check(i):
					i.Position(tem+ic*StepPosition+offset + Vector2(0,-50),ri)
				else:
					i.Position(tem+ic*StepPosition+offset + Vector2(0,50),ri)
		ic += 1

func _shuffle():
	_addWait = []
	randomize()
	_cards.shuffle()
	_cardsAnime = []
	_cardsAnime.append_array(_cards)


func _updateOrder():
	var ic = 12
	for i in _cardsAnime:
		if i != MouseManager._hoverCard:
			if _filter == null:
				i.Order(ic)
			else:
				if _filter.Check(i):
					i.Order(ic+10)
				else:
					i.Order(ic)
		else :
			i.Order(40)
		ic+=1


func _myInit():
	pass
	
func _ready():
	_myInit()
	#AddCardByNames(["y0","y0","y0","y0","y0","y0","y0"])
	
	
func _process(delta):
	_updateAdd(delta)
	_updatePosition()
	_updateOrder()
	

