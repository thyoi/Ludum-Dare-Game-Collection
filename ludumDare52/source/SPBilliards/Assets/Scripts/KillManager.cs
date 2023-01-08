using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class KillManager : MonoBehaviour
{
    public static KillManager GlobalManager;
    
    public static void CRK(Vector2 p,float s, Color c)
    {
        GlobalManager.CreateRoundKill(p, s, c);

    }

    public KillManager()
    {
        GlobalManager = this;
    }



    public GameObject RoundKill;
    public GameObject RectKill;
    public GameObject BulletKill;



    public void CreateRoundKill(Vector2 p, float size,Color c)
    {
        Transform tem = particalManager.GlobalManager.CreateShinePartical(p, size, c, true);
        GameObject ttem = Instantiate(RoundKill);
        ttem.transform.GetComponent<RoundKill1>().round = tem;
        BackEffect.BoomAt(p, 0);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
