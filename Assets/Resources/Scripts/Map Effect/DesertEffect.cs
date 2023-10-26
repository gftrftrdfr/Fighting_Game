using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DesertEffect : MapEffect
{
    public GameObject windEffect;
    float cooldown = 0;
    bool check = false;
    // Start is called before the first frame update
    public override void Effect()
    {
        foreach (GameObject player in players)
        {
            player.GetComponent<CharacterController>().attackSpeed /= 1.5f;
            player.GetComponent<CharacterController>().walkSpeed /= 2f;
            player.GetComponent<CharacterController>().staminaLoss = 1.5f;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (check)
        {
            if(Random.Range(0,1)>0.5f)
            {
                GameObject effect = Instantiate(windEffect, new Vector3(Random.Range(-30, 30), Random.Range(-3, 2), 0), Quaternion.Euler(-90, -180, 0));
                effect.transform.localScale *= Random.Range(1, 3);
                Destroy(effect, 10f);
                check = false;
            }
            else
            {
                GameObject effect = Instantiate(windEffect, new Vector3(Random.Range(-30, 30), Random.Range(-3, 2), 0), Quaternion.Euler(-90, 0, 0));
                effect.transform.localScale *= Random.Range(1, 3);
                Destroy(effect, 10f);
                check = false;
            }
            
        }
    }

    private void FixedUpdate()
    {
        if (!check)
        {
            cooldown -= Time.fixedDeltaTime;
            if (cooldown <= 0)
            {
                check = true;
                cooldown = Random.Range(10, 20);
            }
        }
    }
  
}
