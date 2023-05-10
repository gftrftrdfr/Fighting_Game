using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class HeatlhBarP1 : MonoBehaviour
{
    public Image fill;
    public int maxHealth;
    public int currentHealth;
    private float lerpSpeed;

    GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player 1");
        maxHealth = player.GetComponent<CharacterMovement>().maxHealth;
        fill.fillAmount = 1;
    }

    // Update is called once per frame
    void Update()
    {
        currentHealth = player.GetComponent<CharacterMovement>().currentHealth;
        lerpSpeed = 3f * Time.deltaTime;
        SetCurrentHealth();
    }

    public void SetCurrentHealth()
    {
        fill.fillAmount = Mathf.Lerp(fill.fillAmount, (float)currentHealth / maxHealth, lerpSpeed);
        Color healthColor = Color.Lerp(new Color(0.55f, 0f, 0f, 1f), new Color(1f, 0.85f, 0.85f, 1f), (float)currentHealth / maxHealth);
        fill.color = healthColor;
    }
}
