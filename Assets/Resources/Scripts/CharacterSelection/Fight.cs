using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Fight : MonoBehaviour
{
    public GameObject chosing;

    public GameObject P1;
    public GameObject P2;

    public GameObject PlayerSelection;

    public GameObject settings;
    public GameObject select;

    public TMP_Text healthCount;
    public Slider healthSettings;
    public TMP_Dropdown timeSettings;
    public Button fight;

    float cooldown = 0;
    bool check = false;
    int count = 0;
    GameObject gObject;
    Vector3 temp = new Vector3(-88.8f, 0, 0);
    // Start is called before the first frame update
    void Start()
    {
        gObject = Instantiate(chosing, settings.transform);
        gObject.transform.localPosition += temp;
        gObject.transform.localScale = new Vector3(.6f, .6f, .6f);
    }

    // Update is called once per frame
    void Update()
    {      
        if (P1.GetComponent<CharacterChoicing>().ready && P2.GetComponent<CharacterChoicing>().ready)
        {
            settings.gameObject.SetActive(true);
            select.gameObject.SetActive(false);          

            if (check)
            {
                if (Input.GetButtonDown("Fire2") || Input.GetKeyDown("joystick 2 button 0") || Input.GetButtonDown("Fire1") || Input.GetKeyDown("joystick 1 button 0") || Input.GetButtonDown("Submit"))
                {
                    PlayerPrefs.SetInt("healthBar", (int)healthSettings.value);
                    string times = timeSettings.options[timeSettings.value].text;
                    if(times == "No Limit")
                    {
                        times = "0";
                    }
                    PlayerPrefs.SetInt("time", int.Parse(times));
                    fight.onClick.Invoke();
                    if (AudioManager.Instance)
                        AudioManager.Instance.PlaySFX("Confirm");
                }
                if (Input.GetButtonDown("Cancel"))
                {
                    Back();
                    if (AudioManager.Instance)
                        AudioManager.Instance.PlaySFX("Back");
                }
                if (Input.GetAxisRaw("Vertical") == -1)
                {
                    count++;
                    if (count > 1)
                    {
                        count = 0;
                    }
                    check = false;
                    if (AudioManager.Instance)
                        AudioManager.Instance.PlaySFX("Move");
                }
                if (Input.GetAxisRaw("Vertical") == 1)
                {
                    count--;
                    if (count < 0)
                    {
                        count = 1;
                    }
                    check = false;
                    if (AudioManager.Instance)
                        AudioManager.Instance.PlaySFX("Move");
                }

                switch (count)
                {
                    case 0:
                        gObject.transform.localPosition = healthSettings.transform.localPosition;
                        gObject.transform.localPosition += temp;
                        if (Input.GetAxisRaw("Horizontal") == -1)
                        {
                            healthSettings.value--;
                            check = false;
                            if (AudioManager.Instance)
                                AudioManager.Instance.PlaySFX("Move");
                        }
                        if (Input.GetAxisRaw("Horizontal") == 1)
                        {
                            healthSettings.value++;
                            check = false;
                            if (AudioManager.Instance)
                                AudioManager.Instance.PlaySFX("Move");
                        }
                        healthCount.SetText(healthSettings.value.ToString());
                        break;
                    case 1:
                        gObject.transform.localPosition = timeSettings.transform.localPosition;
                        gObject.transform.localPosition += temp;
                        if (Input.GetAxisRaw("Horizontal") == -1)
                        {
                            timeSettings.value--;
                            check = false;
                            if (AudioManager.Instance)
                                AudioManager.Instance.PlaySFX("Move");
                        }
                        if (Input.GetAxisRaw("Horizontal") == 1)
                        {
                            timeSettings.value++;
                            check = false;
                            if (AudioManager.Instance)
                                AudioManager.Instance.PlaySFX("Move");
                        }
                        break;
                }
            }
        }      
    }
    private void FixedUpdate()
    {
        if (!check)
        {
            cooldown += Time.fixedDeltaTime;
            if (cooldown > .2f)
            {
                check = true;
                cooldown = 0;
            }
        }
    }

    public void Back()
    {
        P1.GetComponent<CharacterChoicing>().chooseSkin = false;
        P2.GetComponent<CharacterChoicing>().chooseSkin = false;
        P1.GetComponent<CharacterChoicing>().ready = false;
        P2.GetComponent<CharacterChoicing>().ready = false;
        settings.gameObject.SetActive(false);
        select.gameObject.SetActive(true);
    }
}
