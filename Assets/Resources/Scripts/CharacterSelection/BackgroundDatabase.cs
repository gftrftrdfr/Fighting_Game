using UnityEngine;
[CreateAssetMenu]

public class BackgroundDatabase : ScriptableObject
{
    public BackgroundObject[] backgroundObjects;
    public int CharacterCount
    {
        get
        {
            return backgroundObjects.Length;
        }
    }
    public BackgroundObject GetBackgroundByIndex(int index)
    {
        return backgroundObjects[index];
    }
    public BackgroundObject GetBackgroundByName(string bgName)
    {
        for(int i = 0; i < CharacterCount; i++)
        {
            if (backgroundObjects[i].backgroundName == bgName)
            {
                return backgroundObjects[i];
            }
        }
        return null;
    }
}
