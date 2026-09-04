using System;
using UnityEngine;
using UnityEngine.Audio;

public class AudioController : MonoBehaviour
{
    [SerializeField] private AudioMixer audioMixer;

    [SerializeField] private AudioSource sfxSource;
    [SerializeField] private AudioClip[] sfxClips;
    [SerializeField] private float sfxVolume;

    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioClip[] musicClips;
    [SerializeField] private float musicVolume;


    void Start()
    {
        if (sfxSource == null) Debug.Log("AudioController not provided an AudioSource for SFX");
        else EventBus.Instance.OnPlaySFX += PlaySFX; 

        if (musicSource == null) Debug.Log("AudioController not provided an AudioSource for music");
        else EventBus.Instance.OnPlayMusic += PlayMusic;
    }

    public void PlaySFX(SFX effect)
    {
        if (effect >= 0 && (int)effect < sfxClips.Length)
        {
            sfxSource.PlayOneShot(sfxClips[(int)effect], sfxVolume);
        }
    }

    public void PlayMusic(Music song)
    {
        Debug.Log("Play song");
        if (song >= 0 && (int)song < musicClips.Length)
        {
            Debug.Log("Play song");
            musicSource.clip = musicClips[(int)song];
            musicSource.volume = musicVolume;
            musicSource.Play();
        }
    }
}