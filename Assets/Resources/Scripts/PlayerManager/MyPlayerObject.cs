using System;
using UnityEngine;
[System.Serializable]
public class MyPlayerObject
{
    // [Character]
    public GameObject[] gameObjects;
    public int SkinCount
    {
        get
        {
            return gameObjects.Length;
        }
    }
    public GameObject GetGameObject(int index)
    {
        return gameObjects[index];
    }

    public string ChampName(int index)
    {
        return gameObjects[index].name.ToString();
    }
}
