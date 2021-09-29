using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

   [SerializeField, Range(0, 1), Tooltip("マスター音量")]
    float volume = 1;

    [SerializeField]
    private AudioClip[] bgm;
    [SerializeField]
    private AudioClip[] se;
 
    AudioSource bgmAudioSource;
    AudioSource seAudioSource;
 
    public float Volume
    {
        set
        {
            volume = Mathf.Clamp01(value);
            bgmAudioSource.volume = volume;
            seAudioSource.volume = volume;
        }
        get
        {
            return volume;
        }
    }
    public void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
 
        bgmAudioSource = gameObject.AddComponent<AudioSource>();
        seAudioSource = gameObject.AddComponent<AudioSource>();
        volume = 0.2f;
    }

    public void PlayBgm(int index)
    {
        bgmAudioSource.clip = bgm[index];
        bgmAudioSource.loop = true;
        bgmAudioSource.Play();
    }

    public void StopBgm()
    {
        bgmAudioSource.Stop();
        bgmAudioSource.clip = null;
    }
 
    public void PlaySe(int index,float x)
    {
        seAudioSource.PlayOneShot(se[index],Volume * x);
    }
 
    public void StopSe()
    {
        seAudioSource.Stop();
        seAudioSource.clip = null;
    }
 

}
