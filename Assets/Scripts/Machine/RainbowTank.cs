using UnityEngine;

[RequireComponent(typeof(Animator))]
class RainbowTank : MonoBehaviour
{
    private Color _baseColor;
    private void Start()
    {
        EventBus.Instance.OnRainbowTimeActivated += ActivateRainbowTime;
        EventBus.Instance.OnLevelStart += DeactivateRainbowTime;
        _baseColor = GetComponent<SpriteRenderer>().color;
    }

    private void Update()
    {

    }

    private void OnDestroy()
    {
        EventBus.Instance.OnRainbowTimeActivated -= ActivateRainbowTime;
        EventBus.Instance.OnLevelStart -= DeactivateRainbowTime;
    }

    void ActivateRainbowTime()
    {
        GetComponent<Animator>().SetBool("isRainbow", true);
        GetComponent<SpriteRenderer>().color = Color.white;
    }

    void DeactivateRainbowTime()
    {
        GetComponent<Animator>().SetBool("isRainbow", false);
        GetComponent<SpriteRenderer>().color = _baseColor;
    }

}