using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SkillCooldown : MonoBehaviour
{
    public Image imageCooldown;
    public TMP_Text textCooldown;

    bool isCooldown = false;
    float coolDownTime = 10;
    public float coolDownTimer = 0;
    // Start is called before the first frame update
    void Start()
    {
        textCooldown.gameObject.SetActive(false);
        imageCooldown.fillAmount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(isCooldown)
        {
            ApplyCooldown();
        }
    }

    void ApplyCooldown()
    {
        coolDownTimer -= Time.deltaTime;
        if(coolDownTimer < 0)
        {
            isCooldown = false;
            textCooldown.gameObject.SetActive(false);
            imageCooldown.fillAmount = 0;
        }
        else
        {
            textCooldown.text = Mathf.RoundToInt(coolDownTimer).ToString();
            imageCooldown.fillAmount = coolDownTimer / coolDownTime;
        }
    }

    public void UseSkill(float cooldown)
    {
        isCooldown = true;
        textCooldown.gameObject.SetActive(true);
        coolDownTime = cooldown;
        coolDownTimer = cooldown;
    }

}
