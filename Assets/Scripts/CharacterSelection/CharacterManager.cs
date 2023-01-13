using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
public class CharacterManager : MonoBehaviour
{
    public CharacterDatabase characterDB;
    public TextMeshProUGUI nameText;
    public Image artworkSprite;
    private int selectedCharacterOption = 0;
    private int selectedSkinOption = 0;
    public int playerNumber;

    void Start()
    {
        if (!PlayerPrefs.HasKey("selectedCharacterOption" + playerNumber.ToString())
            || !PlayerPrefs.HasKey("selectedSkinOption" + playerNumber.ToString()))
        {
            selectedCharacterOption = 0;
            selectedSkinOption = 0;
        }
        else
        {
            Load();
        }
        UpdateCharacter(selectedCharacterOption,selectedSkinOption);
    }

    public void NextSkinOption()
    {
        selectedSkinOption++;
        Character character = characterDB.GetCharacter(selectedCharacterOption);
        if(selectedSkinOption >= character.SkinCount)
        {
            selectedSkinOption = 0;
        }

        UpdateCharacter(selectedCharacterOption,selectedSkinOption);
        Save();

    }

    public void BackSkinOption()
    {
        selectedSkinOption--;
        Character character = characterDB.GetCharacter(selectedCharacterOption);
        if (selectedSkinOption < 0)
        {
            selectedSkinOption = character.SkinCount - 1;
        }
        UpdateCharacter(selectedCharacterOption,selectedSkinOption);
        Save();
    }
    public void NextCharacterOption()
    {
        selectedCharacterOption++;
        if( selectedCharacterOption >= characterDB.CharacterCount)
        {
            selectedCharacterOption = 0;
        }
        selectedSkinOption = 0;
        UpdateCharacter(selectedCharacterOption, 0);
        Save();
    }

    public void BackCharacterOption()
    {
        selectedCharacterOption--;
        if(selectedCharacterOption < 0)
        {
            selectedCharacterOption = characterDB.CharacterCount - 1;
        }
        selectedSkinOption = 0;
        UpdateCharacter(selectedCharacterOption, 0);
        Save();
    }

    private void UpdateCharacter(int selectedCharacterOption, int selectedSkinOption)
    {
        Character character = characterDB.GetCharacter(selectedCharacterOption);
        Debug.Log(selectedSkinOption);
        artworkSprite.sprite = character.characterSprite[selectedSkinOption];
        nameText.text = character.characterName;
    }

    private void Load()
    {
        selectedCharacterOption = PlayerPrefs.GetInt("selectedCharacterOption" + playerNumber.ToString());
        selectedSkinOption = PlayerPrefs.GetInt("selectedSkinOption" + playerNumber.ToString());
    }

    private void Save()
    {
        PlayerPrefs.SetInt("selectedCharacterOption" + playerNumber.ToString(), selectedCharacterOption);
        PlayerPrefs.SetInt("selectedSkinOption" + playerNumber.ToString(), selectedSkinOption);
    }
    public void ChangeScene()
    {
        string scenceName = "Desert";
        if (PlayerPrefs.HasKey("sceneName"))
        {
            scenceName = PlayerPrefs.GetString("sceneName");
        }
        UpdateCharacter(selectedCharacterOption, selectedSkinOption);
        SceneManager.LoadScene(scenceName);
        //SceneManager.LoadScene("TestScene");
    }
}
