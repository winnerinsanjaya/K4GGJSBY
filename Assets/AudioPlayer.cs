using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;

public class AudioPlayer : MonoBehaviour
{
    public static AudioPlayer instance;

    [SerializeField]
    private AudioSource sfxAudioSource;
    [SerializeField]
    private AudioSource sfx2AudioSource;

    [SerializeField]
    private AudioSource bgmAudioSource;


    [SerializeField]
    private List<AudioClip> sfxClip;
    [SerializeField]
    private List<AudioClip> sfx2Clip;

    [SerializeField]
    private List<AudioClip> bgmClip;


    private void Awake()
    {
        if (instance != null)
        {
            Destroy(this.gameObject);
            return;

        }


        instance = this;
        DontDestroyOnLoad(this.gameObject);

    }


    public void PlayBGM(int index)
    {
        if (bgmAudioSource.clip != null)
        {
            bgmAudioSource.Stop();
        }
        bgmAudioSource.clip = bgmClip[index];
        bgmAudioSource.Play();
    }


    public void PlaySFX(int index)
    {
        if (sfxAudioSource.clip != null)
        {
            sfxAudioSource.Stop();
        }
        sfxAudioSource.clip = sfxClip[index];
        sfxAudioSource.Play();
    }
    
    public void PlaySFX2(int index)
    {
        if (sfx2AudioSource.clip != null)
        {
            sfx2AudioSource.Stop();
        }
        sfx2AudioSource.clip = sfx2Clip[index];
        sfx2AudioSource.Play();
    }


}