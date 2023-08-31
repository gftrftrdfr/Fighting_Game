using UnityEngine;
[CreateAssetMenu]
public class PlayerObjectDB : ScriptableObject
{
    public MyPlayerObject[] player;
    public int PlayerObjectCount
    {
        get
        {
            return player.Length;
        }
    }
    public MyPlayerObject GetPlayerObject(int index)
    {
        return player[index];
    }
}
