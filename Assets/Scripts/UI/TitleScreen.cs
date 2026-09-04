using UnityEngine;

public class TitleScreen : MonoBehaviour
{
    public AudioController audioController;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (audioController == null) Debug.Log("TitleScreen not given an AudioController");
        else audioController.PlayMusic(Music.TITLE_SCREEN);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
