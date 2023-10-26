using System;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    public PlayerObjectDB playerObjectDB;
    public CharacterDatabase characterDatabase;

    [Header("Image")]
    public Button powerP1;
    public Button powerP2;
    public Image skill1P1;
    public Image skill2P1;
    public Image skill1P2;
    public Image skill2P2;
    public Image AvtP1;
    public Image AvtP2;
    public SpriteRenderer emblemP1_1;
    public SpriteRenderer emblemP1_2;
    public SpriteRenderer emblemP2_1;
    public SpriteRenderer emblemP2_2;

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
        if(PlayerPrefs.GetString("PlayMode") == "practice")
        {
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

            AvtP1.sprite = CreateAvatar(selectedCharacterOption1, selectedSkinOption1);

            GameObject gameObject;
            gameObject = CreateCharacter(selectedCharacterOption1, selectedSkinOption1, 1);
            gameObject.tag = "Player 1";
            gameObject.layer = 9;
            Instantiate(gameObject, new Vector3(-10, -4, 0f), Quaternion.identity);
            powerP1.image.sprite = Resources.Load<Sprite>("Sprites/Characters/" + champName1 + "/3");
            skill1P1.sprite = Resources.Load<Sprite>("Sprites/Characters/" + champName1 + "/1");
            skill2P1.sprite = Resources.Load<Sprite>("Sprites/Characters/" + champName1 + "/2");

        }
        else
        {
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

            AvtP1.sprite = CreateAvatar(selectedCharacterOption1, selectedSkinOption1);
            AvtP2.sprite = CreateAvatar(selectedCharacterOption2, selectedSkinOption2);

            Color colorEmblemP1 = new Color32(232, 106, 23, 255);
            emblemP1_1.color = colorEmblemP1;
            emblemP1_1.sprite = CreateEmblem(selectedCharacterOption1);
            emblemP1_2.color = colorEmblemP1;
            emblemP1_2.sprite = CreateEmblem(selectedCharacterOption1);

            Color colorEmblemP2 = new Color32(65, 159, 221, 255);
            emblemP2_1.color = colorEmblemP2;
            emblemP2_1.sprite = CreateEmblem(selectedCharacterOption2);
            emblemP2_2.color = colorEmblemP2;
            emblemP2_2.sprite = CreateEmblem(selectedCharacterOption2);


            GameObject gameObject;
            gameObject = CreateCharacter(selectedCharacterOption1, selectedSkinOption1, 1);
            gameObject.tag = "Player 1";
            gameObject.layer = 9;
            Instantiate(gameObject, new Vector3(-10, -4, 0f), Quaternion.identity);
            powerP1.image.sprite = Resources.Load<Sprite>("Sprites/Characters/" + champName1 + "/3");
            skill1P1.sprite = Resources.Load<Sprite>("Sprites/Characters/" + champName1 + "/1");
            skill2P1.sprite = Resources.Load<Sprite>("Sprites/Characters/" + champName1 + "/2");

            gameObject = CreateCharacter(selectedCharacterOption2, selectedSkinOption2, 2);
            gameObject.tag = "Player 2";
            gameObject.layer = 10;
            Instantiate(gameObject, new Vector3(10, -4, -0.5f), Quaternion.identity);
            powerP2.image.sprite = Resources.Load<Sprite>("Sprites/Characters/" + champName2 + "/3");
            skill1P2.sprite = Resources.Load<Sprite>("Sprites/Characters/" + champName2 + "/1");
            skill2P2.sprite = Resources.Load<Sprite>("Sprites/Characters/" + champName2 + "/2");
        }

    }
    private GameObject CreateCharacter(int selectedCharacterOption, int selectedSkinOption, int playerNumber)
    {
        MyPlayerObject myPlayerObject = playerObjectDB.GetPlayerObject(selectedCharacterOption);
        GameObject gameObject = myPlayerObject.GetGameObject(selectedSkinOption);
        if(playerNumber == 1)
        {
            champName1 = PlayerPrefs.GetString("characterName1");
        }
        else if (PlayerPrefs.GetString("PlayMode") != "practice" && playerNumber == 2)
        {
            champName2 = PlayerPrefs.GetString("characterName2");
        }
        return gameObject;
    }

    private Sprite CreateAvatar(int selectedCharacterOption, int selectedSkinOption)
    {
        Character character = characterDatabase.GetCharacter(selectedCharacterOption);
        return character.GetUI(selectedSkinOption);      
    }

    private Sprite CreateEmblem(int selectedCharacterOption)
    {
        Character character = characterDatabase.GetCharacter(selectedCharacterOption);
        return character.emblem;
    }

    private void Load()
    {
        selectedCharacterOption1 = PlayerPrefs.GetInt("selectedCharacterOption1");       
        selectedSkinOption1 = PlayerPrefs.GetInt("selectedSkinOption1");
        
        if (PlayerPrefs.GetString("PlayMode") != "practice")
        {
            selectedCharacterOption2 = PlayerPrefs.GetInt("selectedCharacterOption2");
            selectedSkinOption2 = PlayerPrefs.GetInt("selectedSkinOption2");
        }
    }
}
