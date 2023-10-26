using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class HeatlhBarP1 : MonoBehaviour
{
    public List<Image> fills = new List<Image> ();
    public int maxHealth;
    public int currentHealth;
    private float lerpSpeed;

    GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player 1");
        maxHealth = player.GetComponent<CharacterController>().maxHealth;
        foreach (Image fill in fills)
        {
            fill.fillAmount = 1;
        }
        for(int  i = 0; i < PlayerPrefs.GetInt("healthBar"); i++)
        {
            fills[i].gameObject.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        currentHealth = player.GetComponent<CharacterController>().currentHealth;
        lerpSpeed = 3f * Time.deltaTime;
        switch (currentHealth)
        {
            case int tmp when (currentHealth > 6000 && currentHealth <= 7000):
                SetCurrentHealth(fills[6], 6000);
                break;

            case int tmp when (currentHealth > 5000 && currentHealth <= 6000):
                fills[6].fillAmount = 0;
                SetCurrentHealth(fills[5], 5000);
                break;

            case int tmp when (currentHealth > 4000 && currentHealth <= 5000):
                fills[5].fillAmount = 0;
                SetCurrentHealth(fills[4], 4000);
                break;

            case int tmp when (currentHealth > 3000 && currentHealth <= 4000):
                fills[4].fillAmount = 0;
                SetCurrentHealth(fills[3], 3000);
                break;

            case int tmp when (currentHealth > 2000 && currentHealth <= 3000):
                fills[3].fillAmount = 0;
                SetCurrentHealth(fills[2], 2000);
                break;

            case int tmp when (currentHealth > 1000 && currentHealth <= 2000):
                fills[2].fillAmount = 0;
                SetCurrentHealth(fills[1], 1000);
                break;

            default:
                fills[1].fillAmount = 0;
                SetCurrentHealth(fills[0], 0);
                break;
        }
    }

    public void SetCurrentHealth(Image fill, int count)
    {
        fill.fillAmount = Mathf.Lerp(fill.fillAmount, (float)(currentHealth - count) / 1000, lerpSpeed);
    }
}
