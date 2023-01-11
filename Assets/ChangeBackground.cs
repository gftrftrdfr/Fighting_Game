using UnityEngine;
using UnityEngine.UI;

public class ChangeBackground : MonoBehaviour
{
    public void ChangeImageBackground(Sprite newBackground)
    {
        Image oldBackground = GetComponent<Image>();
        oldBackground.sprite = newBackground;
    }
}
