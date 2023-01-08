using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowBall : MonoBehaviour
{
    public Transform mainBall;
    public float pt;
    public Color pc;
    public Rigidbody2D myRigidBody;
    public float Speed = 1;
    
    private float count;
    
    // Start is called before the first frame update
    void Start()
    {
        mainBall = BallCounter.MainBallTransform();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 dir = mainBall.position - transform.position;
        myRigidBody.AddForce(dir.normalized * 0.5f * Time.deltaTime*Speed, ForceMode2D.Impulse);
        UF.SetRotationZ(transform, Vector2.Angle(Vector2.right, dir) * ((dir.y > 0) ? 1 : -1) *((Speed>0)?1:-1));
        count += Time.deltaTime;
        if (count > pt)
        {
            count = 0;
            particalManager.GlobalManager.BoomParticalBust(1, transform.position, pc, 0.5f*Speed, true);
        }
    }
}
