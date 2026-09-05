using UnityEngine;

public class TitleScreen : MonoBehaviour
{
    private AudioController audioController;
    public GameObject audioControllerPrototype;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GameObject audioControllerObj = Instantiate(audioControllerPrototype);
        if (!audioControllerObj.TryGetComponent(out audioController)) Debug.Log("AudioController prototype does not have an AudioController component");
        else audioController.PlayMusic(Music.TITLE_SCREEN);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
