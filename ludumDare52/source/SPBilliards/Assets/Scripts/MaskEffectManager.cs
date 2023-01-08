using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaskEffectManager : MonoBehaviour
{
    public Transform[] Masks;
    public float[] Scales;
    public AnimationCurve[] Curves;
    public float Size;
    public float Speed;

    private float CurSize = 15;

    public void UpdateSize()
    {
        CurSize = UF.Lerp(CurSize, Size, Speed, TimeManager.DT());
    }


    // Update is called once per frame
    void Update()
    {
        UpdateSize();
        for(int i = 0; i < Masks.Length; i++)
        {
            float tem = Scales[i] * Curves[i].Evaluate(CurSize / 20) * 20;
            Masks[i].localScale = new Vector3(tem,tem, 1);
        }
    }
}
