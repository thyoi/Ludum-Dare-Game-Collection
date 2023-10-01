extends Node


var _cardDic:Dictionary
const y0 = preload("res://GameAsset/Cards/allPrefab/uno/unoY0.tscn")
const yq = preload("res://GameAsset/Cards/allPrefab/uno/unoYq.tscn")


const pa0 = preload("res://GameAsset/Cards/allPrefab/poker/pokerA0.tscn")
const pa1 = preload("res://GameAsset/Cards/allPrefab/poker/pokerA1.tscn")
const p30 = preload("res://GameAsset/Cards/allPrefab/poker/poker30.tscn")
const p31 = preload("res://GameAsset/Cards/allPrefab/poker/poker31.tscn")
const p40 = preload("res://GameAsset/Cards/allPrefab/poker/poker40.tscn")
const p60 = preload("res://GameAsset/Cards/allPrefab/poker/poker60.tscn")
const p70 = preload("res://GameAsset/Cards/allPrefab/poker/poker70.tscn")
const p90 = preload("res://GameAsset/Cards/allPrefab/poker/poker90.tscn")
const p91 = preload("res://GameAsset/Cards/allPrefab/poker/poker91.tscn")



const back0 = preload("res://GameAsset/Cards/art/other/back0.png")
const back1 = preload("res://GameAsset/Cards/art/other/back.png")


var backList : Array



func _regCardPrefab():
	_cardDic["y0"] = y0
	_cardDic["yq"] = yq
	
	_cardDic["pa0"] = pa0
	_cardDic["pa1"] = pa1
	_cardDic["p30"] = p30
	_cardDic["p31"] = p31
	_cardDic["p40"] = p40
	_cardDic["p60"] = p60
	_cardDic["p70"] = p70
	_cardDic["p90"] = p90
	_cardDic["pa91"] = p91


func _ready():
	_cardDic = Dictionary()
	_regCardPrefab()
	backList = []
	backList.append(back0)
	backList.append(back1)
	

func Card(name:String,back:int = 0)->BaseCardContainer:
	if _cardDic.has(name):
		var tem : BaseCardContainer =  _cardDic[name].instance()
		tem.ChangeCardBack(backList[back])
		tem._genName = name
		return tem
	else:
		return null
