using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HUDController : MonoBehaviour
{
    public bool isOpen = true;

    public TextMeshProUGUI timeLabel;
    public TextMeshProUGUI scoreLabel;
    public TextMeshProUGUI multLabel;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if(gameObject.name == "HUD"){ScoreManager.Instance.HUDController = this;}

        if(!isOpen){isOpen = true; ToggleOpen();}
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ToggleOpen()
    {
        isOpen = !isOpen;

        this.gameObject.SetActive(isOpen);
    }
}
