class_name SlotAnimationData


var Type:TypeEventData =null
var Size:FloatEventData = null


func Init(d:SlotAnimationIns):
	Size = d.PackSize()
	Type = d.PackType()


func Copy(d:SlotAnimationData):
	if d.Type != null:
		Type = TypeEventData.new()
		Type.Copy(d.Type)
	if d.Size != null:
		Size = FloatEventData.new()
		Size.Copy(d.Size)

