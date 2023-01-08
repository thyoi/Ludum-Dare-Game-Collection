using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RodeKill : MonoBehaviour
{
    // Start is called before the first frame update
    public AnimeCountroler[] RoadUnits;
    public float StepTime;

    public void InitRode()
    {
        for(int i = 0; i < RoadUnits.Length; i++)
        {
            RoadUnits[i].Apha.Delay = i * StepTime;
            RoadUnits[i].StartAnime();
        }
    }



    public void EndRode()
    {
        for (int i = 0; i < RoadUnits.Length; i++)
        {
            RoadUnits[i].Apha.Loop = AnimeFloatProperty.LoopMode.None;
        }
    }



    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
