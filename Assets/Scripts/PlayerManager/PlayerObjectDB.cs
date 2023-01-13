using UnityEngine;
[CreateAssetMenu]
public class PlayerObjectDB : ScriptableObject
{
    public MyPlayerObject[] player1;
    public MyPlayerObject[] player2;
    public int PlayerObjectCount
    {
        get
        {
            return player1.Length;
        }
    }
    public MyPlayerObject GetPlayerObject(int index, int player)
    {
        if (player == 1)
        {
            return player1[index];
        }
        else
        {
            return player2[index];
        }
    }
}
