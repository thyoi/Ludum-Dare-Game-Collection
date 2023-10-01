extends Node


var _shadowLayer : Viewport
var _shadowSprite : Sprite
const _cardShadowPrefab = preload("res://GameAsset/prefab/CardShadow.tscn")


func ShadowColor(c:Color):
	_shadowSprite.material.set_shader_param("c", c)


func CardShadow()->ShrinkDisapper:
	var tem = _cardShadowPrefab.instance()
	print(tem)
	_shadowLayer.add_child(tem)
	return tem


func _myInit():
	_shadowLayer = get_node("show/Viewport2/temSprite1/Viewport1")
	_shadowSprite = get_node("show")

func _ready():
	_myInit()


func _process(delta):
	pass
