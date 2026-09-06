using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioController : MonoBehaviour
{
    [SerializeField] private AudioMixer audioMixer;

    [SerializeField] private AudioSource sfxSource;
    [SerializeField] private AudioClip[] sfxClips;
    [SerializeField] private float sfxVolume;

    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioClip[] musicClips;
    [SerializeField] private float musicVolume;

    [SerializeField] private Slider volumeSlider;


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

    public void PlaySFX(SFX effect, float volumeMod)
    {
        if (effect >= 0 && (int)effect < sfxClips.Length)
        {
            sfxSource.PlayOneShot(sfxClips[(int)effect], sfxVolume * volumeMod);
        }
    }

    public void PlaySFX(SFX effect, AudioSource source)
    {
        if (effect >= 0 && (int)effect < sfxClips.Length)
        {
            source.clip = sfxClips[(int)effect];
            source.volume = sfxVolume;
            source.Play();
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

    public void ChangeVolume()
    {
        AudioListener.volume = volumeSlider.value;
    }
}