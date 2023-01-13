using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
public class CharacterManager : MonoBehaviour
{
    public CharacterDatabase characterDB;
    public TextMeshProUGUI nameText;
    public Image artworkSprite;
    private int selectedOption = 0;
    public int playerNumber;

    void Start()
    {
        if (!PlayerPrefs.HasKey("selectedOption" + playerNumber.ToString()))
        {
            selectedOption = 0;
        }
        else
        {
            Load();
        }
        UpdateCharacter(selectedOption);
    }

    public void NextOption()
    {
        selectedOption++;
        if( selectedOption >= characterDB.CharacterCount)
        {
            selectedOption = 0;
        }

        UpdateCharacter(selectedOption);
        Save();
        Debug.Log(selectedOption);

    }

    public void BackOption()
    {
        selectedOption--;
        if(selectedOption < 0)
        {
            selectedOption = characterDB.CharacterCount - 1;
        }
        UpdateCharacter(selectedOption);
        Save();
    }

    private void UpdateCharacter(int selectedOption)
    {
        Character character = characterDB.GetCharacter(selectedOption);
        artworkSprite.sprite = character.characterSprite;
        nameText.text = character.characterName;
    }

    private void Load()
    {
        selectedOption = PlayerPrefs.GetInt("selectedOption" + playerNumber.ToString());
    }

    private void Save()
    {
        PlayerPrefs.SetInt("selectedOption" + playerNumber.ToString(), selectedOption);
    }
    public void ChangeScene()
    {
        string scenceName = "Desert";
        if (PlayerPrefs.HasKey("sceneName"))
        {
            scenceName = PlayerPrefs.GetString("sceneName");
        }
        UpdateCharacter(selectedOption);
        SceneManager.LoadScene(scenceName);
    }
}
