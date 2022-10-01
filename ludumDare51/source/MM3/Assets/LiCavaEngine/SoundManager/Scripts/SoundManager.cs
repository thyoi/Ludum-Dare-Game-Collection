using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    static SoundManager mainManager;
    static public void PlaySound(string name)
    {
        mainManager.Play(name);
    }


    public AudioData[] audios;
    public GameObject audioPrefab;

    private Dictionary<string,AudioPlayer> audioDic;
    private Transform myTransform;

    public void InitSound()
    {
        audioDic = new Dictionary<string, AudioPlayer>();
        foreach(AudioData ad in audios)
        {
            audioDic[ad.name] = new AudioPlayer(ad, audioPrefab, myTransform);
        }
    }
    public void Awake()
    {
        if (!(mainManager is null))
        {
            Debug.LogError("Repetitive SsoundManager");
        }
        mainManager = this;
        myTransform = transform;
        InitSound();
    }

    public void Play(string name)
    {
        if (audioDic.ContainsKey(name))
        {
            audioDic[name].Play();
        }
    }

}


[System.Serializable]
public struct AudioData
{
    public string name;
    public AudioClip audio;
    public int mulNum;
    public float volum;
    public float pitch;
    
}

public class AudioPlayer
{

    private AudioSource[] audios;
    private int counter;

    public AudioPlayer(AudioData audioData, GameObject audioPrefab, Transform target)
    {
        audios = new AudioSource[audioData.mulNum];
        for(int i = 0; i < audioData.mulNum; i++)
        {
            audios[i] = MonoBehaviour.Instantiate(audioPrefab, target).transform.GetComponent<AudioSource>();
            audios[i].volume = audioData.volum;
            audios[i].pitch = audioData.pitch;
            audios[i].clip = audioData.audio;
        }
        counter = 0;
    }

    public void Play()
    {
        audios[counter].Play();
        counter++;
        if (counter >= audios.Length)
        {
            counter = 0;
        }
    }
}
