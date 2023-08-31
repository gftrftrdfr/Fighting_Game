using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pow : MonoBehaviour
{
    GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        if(tag == "Pow P1")
        {
            player = GameObject.FindGameObjectWithTag("Player 1");
        }
        else if(tag == "Pow P2")
        {
            player = GameObject.FindGameObjectWithTag("Player 2");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(player.GetComponent<CharacterController>().currentPower != 100)
        {
            GetComponent<Button>().interactable = false;
        }
        else
        {
            GetComponent<Button>().interactable = true;
        }
    }
}
