class_name GameIniter
extends Node


var _initCard : BaseCardContainer
var _center : CardHolder
var _player : CardHolder
var _thiori : CardHolder
var _start : InitItemsCountroler
var _delete : CanvasLayer

var _oldmaid : OldMaid


export var BackGround0:Color
export var BackGround1:Color


func _deleteShow():
	_delete.queue_free()

func _StartConverSation1_0():
	CharacterManager.Show("c9",Vector2(140,160))
	TimeManager.DelayFunction(funcref(self,"_StartConverSation1_1"),0.6)
	

func _StartConverSation1_1():
	DialogueManager.StartDia(["Hi. Thanks for coming to play with me.",
	"I hope you enjoy playing old maid. sorry that's all I have.",
	"Don't worry if you're unfamiliar with it. It's really simple.",
	"We take turns drawing cards. Now, your turn."],Vector2(580,200),funcref(self,"_StartTor"),["c3","c1","c7"])


func _StartTor():
	_oldmaid.DrawFromThiori()
	#CharacterManager.Hide()
	

func _StartConverSation2_0(f:FuncRef):
	CharacterManager.Show("c6",Vector2(140,160))
	DialogueManager.StartDia(["having matching cards in your hand makes you \nable to discard them.",
	"While you can opt to keep them, remember, \nthe goal is to be the first to empty your hand.",
	"Check and see if you can discard any now",],Vector2(580,160),f,["c4","c5"])
	
	
func _StartConverSation3_0(f:FuncRef):
	CharacterManager.Show("c1",Vector2(140,160))
	DialogueManager.StartDia(["That's right! Now it's my turn.",],Vector2(580,160),f,["c4","c5"])

func _StartLevel(c:BaseCardContainer):
	_start.Disapper()
	GameEffectManager.ChangeBackGround(BackGround0)
	GameEffectManager.ChangePartical(0,"full_round_ac_1","l4",0,0,0)
	_center._clickFunc = null
	_player.AddCard(_center.PopCardAt(0),1.4)
	_player.AddCardByNames(["p30","p90","p60","p40","p70"])
	_thiori.AddCardByNamesIns(["p31","p31","p31","p31","p31","p31","p31"])
	TimeManager.DelayFunction(funcref(GameEffectManager,"_deleteThings"),1.6)
	TimeManager.DelayFunction(funcref(self,"_StartConverSation1_0"),3)
	SoundManager.Play("start")
	MouseManager._torPoint = false;
	


func _ready():
	_initCard = get_child(0)
	_center = get_parent().get_node("oldMaid/center")
	_center.AddCardIns(_initCard)
	_center._clickFunc = funcref(self,"_StartLevel")
	_start = get_parent().get_node("Start/Viewport2/Node7")
	_player  = get_parent().get_node("oldMaid/player")
	_thiori = get_parent().get_node("oldMaid/thiori")
	var show = get_parent().get_node("CanvasLayer/showMask/Viewport/Node2D") as ShrinkDisapper
	show.Start()
	_delete = get_parent().get_node("CanvasLayer")
	TimeManager.DelayFunction(funcref(self,"_deleteShow"),0.7)
	_oldmaid = get_parent().get_node("oldMaid")
	

func _process(delta):
	pass
