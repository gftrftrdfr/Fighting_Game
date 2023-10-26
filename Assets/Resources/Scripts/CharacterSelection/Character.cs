using UnityEngine;

[System.Serializable]
public class Character
{
    public string characterName;
    public GameObject[] characterSprite;
    public Sprite[] UI_Sprite;
    public Sprite emblem;
    public int SkinCount
    {
        get
        {
            return characterSprite.Length;
        }
    }

    public Sprite GetUI(int index)
    {
        return UI_Sprite[index];
    }
}
