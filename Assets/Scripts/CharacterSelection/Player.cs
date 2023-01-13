using UnityEngine;
using UnityEngine.UI;
public class Player : MonoBehaviour
{
    public CharacterDatabase characterDB;
    public Image artworkSprite;
    private int selectedCharacterOption = 0;
    private int selectedSkinOption = 0;
    public int playerNumber;
    // Start is called before the first frame update
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
        UpdateCharacter(selectedCharacterOption, selectedSkinOption);
    }
    private void UpdateCharacter(int selectedCharacterOption, int selectedSkinOption)
    {
        Character character = characterDB.GetCharacter(selectedCharacterOption);
        artworkSprite.sprite = character.characterSprite[selectedSkinOption];
    }

    private void Load()
    {
        selectedCharacterOption = PlayerPrefs.GetInt("selectedCharacterOption" + playerNumber.ToString());
        selectedSkinOption = PlayerPrefs.GetInt("selectedSkinOption" + playerNumber.ToString());
    }

}
