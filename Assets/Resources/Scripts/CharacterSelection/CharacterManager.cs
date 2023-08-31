using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
public class CharacterManager : MonoBehaviour
{
    public CharacterDatabase characterDB;
    public TextMeshProUGUI nameText;
    public GameObject artworkSprite;
    public int selectedCharacterOption = 0;
    public int selectedSkinOption = 0;
    public int playerNumber;

    void Start()
    {
        PlayerPrefs.SetInt("selectedCharacterOption" + playerNumber.ToString(), 0);
        PlayerPrefs.SetInt("selectedSkinOption" + playerNumber.ToString(), 0);

        Load();

        UpdateCharacter(selectedCharacterOption, selectedSkinOption);
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
    public void CharacterOption(int number)
    {
        selectedCharacterOption = number;
        selectedSkinOption = 0;
        UpdateCharacter(selectedCharacterOption, 0);
        Save();
    }

    private void UpdateCharacter(int selectedCharacterOption, int selectedSkinOption)
    {
        Destroy(artworkSprite);
        Character character = characterDB.GetCharacter(selectedCharacterOption);
        artworkSprite = Instantiate(character.characterSprite[selectedSkinOption], this.transform);
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
    public void SetPlayerNumber(int number)
    {
        playerNumber = number;
    }
}
