using System.Collections;
using UnityEngine;

public class SceneManager : MonoBehaviour
{

    public void ChangeScene(string sceneName)
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
    }

    public void QuitGame(){
        Application.Quit();
    }
}
