using System;
using UnityEngine;
using UnityEngine.Audio;

[RequireComponent(typeof(AudioSource))]
public class AudioController : MonoBehaviour
{
    [SerializeField] private AudioMixer audioMixer;

    [SerializeField] private AudioSource sfxSource;
    [SerializeField] private AudioClip[] sfxClips;
    [SerializeField] private float sfxVolume;


    void Start()
    {

        sfxSource = GetComponent<AudioSource>();

        EventBus.Instance.OnPlaySFX += PlaySFX;
    }

    public void PlaySFX(SFX effect)
    {
        if (effect >= 0 && (int)effect < sfxClips.Length)
        {
            sfxSource.PlayOneShot(sfxClips[(int)effect], sfxVolume);
        }
    }
}