using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
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

    int count = 0;
    Button previousButton;
    Button presentButton;
    Color tempColor1;
    Color tempColor2;

    float cooldown = 0;
    bool check = true;
    GameObject gameObject;
    // Start is called before the first frame update
    void Start()
    {
        gameObject = Instantiate(chosing, playOffline.transform);
        playOffline.transform.localScale *= 1.2f;
        presentButton = playOffline;
        previousButton = playOnline;
    }

    // Update is called once per frame
    void Update()
    {
        if(check)
        {
            if (Input.GetAxisRaw("Vertical") == 1)
            {
                count++;
                if (count > 3)
                {
                    count = 0;
                }
                previousButton = presentButton;
                if (count == 1 && !playOnline.interactable) count++;
                check = false;
            }
            if (Input.GetAxisRaw("Vertical") == -1)
            {
                count--;
                if (count < 0)
                {
                    count = 3;
                }
                previousButton = presentButton;
                if (count == 1 && !playOnline.interactable) count--;
                check = false;
            }

            switch (count)
            {
                case 0:
                    presentButton = playOffline;
                    presentButton.transform.localScale = new Vector3(1.2f, 1.2f, 1.2f);
                    tempColor1 = presentButton.image.color;
                    tempColor1.a = 1f;
                    presentButton.image.color = tempColor1;
                    gameObject.transform.localPosition = presentButton.transform.localPosition;
                    gameObject.transform.localScale = Vector3.one;

                    previousButton.transform.localScale = Vector3.one;
                    tempColor2 = previousButton.image.color;
                    tempColor2.a = .85f;
                    previousButton.image.color = tempColor2;
                    break;

                case 1:
                    presentButton = playOnline;
                    presentButton.transform.localScale = new Vector3(1.2f, 1.2f, 1.2f);
                    tempColor1 = presentButton.image.color;
                    tempColor1.a = 1f;
                    presentButton.image.color = tempColor1;
                    gameObject.transform.localPosition = presentButton.transform.localPosition;
                    gameObject.transform.localScale = new Vector3(.7f,.7f,.7f);

                    previousButton.transform.localScale = Vector3.one;
                    tempColor2 = previousButton.image.color;
                    tempColor2.a = .85f;
                    previousButton.image.color = tempColor2;
                    break;

                case 2:
                    presentButton = howToPlay;
                    presentButton.transform.localScale = new Vector3(1.2f, 1.2f, 1.2f);
                    tempColor1 = presentButton.image.color;
                    tempColor1.a = 1f;
                    presentButton.image.color = tempColor1;
                    gameObject.transform.localPosition = presentButton.transform.localPosition;
                    gameObject.transform.localScale = new Vector3(.7f, .7f, .7f);

                    previousButton.transform.localScale = Vector3.one;
                    tempColor2 = previousButton.image.color;
                    tempColor2.a = .85f;
                    previousButton.image.color = tempColor2;
                    break;

                case 3:
                    presentButton = setting;
                    presentButton.transform.localScale = new Vector3(1.2f, 1.2f, 1.2f);
                    tempColor1 = presentButton.image.color;
                    tempColor1.a = 1f;
                    presentButton.image.color = tempColor1;
                    gameObject.transform.localPosition = presentButton.transform.localPosition;
                    gameObject.transform.localScale = new Vector3(.7f, .7f, .7f);

                    previousButton.transform.localScale = Vector3.one;
                    tempColor2 = previousButton.image.color;
                    tempColor2.a = .85f;
                    previousButton.image.color = tempColor2;
                    break;

                default:
                    presentButton = credit;
                    presentButton.transform.localScale = new Vector3(1.2f, 1.2f, 1.2f);
                    tempColor1 = presentButton.image.color;
                    tempColor1.a = 1f;
                    presentButton.image.color = tempColor1;
                    gameObject.transform.localPosition = presentButton.transform.localPosition;
                    gameObject.transform.localScale = new Vector3(.7f, .7f, .7f);

                    previousButton.transform.localScale = Vector3.one;
                    tempColor2 = previousButton.image.color;
                    tempColor2.a = .85f;
                    previousButton.image.color = tempColor2;
                    break;
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
