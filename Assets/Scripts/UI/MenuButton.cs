using UnityEngine;

public class MenuButton : MonoBehaviour
{
    public void PlaySound()
    {
        Game.Instance.audioController.PlaySFX(SFX.MENU_TICK, 2.0f);
    }
}