using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager GlobleManager;
    public static void Play(string s)
    {
        GlobleManager.PlaySound(s);

    }

    public static void Play(string s, float f)
    {
        GlobleManager.PlaySound(s,f);
    }

    public static void Stop(string s)
    {
        GlobleManager.StopSound(s);
    }

    public SoundManager()
    {
        GlobleManager = this;
    }
    public SoundItem[] Sounds;

    public void PlaySound(string s)
    {
        for(int i = 0; i < Sounds.Length; i++)
        {
            if (Sounds[i].name == s)
            {
                Sounds[i].Play();
                return;
            }
        }
    }
    public void PlaySound(string s, float f)
    {
        for (int i = 0; i < Sounds.Length; i++)
        {
            if (Sounds[i].name == s)
            {
                Sounds[i].Play(f);
                return;
            }
        }
    }
    public void StopSound(string s)
    {
        for (int i = 0; i < Sounds.Length; i++)
        {
            if (Sounds[i].name == s)
            {
                Sounds[i].Stop();
                return;
            }
        }
    }



}

[System.Serializable]
public class SoundItem
{
    public string name;
    public AudioSource[] audios;


    private int count = 0;

    public void Play()
    {
        audios[count].Play();
        count++;
        if (count >= audios.Length)
        {
            count = 0;
        }
    }
    public void Play(float f)
    {
        audios[count].volume = f;
        audios[count].Play();
        count++;
        if (count >= audios.Length)
        {
            count = 0;
        }
    }

    public void Stop()
    {
        count--;
        if (count < 0)
        {
            count = audios.Length - 1;
        }
        audios[count].Stop();
    }
   


}
