class_name TypeEventData


var Priority : int
var Name : String
var Duration:float


func Copy(d:TypeEventData):
	if(d!=null):
		Priority = d.Priority
		Name = d.Name
		Duration = d.Duration
