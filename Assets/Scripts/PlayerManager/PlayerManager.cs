using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public PlayerObjectDB playerObjectDB;
    private int selectedCharacterOption1 = 0;
    private int selectedCharacterOption2 = 0;
    private int selectedSkinOption1 = 0;
    private int selectedSkinOption2 = 0;
    //public int playerNumber;
    // Start is called before the first frame update
    void Awake()
    {
        //if (!PlayerPrefs.HasKey("selectedCharacterOption" + playerNumber.ToString())
        //    || !PlayerPrefs.HasKey("selectedSkinOption" + playerNumber.ToString()))
        if (!PlayerPrefs.HasKey("selectedCharacterOption1")
        || !PlayerPrefs.HasKey("selectedCharacterOption2")
        || !PlayerPrefs.HasKey("selectedSkinOption1")
        || !PlayerPrefs.HasKey("selectedSkinOption2"))
        {
            selectedCharacterOption1 = 0;
            selectedCharacterOption2 = 0;
            selectedSkinOption1 = 0;
            selectedSkinOption2 = 0;
        }
        else
        {
            Load();
        }
        GameObject gameObject;
        gameObject = CreateCharacter(selectedCharacterOption1, selectedSkinOption1, 1);
        Instantiate(gameObject, new Vector2(-10, -4), Quaternion.identity);
        gameObject = CreateCharacter(selectedCharacterOption2, selectedSkinOption2, 2);
        Instantiate(gameObject, new Vector2(10, -4), Quaternion.identity);

    }
    private GameObject CreateCharacter(int selectedCharacterOption, int selectedSkinOption, int playerNumber)
    {
        MyPlayerObject myPlayerObject = playerObjectDB.GetPlayerObject(selectedCharacterOption, playerNumber);
        GameObject gameObject = myPlayerObject.GetGameObject(selectedSkinOption);
        return gameObject;
    }

    private void Load()
    {
        selectedCharacterOption1 = PlayerPrefs.GetInt("selectedCharacterOption1");
        selectedCharacterOption2 = PlayerPrefs.GetInt("selectedCharacterOption2");
        selectedSkinOption1 = PlayerPrefs.GetInt("selectedSkinOption1");
        selectedSkinOption2 = PlayerPrefs.GetInt("selectedSkinOption2");
    }

}
