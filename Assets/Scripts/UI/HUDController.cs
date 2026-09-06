using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HUDController : MonoBehaviour
{
    public bool isOpen = true;

    public TextMeshProUGUI timeLabel;
    public TextMeshProUGUI scoreLabel;
    public TextMeshProUGUI multLabel;
    public Animator clockSpriteAnimator;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
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

    public void ToggleOpen(bool toOpen)
    {
        isOpen = toOpen;
        this.gameObject.SetActive(isOpen);
    }
}
