using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class Button : MonoBehaviour
{
    public MachineButton machineButton;
    public Sprite spriteOnPressed;
    private Sprite _baseSprite;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _baseSprite = GetComponent<SpriteRenderer>().sprite; 
        if (_baseSprite == null)
        {
            Debug.Log("Button not given a sprite");
        }
        if (spriteOnPressed == null)
        {
            Debug.Log("Button not given a sprite on press");
        }

        machineButton.OnChange += ToggleSprite;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void ToggleSprite(bool status)
    {
        if (status && spriteOnPressed)
        {
            GetComponent<SpriteRenderer>().sprite = spriteOnPressed;
        }
        else if (_baseSprite)
        {
            GetComponent<SpriteRenderer>().sprite = _baseSprite;
        }
    }
}
