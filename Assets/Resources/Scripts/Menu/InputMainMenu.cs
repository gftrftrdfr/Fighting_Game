using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class InputMainMenu : MonoBehaviour
{
    public Button playOffline;
    public Button playOnline;
    public Button howToPlay;
    public Button setting;
    public Button credit;

    public GameObject chosing;

    public GameObject menuTab;
    public GameObject settingTab;
    public GameObject helpTab;
    public GameObject rimTab;
    public Button quitButton;

    [Header("Setting Tab")]
    public GameObject musicSetting;
    public GameObject soundSetting;
    public GameObject resolutionSetting;
    public Toggle fullScreen;
    public GameObject qualitySetting;
    public GameObject languageSetting;
    public Button backSetting;

    [Header("Help Tab")]
    public ScrollRect scrollRect;
    public Button tab1;
    public Button tab2;
    public Button tab3;
    public Button tab2_1;
    public Button tab2_2;
    public Button tab2_3;
    public Button tab2_4;
    public Button tab2_5;
    public Button tab2_6;
    public Button backHelp;


    int count = 0;
    int countTab = 0;
    int countTab2 = 0;
    int countResolution;
    int countQuality;
    Button previousButton;
    Button presentButton;
    Color tempColor1;
    Color tempColor2;

    float cooldown = 0;
    bool check = true;
    bool checkHelp = false;
    GameObject gameObject1;
    GameObject gameObject2;
    // Start is called before the first frame update
    void Start()
    {
        gameObject1 = Instantiate(chosing, playOffline.transform);
        gameObject2 = Instantiate(chosing, settingTab.transform);
        gameObject2.transform.localScale *= 1.2f;
        playOffline.transform.localScale *= 1.2f;
        presentButton = playOffline;
        previousButton = playOnline;
    }

    // Update is called once per frame
    void Update()
    {
        if(check)
        {
            if(!helpTab.activeSelf && !settingTab.activeSelf)
            {
                if(Input.GetButtonDown("Cancel"))
                {
                    quitButton.onClick.Invoke();
                }
                if (Input.GetAxisRaw("Vertical") == -1)
                {
                    count++;
                    if (count > 3)
                    {
                        count = 0;
                    }
                    previousButton = presentButton;
                    if (count == 1 && !playOnline.interactable) count++;
                    check = false;
                    if (AudioManager.Instance)
                        AudioManager.Instance.PlaySFX("Move");
                }
                if (Input.GetAxisRaw("Vertical") == 1)
                {
                    count--;
                    if (count < 0)
                    {
                        count = 3;
                    }
                    previousButton = presentButton;
                    if (count == 1 && !playOnline.interactable) count--;
                    check = false;
                    if (AudioManager.Instance)
                        AudioManager.Instance.PlaySFX("Move");
                }

                switch (count)
                {
                    case 0:
                        presentButton = playOffline;
                        presentButton.transform.localScale = new Vector3(1.2f, 1.2f, 1.2f);
                        tempColor1 = presentButton.image.color;
                        tempColor1.a = 1f;
                        presentButton.image.color = tempColor1;
                        gameObject1.transform.localPosition = presentButton.transform.localPosition;
                        gameObject1.transform.localScale = Vector3.one;

                        previousButton.transform.localScale = Vector3.one;
                        tempColor2 = previousButton.image.color;
                        tempColor2.a = .85f;
                        previousButton.image.color = tempColor2;

                        if (Input.GetButtonDown("Submit"))
                        {
                            playOffline.GetComponent<Button>().onClick.Invoke();
                            check = false;
                            if (AudioManager.Instance)
                                AudioManager.Instance.PlaySFX("Confirm");
                        }
                        break;

                    case 1:
                        presentButton = playOnline;
                        presentButton.transform.localScale = new Vector3(1.2f, 1.2f, 1.2f);
                        tempColor1 = presentButton.image.color;
                        tempColor1.a = 1f;
                        presentButton.image.color = tempColor1;
                        gameObject1.transform.localPosition = presentButton.transform.localPosition;
                        gameObject1.transform.localScale = new Vector3(.7f, .7f, .7f);

                        previousButton.transform.localScale = Vector3.one;
                        tempColor2 = previousButton.image.color;
                        tempColor2.a = .85f;
                        previousButton.image.color = tempColor2;
                        if (Input.GetButtonDown("Submit"))
                        {
                            playOnline.GetComponent<Button>().onClick.Invoke(); check = false;
                            if (AudioManager.Instance)
                                AudioManager.Instance.PlaySFX("Confirm");
                        }
                        break;

                    case 2:
                        presentButton = howToPlay;
                        presentButton.transform.localScale = new Vector3(1.2f, 1.2f, 1.2f);
                        tempColor1 = presentButton.image.color;
                        tempColor1.a = 1f;
                        presentButton.image.color = tempColor1;
                        gameObject1.transform.localPosition = presentButton.transform.localPosition;
                        gameObject1.transform.localScale = new Vector3(.7f, .7f, .7f);

                        previousButton.transform.localScale = Vector3.one;
                        tempColor2 = previousButton.image.color;
                        tempColor2.a = .85f;
                        previousButton.image.color = tempColor2;

                        if (Input.GetButtonDown("Submit"))
                        {
                            howToPlay.GetComponent<Button>().onClick.Invoke();
                            check = false;
                            if (AudioManager.Instance)
                                AudioManager.Instance.PlaySFX("Confirm");
                        }
                        break;

                    case 3:
                        presentButton = setting;
                        presentButton.transform.localScale = new Vector3(1.2f, 1.2f, 1.2f);
                        tempColor1 = presentButton.image.color;
                        tempColor1.a = 1f;
                        presentButton.image.color = tempColor1;
                        gameObject1.transform.localPosition = presentButton.transform.localPosition;
                        gameObject1.transform.localScale = new Vector3(.7f, .7f, .7f);

                        previousButton.transform.localScale = Vector3.one;
                        tempColor2 = previousButton.image.color;
                        tempColor2.a = .85f;
                        previousButton.image.color = tempColor2;

                        if (Input.GetButtonDown("Submit"))
                        {
                            setting.GetComponent<Button>().onClick.Invoke();
                            check = false;
                            if (AudioManager.Instance)
                                AudioManager.Instance.PlaySFX("Confirm");
                        }
                        break;

                    default:
                        presentButton = credit;
                        presentButton.transform.localScale = new Vector3(1.2f, 1.2f, 1.2f);
                        tempColor1 = presentButton.image.color;
                        tempColor1.a = 1f;
                        presentButton.image.color = tempColor1;
                        gameObject1.transform.localPosition = presentButton.transform.localPosition;
                        gameObject1.transform.localScale = new Vector3(.7f, .7f, .7f);

                        previousButton.transform.localScale = Vector3.one;
                        tempColor2 = previousButton.image.color;
                        tempColor2.a = .85f;
                        previousButton.image.color = tempColor2;

                        if (Input.GetButtonDown("Submit"))
                        {
                            credit.GetComponent<Button>().onClick.Invoke();
                            check = false;
                            if (AudioManager.Instance)
                                AudioManager.Instance.PlaySFX("Confirm");
                        }
                        break;
                }
            }
            else if (helpTab.activeSelf == true)
            {
                if (Input.GetButtonDown("Cancel"))
                {
                    backHelp.onClick.Invoke();
                    if (AudioManager.Instance)
                        AudioManager.Instance.PlaySFX("Back");
                }
                if (Input.GetButtonDown("Submit"))
                {
                    checkHelp = true;
                    if (AudioManager.Instance)
                        AudioManager.Instance.PlaySFX("Confirm");
                }
                else if (Input.GetButtonDown("Cancel") && checkHelp)
                {
                    checkHelp = false;
                    if (AudioManager.Instance)
                        AudioManager.Instance.PlaySFX("Back");
                }              

                if (checkHelp)
                {
                    rimTab.SetActive(true);
                    if(countTab == 0)
                    {
                        int scroll = 0;
                        if (Input.GetAxisRaw("Vertical") == -1)
                        {
                            scroll = -1;
                            if (AudioManager.Instance)
                                AudioManager.Instance.PlaySFX("Move");
                        }
                        if (Input.GetAxisRaw("Vertical") == 1)
                        {
                            scroll = 1;
                            if (AudioManager.Instance)
                                AudioManager.Instance.PlaySFX("Move");
                        }
                        float contentHeight = scrollRect.content.sizeDelta.y;
                        float contentShift = 300 * scroll * Time.deltaTime;                      
                        scrollRect.verticalNormalizedPosition += contentShift / contentHeight;
                    }
                    else if(countTab == 1) 
                    {
                        if (Input.GetAxisRaw("Horizontal") == 1)
                        {
                            countTab2++;
                            if (countTab2 > 5)
                            {
                                countTab2 = 0;
                            }
                            check = false;
                            if (AudioManager.Instance)
                                AudioManager.Instance.PlaySFX("Move");
                        }
                        if (Input.GetAxisRaw("Horizontal") == -1)
                        {
                            countTab2--;
                            if (countTab2 < 0)
                            {
                                countTab2 = 5;
                            }
                            check = false;
                            if (AudioManager.Instance)
                                AudioManager.Instance.PlaySFX("Move");
                        }
                        switch (countTab2)
                        {
                            case 0:
                                tab2_1.onClick.Invoke();
                                break;
                            case 1:
                                tab2_2.onClick.Invoke();
                                break;
                            case 2:
                                tab2_3.onClick.Invoke();
                                break;
                            case 3:
                                tab2_4.onClick.Invoke();
                                break;
                            case 4:
                                tab2_5.onClick.Invoke();
                                break;
                            case 5:
                                tab2_6.onClick.Invoke();
                                break;
                        }
                    }
                    
                }

                else
                {
                    rimTab.SetActive(false);
                    if (Input.GetAxisRaw("Vertical") == -1)
                    {
                        countTab++;
                        if (countTab > 2)
                        {
                            countTab = 0;
                        }
                        check = false;
                        if (AudioManager.Instance)
                            AudioManager.Instance.PlaySFX("Move");
                    }
                    if (Input.GetAxisRaw("Vertical") == 1)
                    {
                        countTab--;
                        if (countTab < 0)
                        {
                            countTab = 3;
                        }
                        check = false;
                        if (AudioManager.Instance)
                            AudioManager.Instance.PlaySFX("Move");
                    }
                    switch (countTab)
                    {
                        case 0:
                            tab1.onClick.Invoke();
                            break;
                        case 1:
                            tab2.onClick.Invoke();
                            break;
                        case 2:
                            tab3.onClick.Invoke();
                            break;
                    }
                }
            }

            else if (settingTab.activeSelf == true)
            {
                if(Input.GetButtonDown("Cancel"))
                {
                    backSetting.onClick.Invoke();
                    if (AudioManager.Instance)
                        AudioManager.Instance.PlaySFX("Back");
                }
                if (Input.GetAxisRaw("Vertical") == -1)
                {
                    countTab++;
                    if (countTab > 4)
                    {
                        countTab = 0;
                    }
                    check = false;
                    if (AudioManager.Instance)
                        AudioManager.Instance.PlaySFX("Move");
                }
                if (Input.GetAxisRaw("Vertical") == 1)
                {
                    countTab--;
                    if (countTab < 0)
                    {
                        countTab = 4;
                    }
                    check = false;
                    if (AudioManager.Instance)
                        AudioManager.Instance.PlaySFX("Move");
                }
                switch (countTab)
                {
                    case 0:
                        gameObject2.transform.localPosition = musicSetting.transform.localPosition;
                        musicSetting.GetComponentInChildren<Slider>().value += Input.GetAxisRaw("Horizontal") * Time.deltaTime;
                        break;
                    case 1:
                        gameObject2.transform.localPosition = soundSetting.transform.localPosition;
                        soundSetting.GetComponentInChildren<Slider>().value += Input.GetAxisRaw("Horizontal") * Time.deltaTime;
                        break;
                    case 2:
                        gameObject2.transform.localPosition = resolutionSetting.transform.localPosition;
                        countResolution = resolutionSetting.GetComponentInChildren<TMP_Dropdown>().value;
                        if (Input.GetAxisRaw("Horizontal") == 1)
                        {
                            countResolution++;
                            if (countResolution > resolutionSetting.GetComponentInChildren<TMP_Dropdown>().options.Count - 1)
                            {
                                countResolution = 0;
                            }
                            check = false;
                            if (AudioManager.Instance)
                                AudioManager.Instance.PlaySFX("Move");
                        }
                        if (Input.GetAxisRaw("Horizontal") == -1)
                        {
                            countResolution--;
                            if (countResolution < 0)
                            {
                                countResolution = resolutionSetting.GetComponentInChildren<TMP_Dropdown>().options.Count - 1;
                            }
                            check = false;
                            if (AudioManager.Instance)
                                AudioManager.Instance.PlaySFX("Move");
                        }
                        resolutionSetting.GetComponentInChildren<TMP_Dropdown>().value = countResolution;
                        if(Input.GetButtonDown("Submit"))
                        {
                            if(!fullScreen.isOn)
                            {
                                fullScreen.isOn = true;
                            }
                            else
                            {
                                fullScreen.isOn = false;
                            }
                            if (AudioManager.Instance)
                                AudioManager.Instance.PlaySFX("Confirm");
                        }
                        break;
                    case 3:
                        gameObject2.transform.localPosition = qualitySetting.transform.localPosition;
                        countQuality = qualitySetting.GetComponentInChildren<TMP_Dropdown>().value;
                        if (Input.GetAxisRaw("Horizontal") == 1)
                        {
                            countQuality++;
                            if (countQuality > qualitySetting.GetComponentInChildren<TMP_Dropdown>().options.Count - 1)
                            {
                                countQuality = 0;
                            }
                            check = false;
                            if (AudioManager.Instance)
                                AudioManager.Instance.PlaySFX("Move");
                        }
                        if (Input.GetAxisRaw("Horizontal") == -1)
                        {
                            countQuality--;
                            if (countQuality < 0)
                            {
                                countQuality = qualitySetting.GetComponentInChildren<TMP_Dropdown>().options.Count - 1;
                            }
                            check = false;
                            if (AudioManager.Instance)
                                AudioManager.Instance.PlaySFX("Move");
                        }
                        qualitySetting.GetComponentInChildren<TMP_Dropdown>().value = countQuality;
                        break;
                    case 4:
                        gameObject2.transform.localPosition = languageSetting.transform.localPosition;
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
}
