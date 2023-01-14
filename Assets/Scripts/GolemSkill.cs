using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class GolemSkill : MonoBehaviour
{

    GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        player = this.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (player.tag == "Player 1")
        {
            if (Input.GetButtonDown("Cast 1") && player.GetComponent<CharacterMovement>().currentPower == 100)
            {
                StartCoroutine(Skill());
                player.GetComponent<CharacterMovement>().currentPower = 0;
            }
            else if (Input.GetButtonDown("Cast 1") && player.GetComponent<CharacterMovement>().currentPower < 100)
            {
                player.GetComponent<CharacterMovement>().Show("Not enough stamina");
            }
        }
        else if (player.tag == "Player 2")
        {
            if (Input.GetButtonDown("Cast 2") && player.GetComponent<CharacterMovement>().currentPower == 100)
            {
                StartCoroutine(Skill());
                player.GetComponent<CharacterMovement>().currentPower = 0;
            }
            else if (Input.GetButtonDown("Cast 2") && player.GetComponent<CharacterMovement>().currentPower < 100)
            {
                player.GetComponent<CharacterMovement>().Show("Not enough stamina");
            }
        }
    }

    private IEnumerator Skill()
    {
        player.GetComponent<CharacterMovement>().healthSkill();
        int tmp = player.GetComponent<CharacterMovement>().armor;
        player.GetComponent<CharacterMovement>().attackSpeed = tmp + 30;
        yield return new WaitForSeconds(5f);
        player.GetComponent<CharacterMovement>().armor = tmp;
    }
}
