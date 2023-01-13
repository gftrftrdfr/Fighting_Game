using UnityEngine;
using UnityEngine.UI;
public class Player : MonoBehaviour
{
    public CharacterDatabase characterDB;
    public Image artworkSprite;
    private int selectedOption = 0;
    public int playerNumber;
    // Start is called before the first frame update
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
        Debug.Log(selectedOption);
        UpdateCharacter(selectedOption);
    }
    private void UpdateCharacter(int selectedOption)
    {
        Character character = characterDB.GetCharacter(selectedOption);
        artworkSprite.sprite = character.characterSprite;
    }

    private void Load()
    {
        selectedOption = PlayerPrefs.GetInt("selectedOption" + playerNumber.ToString());
    }

}
