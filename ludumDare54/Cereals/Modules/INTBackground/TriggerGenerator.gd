class_name TriggerGenerator


static func Trigger(name:String)->TriggerData:
	if name == "full_round_ac_1":
		var res = TriggerData.new()
		res.Duration = 1
		res.AnimationCurveName = "ac0"
		res.EndSize = 1300
		res.Type = RoundTrigger.TriggerType.Round
		res.revers = false
		return res
	elif name == "full_diamond_ac_1":
		var res = TriggerData.new()
		res.Duration = 1
		res.AnimationCurveName = "ac0"
		res.EndSize = 1300
		res.Type = RoundTrigger.TriggerType.Diamond
		res.revers = false
		return res
	elif name == "full_rectangle_ac_1":
		var res = TriggerData.new()
		res.Duration = 1
		res.AnimationCurveName = "ac0"
		res.EndSize = 1300
		res.Type = RoundTrigger.TriggerType.Rectangular
		res.revers = false
		return res
		
	elif name == "fullR_round_ac_1":
		var res = TriggerData.new()
		res.Duration = 1
		res.AnimationCurveName = "ac0"
		res.EndSize = 1300
		res.Type = RoundTrigger.TriggerType.Round
		res.revers = true
		return res
	elif name == "fullR_diamond_ac_1":
		var res = TriggerData.new()
		res.Duration = 1
		res.AnimationCurveName = "ac0"
		res.EndSize = 1300
		res.Type = RoundTrigger.TriggerType.Diamond
		res.revers = true
		return res
	elif name == "fullR_rectangle_ac_1":
		var res = TriggerData.new()
		res.Duration = 1
		res.AnimationCurveName = "ac0"
		res.EndSize = 1300
		res.Type = RoundTrigger.TriggerType.Rectangular
		res.revers = true
		return res
	
	else:
		return null
