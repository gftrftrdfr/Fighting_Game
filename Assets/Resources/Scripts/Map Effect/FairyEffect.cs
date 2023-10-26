using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FairyEffect : MapEffect
{
    // Start is called before the first frame update
    public override void Effect()
    { 
        foreach (GameObject player in players)
        {
            player.GetComponent<CharacterController>().dameSkill = 1.5f;
        }
    }
}
