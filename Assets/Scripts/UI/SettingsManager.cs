using UnityEngine;

public class SettingsManager : MonoBehaviour
{
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
}
