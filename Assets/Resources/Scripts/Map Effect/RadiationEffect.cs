using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadiationEffect : MapEffect
{
    float cooldown = 0;
    bool check = true;

    // Update is called once per frame
    void Update()
    {
        if(check)
        {
            foreach (GameObject player in players)
            {
                player.GetComponent<CharacterController>().currentHealth -= 10;       
            }
            check = false;
        }
    }

    private void FixedUpdate()
    {
        if (!check)
        {
            cooldown += Time.fixedDeltaTime;
            if (cooldown > 1)
            {
                check = true;
                cooldown = 0;
            }
        }
    }
}
