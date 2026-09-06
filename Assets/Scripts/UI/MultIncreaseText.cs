using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
[RequireComponent(typeof(Animator))]
public class MultIncreaseText : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        EventBus.Instance.OnMultEarned += ChangeText;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnDestroy()
    {
        EventBus.Instance.OnMultEarned -= ChangeText;
    }

    private void ChangeText(float multIncrease)
    {
        GetComponent<TextMeshProUGUI>().text = string.Format("+{0:0.0#}", multIncrease);
        GetComponent<Animator>().Play("multAddedFade");
    }
}
