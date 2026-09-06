using UnityEngine;
using UnityEngine.UI;

public class SettingsManager : MonoBehaviour
{
    [SerializeField] Slider volumeSlider;

    public void ToggleFullscreen()
    {
        Screen.fullScreen = !Screen.fullScreen;
    }

    public void ToggleAudio()
    {
        AudioListener.volume = AudioListener.volume == 0f ? 1f : 0f;
        /*if (isMuted) {
            masterBus.setVolume(1);
        } else {
            masterBus.setVolume(0);
        }

        isMuted = !isMuted;*/
    }

    public void ChangeVolume(){
        AudioListener.volume = volumeSlider.value;
    }
}
