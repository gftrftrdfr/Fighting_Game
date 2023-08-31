using System;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    public PlayerObjectDB playerObjectDB;

    [Header("Image")]
    public Button powerP1;
    public Button powerP2;
    public Image skill1P1;
    public Image skill2P1;
    public Image skill1P2;
    public Image skill2P2;

    private int selectedCharacterOption1 = 0;
    private int selectedCharacterOption2 = 0;
    private int selectedSkinOption1 = 0;
    private int selectedSkinOption2 = 0;
    private string champName1;
    private string champName2;
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
        UnityEngine.Object[] sprites;
        gameObject = CreateCharacter(selectedCharacterOption1, selectedSkinOption1, 1);
        gameObject.tag = "Player 1";
        gameObject.layer = 9;
        Instantiate(gameObject, new Vector3(-10, -4, 0f), Quaternion.identity);
        switch(champName1)
        {
            case "Fallen_Angels":
            case "Fallen_Angels 1":
            case "Fallen_Angels 2":              
                sprites = Resources.LoadAll("Sprites/Characters/Fallen Angels/50-Demon-Skill-Game-Icons2");
                powerP1.image.sprite = sprites[7] as Sprite;
                skill1P1.sprite = sprites[35] as Sprite;
                skill2P1.sprite = sprites[21] as Sprite;
                break;

            case "Stone Giant":
            case "Stone Giant 1":
            case "Stone Giant 2":
                sprites = Resources.LoadAll("Sprites/Characters/Stone Giant/100-RPG-Skill-Icons3");
                powerP1.image.sprite = sprites[34] as Sprite;
                skill2P1.sprite = sprites[22] as Sprite;
                sprites = Resources.LoadAll("Sprites/Characters/Stone Giant/100-RPG-Skill-Icons2");
                skill1P1.sprite = sprites[4] as Sprite;
                break;

            case "Minotaur":
            case "Minotaur 1":
            case "Minotaur 2":
                sprites = Resources.LoadAll("Sprites/Characters/Minotaur/RPG-Demon-Skill-Icons2");
                powerP1.image.sprite = sprites[3] as Sprite;
                sprites = Resources.LoadAll("Sprites/Characters/Minotaur/100-Skill-Icons-Pack-for-RPG3");
                skill1P1.sprite = sprites[5] as Sprite;               
                skill2P1.sprite = sprites[17] as Sprite;
                break;

            case "Satyr":
            case "Satyr 1":
            case "Satyr 2":
                sprites = Resources.LoadAll("Sprites/Characters/Satyr/RPG-Bard-Skill-Icons2");
                powerP1.image.sprite = sprites[35] as Sprite;
                skill1P1.sprite = sprites[46] as Sprite;
                skill2P1.sprite = sprites[6] as Sprite;
                break;

            case "Wraith":
            case "Wraith 1":
            case "Wraith 2":
                powerP1.image.sprite = Resources.Load<Sprite>("Sprites/Characters/Wraith/39");
                skill1P1.sprite = Resources.Load<Sprite>("Sprites/Characters/Wraith/11");
                skill2P1.sprite = Resources.Load<Sprite>("Sprites/Characters/Wraith/43");
                break;

            case "Reaper":
            case "Reaper 1":
            case "Reaper 2":
                sprites = Resources.LoadAll("Sprites/Characters/Reaper/RPG-Blood-Mage-Skill-Icons2");
                powerP1.image.sprite = sprites[1] as Sprite;
                skill1P1.sprite = sprites[12] as Sprite;
                skill2P1.sprite = sprites[27] as Sprite;
                break;
        }
        gameObject = CreateCharacter(selectedCharacterOption2, selectedSkinOption2, 2);
        gameObject.tag = "Player 2";
        gameObject.layer = 10  ;
        Instantiate(gameObject, new Vector3(10, -4,-0.5f), Quaternion.identity);
        switch (champName2)
        {
            case "Fallen_Angels":
            case "Fallen_Angels 1":
            case "Fallen_Angels 2":
                sprites = Resources.LoadAll("Sprites/Characters/Fallen Angels/50-Demon-Skill-Game-Icons2");
                powerP2.image.sprite = sprites[7] as Sprite;
                skill1P2.sprite = sprites[35] as Sprite;
                skill2P2.sprite = sprites[21] as Sprite;
                break;

            case "Stone Giant":
            case "Stone Giant 1":
            case "Stone Giant 2":
                sprites = Resources.LoadAll("Sprites/Characters/Stone Giant/100-RPG-Skill-Icons3");
                powerP2.image.sprite = sprites[34] as Sprite;
                skill2P2.sprite = sprites[22] as Sprite;
                sprites = Resources.LoadAll("Sprites/Characters/Stone Giant/100-RPG-Skill-Icons2");
                skill1P2.sprite = sprites[4] as Sprite;
                break;

            case "Minotaur":
            case "Minotaur 1":
            case "Minotaur 2":
                sprites = Resources.LoadAll("Sprites/Characters/Minotaur/RPG-Demon-Skill-Icons2");
                powerP2.image.sprite = sprites[3] as Sprite;
                sprites = Resources.LoadAll("Sprites/Characters/Minotaur/100-Skill-Icons-Pack-for-RPG3");
                skill1P2.sprite = sprites[5] as Sprite;
                skill2P2.sprite = sprites[17] as Sprite;
                break;

            case "Satyr":
            case "Satyr 1":
            case "Satyr 2":
                sprites = Resources.LoadAll("Sprites/Characters/Satyr/RPG-Bard-Skill-Icons2");
                powerP2.image.sprite = sprites[35] as Sprite;
                skill1P2.sprite = sprites[46] as Sprite;
                skill2P2.sprite = sprites[6] as Sprite;
                break;

            case "Wraith":
            case "Wraith 1":
            case "Wraith 2":
                sprites = Resources.LoadAll("Sprites/Characters/Satyr/RPG-Bard-Skill-Icons2");
                powerP2.image.sprite = Resources.Load<Sprite>("Sprites/Characters/Wraith/39");
                skill1P2.sprite = Resources.Load<Sprite>("Sprites/Characters/Wraith/11");
                skill2P2.sprite = Resources.Load<Sprite>("Sprites/Characters/Wraith/43");
                break;

            case "Reaper":
            case "Reaper 1":
            case "Reaper 2":
                sprites = Resources.LoadAll("Sprites/Characters/Reaper/RPG-Blood-Mage-Skill-Icons2");
                powerP2.image.sprite = sprites[1] as Sprite;
                skill1P2.sprite = sprites[12] as Sprite;
                skill2P2.sprite = sprites[27] as Sprite;
                break;
        }

    }
    private GameObject CreateCharacter(int selectedCharacterOption, int selectedSkinOption, int playerNumber)
    {
        MyPlayerObject myPlayerObject = playerObjectDB.GetPlayerObject(selectedCharacterOption);
        GameObject gameObject = myPlayerObject.GetGameObject(selectedSkinOption);
        if(playerNumber == 1)
        {
            champName1 = myPlayerObject.ChampName(selectedSkinOption);
        }
        else
        {
            champName2 = myPlayerObject.ChampName(selectedSkinOption);
        }
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
