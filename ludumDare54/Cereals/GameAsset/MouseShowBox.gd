class_name MouseShowBox
extends CanvasLayer

var _line :Line2D
var _box : Polygon2D
var _label :Label

var _StopDTime = 0.5
var _StopCount = 0
var _lastMousePosition
var _onShow = false


func _updateHover(dt:float):
	if _lastMousePosition == MouseManager._mousePosition and MouseManager._hoverCard!=null and MouseManager._hoverCard._filped == false:
		if _onShow == false:
			_StopCount+=dt
			if _StopCount >=_StopDTime:
				_showB()
	else:
		_StopCount = 0
		_hideB()
	_lastMousePosition = MouseManager._mousePosition

func _updateContent():
	_setSize(_label.rect_size+Vector2(7,2))
	offset = MouseManager._mousePosition + Vector2(15,-12)


func _showB():
	_onShow = true
	visible = true
	_label.text = _textFromC(MouseManager._hoverCard)
	TimeManager.DelayFunction(funcref(self,"_updateContent"),0)
	
func _hideB():
	_onShow = false
	visible = false

func _setSize(s:Vector2):
	var tem = PoolVector2Array()
	tem.append(Vector2(0,s.y/2))
	tem.append(Vector2(0,0))
	tem.append(Vector2(s.x,0))
	tem.append(Vector2(s.x,s.y))
	tem.append(Vector2(0,s.y))
	tem.append(Vector2(0,s.y/2))
	_line.points = tem
	_box.polygon = tem
	

func _textFromC(c:BaseCardContainer)->String:
	if c == null:
		return ""
	var res = "Num:"
	if c.NumValues.size() == 0:
		res +=" none"
	else:
		for i in c.NumValues:
			res +=" "+String(i)
	if c.ColorValues.size()>0:
		res +="\nColor:"
		if c.ColorValues.size()>=4:
			res +=" all"
		elif c.ColorValues[0] == 0:
			res +=" red"
		elif c.ColorValues[0] == 1:
			res +=" yellow"
		elif c.ColorValues[0] == 2:
			res +=" green"
		elif c.ColorValues[0] == 3:
			res +=" blue"
		
	if c.Discraible!="":
		res +="\n"+c.Discraible
	return res

func _myInit():
	_line = get_node("Line2D")
	_box = get_node("Polygon2D")
	_label = get_node("Label")

func _ready():
	_myInit()


func _process(delta):
	_updateHover(delta)
