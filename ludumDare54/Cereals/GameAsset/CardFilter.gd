class_name CardFilter


var IntValue : Array
var ColorValue :Array


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
	for i in IntValue:
		if _hasSameIntValue(i,c):
			return true
	for i in ColorValue:
		if _hasSameColorValue(i,c):
			return true
	return false
