using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PowerBarP2 : MonoBehaviour
{
    public List<Image> fills;
    public int maxPower;
    public int currentPower;
    public GameObject firePowEffect1;
    public GameObject firePowEffect2;
    private float lerpSpeed;


    GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.GetString("PlayMode") != "practice")
        {
            player = GameObject.FindGameObjectWithTag("Player 2");
            maxPower = player.GetComponent<CharacterController>().maxPower / 10;
            for (int i = 0; i < fills.Count; i++)
            {
                fills[i].fillAmount = 0;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerPrefs.GetString("PlayMode") != "practice")
        {
            currentPower = player.GetComponent<CharacterController>().currentPower;
            lerpSpeed = 3f * Time.deltaTime;
            SetCurrentPower();
            if (currentPower >= 100)
            {
                firePowEffect1.SetActive(true);
                firePowEffect2.SetActive(true);
            }
            else
            {
                firePowEffect1.SetActive(false);
                firePowEffect2.SetActive(false);
            }
        }
    }

    public void SetCurrentPower()
    {
        Color PowerColor = Color.Lerp(new Color(1f, 1f, 1f, 1f), new Color(1f, 0f, 0f, 1f), (float)currentPower / (maxPower * 10));

        if (currentPower < 100)
        {
            fills[(int)(currentPower / maxPower)].fillAmount = Mathf.Lerp(fills[(int)(currentPower / maxPower)].fillAmount, (float)(currentPower % maxPower) / 10, lerpSpeed);
            fills[(int)(currentPower / maxPower)].color = PowerColor;
            if ((int)(currentPower / maxPower) > 0)
            {
                for (int i = 0; i < (int)(currentPower / maxPower); i++)
                {
                    fills[i].fillAmount = Mathf.Lerp(fills[i].fillAmount, 1, lerpSpeed);
                    fills[(int)(currentPower / maxPower) - 1].color = PowerColor;
                }
            }
        }
        else
        {
            foreach (Image fill in fills)
            {
                fill.fillAmount = 1;
                fill.color = PowerColor;
            }
        }
        if (currentPower == 0)
        {
            foreach (Image fill in fills)
            {
                fill.fillAmount = 0;
            }
        }
    }
}
