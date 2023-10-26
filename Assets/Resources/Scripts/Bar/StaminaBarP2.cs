using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class StaminaBarP2 : MonoBehaviour
{
    public Image fill;
    public float maxStamina;
    public float currentStamina;

    GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.GetString("PlayMode") != "practice")
        {
            player = GameObject.FindGameObjectWithTag("Player 2");
            maxStamina = player.GetComponent<CharacterController>().maxStamina;
            fill.fillAmount = 1;
        }
    }

    // Update is called once per frame
    public void Update()
    {
        if (PlayerPrefs.GetString("PlayMode") != "practice")
        {
            currentStamina = player.GetComponent<CharacterController>().currentStamina;
            SetCurrentStamina();
        }
    }

    public void SetCurrentStamina()
    {
        fill.fillAmount = currentStamina / maxStamina;
    }
}
