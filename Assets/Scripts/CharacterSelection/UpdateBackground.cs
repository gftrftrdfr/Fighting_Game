using UnityEngine;
using UnityEngine.UI;

public class UpdateBackground : MonoBehaviour
{
    private string bgName = "Desert";
    public  BackgroundDatabase backgroundDB;
    void Start()
    {
        if (PlayerPrefs.HasKey("sceneName"))
        {
            bgName = PlayerPrefs.GetString("sceneName");
        }
        BackgroundObject bgObject = backgroundDB.GetBackgroundByName(bgName);
        this.GetComponent<Image>().sprite = bgObject.backgroundSprite;
    }
}
