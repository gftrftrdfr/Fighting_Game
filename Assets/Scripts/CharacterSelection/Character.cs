using UnityEngine;

[System.Serializable]
public class Character
{
    public string characterName;
    public Sprite[] characterSprite;
    public int SkinCount
    {
        get
        {
            return characterSprite.Length;
        }
    }
}
