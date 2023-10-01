extends Node


var _shadowLayer : Viewport
var _blurShadowLayer :Viewport
var _shadowSprite : Sprite
const _cardShadowPrefab = preload("res://GameAsset/prefab/CardShadow.tscn")
var _uiLayer : CanvasLayer
var _backGround : BackgroundManager
var _matrix : ParticalMatrixManager

var _deleteThing : Node
var _particalB : CanvasLayer
var _particalF : CanvasLayer



func MatrixBoom(position : Vector2,type : int,ten:int = 0):
	_matrix.MatrixBoom(position,type,ten)
	
	
func MatrixParticalChange(position :Vector2,type : int,particalName:String,sliceType:String = ""):
	_matrix.MatrixParticalChange(position,type,particalName,sliceType)



func ChangeBackGround(c:Color):
	_backGround.Boom(c)

func ChangePartical(matrixIndex : int,triggerName:String, particalName:String,moveType:int,p:int,delay:float):
	_matrix.To(matrixIndex,triggerName,particalName,moveType,p,delay)


func ShadowColor(c:Color):
	_shadowSprite.material.set_shader_param("c", c)


func CardShadow()->ShrinkDisapper:
	var tem = _cardShadowPrefab.instance()
	_shadowLayer.add_child(tem)
	return tem
	
	
func RegUI(n):
	if n.get_parent() != null:
		n.get_parent().remove_child(n)
	_uiLayer.add_child(n)


func RegShadow(n):
	if n.get_parent() != null:
		n.get_parent().remove_child(n)
	_shadowLayer.add_child(n)
	
func RegBlurShadow(n):
	if n.get_parent() != null:
		n.get_parent().remove_child(n)
	_blurShadowLayer.add_child(n)


func RegPartical(n,back:bool = false):
	if n.get_parent() != null:
		n.get_parent().remove_child(n)
	if back:
		_particalB.add_child(n)
	else:
		_particalF.add_child(n)



func _deleteThings():
	_deleteThing.queue_free()


func _myInit():
	_shadowLayer = get_node("shadow/show/Viewport2/temSprite1/Viewport1")
	_blurShadowLayer = get_node("shadow/show/Viewport2/temSprite2/Viewport1")
	_shadowSprite = get_node("shadow/show")
	_uiLayer = get_node("UIlayer")
	_backGround = get_node("backGround")
	_matrix = get_node("matrix/MatrixManager")
	_deleteThing = get_node("backGround/Label")
	_particalB = get_node("cardBackPartical")
	_particalF = get_node("cardFrontPartical")

func _ready():
	_myInit()


func _process(delta):
	pass
