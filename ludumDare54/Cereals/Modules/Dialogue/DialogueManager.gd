extends Node


export var TextTime: float


var _diaList: Array
var _endCallBack : FuncRef
var _characterList :Array
var _onDia:bool = false
var _diaCount:float

var _diaBox :DialogueBox

var _currentText : String 
var _textCount : float
var _timeCount : float
var _onTexting : bool
var _textFinish : bool

func StartDiaDelay():
	StartText(_diaList[0])


func StartDia(ss:Array,p:Vector2,f:FuncRef = null,cl:Array = []):
	_diaBox.Position(p)
	_diaBox.Scale(Vector2(1,1))
	_onDia = true
	_diaCount  = 0
	_diaList = ss
	_endCallBack = f
	_characterList = cl
	TimeManager.DelayFunction(funcref(self,"StartDiaDelay"),0.3)



func StartText(s:String):
	_currentText = s
	var tem = s.count("\n")
	_diaBox.TextYOffset(-tem*23/2)
	_onTexting = true
	_timeCount = 0
	_textCount = 0
	_textFinish = false
	
	
func updateText(dt:float):
	_timeCount+=dt
	if(_timeCount>=TextTime):
		
		_timeCount=0
		_textCount+=1
		
		if _textCount > _currentText.length():
			_onTexting = false
			_textFinish = true
		else:
			_diaBox.SetText(_currentText.substr(0,_textCount))
		if _textCount<_currentText.length() and _currentText[_textCount] != ' ':
			SoundManager.Play("c")
	


func Click():
	if _onDia:
		if _textFinish == false:
			_diaBox.SetText(_currentText)
			_textFinish = true
			_onTexting = false
		else:
			if _diaCount<_characterList.size():
				CharacterManager.Sprite(_characterList[_diaCount])
			_diaCount+=1
			if(_diaCount>=_diaList.size()):
				if _endCallBack !=null:
					_endCallBack.call_func()
				_diaBox.Disapper()
				_onDia = false
				CharacterManager.Hide()
			else:
				StartText(_diaList[_diaCount])




func _myInit():
	_diaBox = get_node("CanvasLayer")


func _ready():
	_myInit()


func _process(delta):
	if _onTexting:
		updateText(delta)
