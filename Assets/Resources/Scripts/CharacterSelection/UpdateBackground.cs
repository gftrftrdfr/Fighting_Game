using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UpdateBackground : MonoBehaviour
{
    private string bgName = "Desert";
    public  BackgroundDatabase backgroundDB;
    BackgroundObject bgObject;
    void Start()
    {
        if (SceneManager.GetActiveScene().name == "Menu")
        {
            int tmp = Random.Range(0, backgroundDB.CharacterCount);
            bgObject = backgroundDB.GetBackgroundByIndex(tmp);
            Debug.Log(bgObject.backgroundName);
            PlayerPrefs.SetString("sceneName", bgObject.backgroundName);
        }
        else
        {
            if (PlayerPrefs.HasKey("sceneName"))
            {
                bgName = PlayerPrefs.GetString("sceneName");
                bgObject = backgroundDB.GetBackgroundByName(bgName);
            }
        }        
        GameObject bg = Instantiate(bgObject.backgroundObject, transform);
    }
}
