using UnityEngine;

[RequireComponent(typeof(Animator))]
public class RainbowText : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        EventBus.Instance.OnRainbowTimeActivated += ActivateRainbowTime;
        EventBus.Instance.OnLevelStart += DeactivateRainbowTime;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void ActivateRainbowTime()
    {
        GetComponent<Animator>().SetBool("isRainbow", true);
    }

    void DeactivateRainbowTime()
    {
        GetComponent<Animator>().SetBool("isRainbow", false);
    }
}
