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

        if (musicSource == null) Debug.Log("AudioController not provided an AudioSource for music");

        Game.Instance.audioController = this;
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
        if (song >= 0 && (int)song < musicClips.Length)
        {
            musicSource.clip = musicClips[(int)song];
            musicSource.volume = musicVolume;
            musicSource.Play();
        }
    }
}