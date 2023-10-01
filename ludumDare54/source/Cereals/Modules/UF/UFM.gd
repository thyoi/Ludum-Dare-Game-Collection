class_name UFM


const sq2 = sqrt(2)
const sq3 = sqrt(3)


static func _floatNormalize(f:float)->float:
	if(f>=0):
		return 1.0
	else:
		return -1.0
		
		
static func _floatOverflow(from:float,to:float,f:float)->bool:
	if(from<=to):
		return f>=to
	else:
		return f<=to
		
		
static func _vector2Overflow(from:Vector2,to:Vector2,v:Vector2)->bool:
	if(from.x!=to.x):
		return _floatOverflow(from.x,to.x,v.x)
	else:
		return _floatOverflow(from.y,to.y,v.y)


static func FloatShift(from:float, to:float, w:float, m:float)->float:
	if(from == to):
		return from
	else:
		var dis = to-from
		var res = from + dis*w + m*_floatNormalize(dis)
		if(_floatOverflow(from,to,res)):
			return to
		else:
			return res


static func Vector2Shift(from:Vector2,to:Vector2,w:float,m:float)->Vector2:
	if(from == to):
		#print ("from:",from," to:",to)
		return from
	else:
		var dis = to-from
		var res = from + dis*w+m*dis.normalized()
		#print ("from:",from," to:",to," res:",res," dis:",dis," w:",w)
		if(_vector2Overflow(from,to,res)):
			return to
		else:
			return res


static func Lerp(from:float, to:float,w:float)->float:
	return from+(to-from)*w


static func Vector2Lerp(from:Vector2,to:Vector2,w:float)->Vector2:
	return from+(to-from)*w


static func RadianVector(a:float,l:float = 1)->Vector2:
	return Vector2(sin(a)*l,cos(a)*l)


static func RegularPolygonPoints(n:int, r:float,l:float = 100)->PoolVector2Array:
	var res = PoolVector2Array()
	for i in n:
		res.append(RadianVector(r+i*2*PI/n,l))
	return res
	

static func LoopPoints(p:PoolVector2Array)->PoolVector2Array:
	var res = PoolVector2Array()
	res.append(Vector2Lerp(p[0],p[-1],0.5))
	res.append_array(p)
	res.append(res[0])
	return res
	
	
static func DistantSquare(a : Vector2,b:Vector2)->float:
	return a.distance_squared_to(b)
	
	
static func DiamondDistant(a : Vector2, b:Vector2)->float:
	var x = Vector2(a.x-a.y,a.x+a.y)
	var y = Vector2(b.x-b.y,b.x+b.y)
	return RectangleDistant(x,y,1)
	
	
static func RectangleDistant(a : Vector2,b:Vector2, w_l:float = 0.8)->float:
	var x = abs((a.x-b.x)*w_l)
	var y = abs(a.y-b.y)
	return Large(x,y)

static func Large(a,b):
	if a>b:
		return a
	else:
		return b
		
static func Less(a,b):
	if(a<b):
		return a
	else:
		return b


static func DiamondPosition(center:Vector2, s:float,i:int,j:int,x:int,y:int)->Vector2:
	if i%2==1:
			return Vector2(center.x+(j-x)*s+s/2, center.y-y*s+i*s/2)
	else:
			return Vector2(center.x+(j-x)*s, center.y-y*s+i*s/2)


static func TrianglePosition(center:Vector2, s:float,i:int,j:int,x:int,y:int)->Vector2:
	var sx = s/1.2
	var sy = s*sq3/1.2
	if i%2==1:
			return Vector2(center.x+(j-x)*sx+sx/2, center.y-y*sy+i*sy/2)
	else:
			return Vector2(center.x+(j-x)*sx, center.y-y*sy+i*sy/2)

