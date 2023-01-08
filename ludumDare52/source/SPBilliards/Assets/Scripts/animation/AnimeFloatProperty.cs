using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[Serializable]
public class AnimeFloatProperty
{
    public enum LoopMode
    {
        None,
        Loop,
        BackLoop
    }

    public bool Ignore;
    public float End;
    public float Init;
    public float Time;
    public float Delay;
    public AnimationCurve Curve;
    public UF.AnimeCallback Callback;
    public UF.AnimeCallback DelayCallback;
    public LoopMode Loop;
    public bool KeepCallback;

    public float GetProges()
    {
        return count / Time;
    }

    public void Copy(AnimeFloatProperty a)
    {
        this.Ignore = a.Ignore;
        this.End = a.End;
        this.Init = a.Init;
        this.Time = a.Time;
        this.Curve = a.Curve;
        this.Callback = a.Callback;
        this.Loop = a.Loop;
    }


    private bool reverse;
    private float cur;
    private bool active;
    private float count;

    private void UpdateCur()
    {
        if (reverse)
        {
            cur = Init + (End - Init) * Curve.Evaluate((Time-count)/ Time);
        }
        else
        {
            cur = Init + (End - Init) * Curve.Evaluate(count / Time);
        }
        
    }

    public AnimeFloatProperty()
    {
        cur = Init;
        count = 0;
        active = false;
    }
    public float Cur
    {
        get { return cur; }
    }
    public void StartAnime(UF.AnimeCallback cb = null)
    {
        reverse = false;
        if (cb != null)
        {
            Callback = cb;
        }
        count = 0;
        active = true;

    }

    public bool Update(float dt)
    {
        if (active)
        {
            UpdateCur();
            if (Delay > 0)
            {
                Delay -= dt;
                if (Delay <= 0)
                {
                  
                    if (DelayCallback != null)
                    {
                        DelayCallback();
                        DelayCallback = null;
                        
                    }
                }
                return true;
                
            }
            else
            {
                
                count += dt;
                if (count >= Time)
                {

                    switch (Loop)
                    {
                        case LoopMode.None:
                            count = Time;
                            active = false;
                            break;
                        case LoopMode.Loop:
                            count = 0;
                            break;
                        case LoopMode.BackLoop:
                            count = 0;
                            reverse = !reverse;
                            break;
                    }
                    if (Callback != null)
                    {
                        Callback();
                        if (!KeepCallback)
                        {
                            Callback = null;
                        }
                    }
                }
                //UpdateCur();
            }
            return true;

        }
        else
        {
            return false;
        }
    }

}
