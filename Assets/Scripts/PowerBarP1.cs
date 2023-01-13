using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PowerBarP1 : MonoBehaviour
{
    public List<Image> fills;
    public int maxPower;
    public int currentPower;
    private float lerpSpeed;

    GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player 1");
        maxPower = player.GetComponent<CharacterMovement>().maxPower / 10;
        for (int i = 0; i < fills.Count; i++)
        {
            fills[i].fillAmount = 0;
        }
    }

    // Update is called once per frame
    void Update()
    {
        currentPower = player.GetComponent<CharacterMovement>().currentPower;
        lerpSpeed = 3f * Time.deltaTime;
        SetCurrentPower();
        
    }

    public void SetCurrentPower()
    {
        
        for (int i = 0; i < fills.Count; i++)
        {
            if (currentPower / 10 > (float)i )
            {
                fills[i].fillAmount = Mathf.Lerp(fills[i].fillAmount, 1, lerpSpeed);
                Color PowerColor = Color.Lerp(new Color(1f, 1f, 1f, 1f), new Color(1f, 0f, 0f, 1f), (float)currentPower / (maxPower * 10));
                fills[i].color = PowerColor;

            }
            

        }
    }
}
