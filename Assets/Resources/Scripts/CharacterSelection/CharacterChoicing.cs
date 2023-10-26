using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CharacterChoicing : MonoBehaviour
{
    public GameObject characterSelection;
    public Button buttonReady;
    int selectedCharacterOption;
    public bool chooseSkin = false;
    public bool ready = false;
    public int temp = 1;
    public CharacterDatabase characterDB;
    int totalChamp;
    Animator animator;
    float cooldown = 0;
    bool check = true;
    // Start is called before the first frame update
    void Start()
    {
        selectedCharacterOption = characterSelection.GetComponent<CharacterManager>().selectedCharacterOption;
        characterSelection.GetComponent<CharacterManager>().CharacterOption(selectedCharacterOption);

        animator = GetComponent<Animator>();
        totalChamp = characterDB.CharacterCount;
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetBool("pause", chooseSkin);
        if(check)
        {
            if (tag == "Player 1")
            {
                if (!chooseSkin)
                {
                    if (Input.GetAxisRaw("Horizontal 1") == -1)
                    {
                        if (temp - 1 > 0)
                        {
                            if ((temp - 1) % 5 == 0)
                            {
                                temp -= 1;
                                transform.Translate(4 * 1.5f, 1.5f, 0);
                                selectedCharacterOption--;
                                characterSelection.GetComponent<CharacterManager>().CharacterOption(selectedCharacterOption);
                            }
                            else
                            {
                                temp -= 1;
                                transform.Translate(-1.5f, 0, 0);
                                selectedCharacterOption--;
                                characterSelection.GetComponent<CharacterManager>().CharacterOption(selectedCharacterOption);
                            }
                        }
                        else
                        {
                            temp = totalChamp;
                            int row = totalChamp / 5;
                            int column = (totalChamp % 5) - 1;
                            if (column < 0) column = 0;
                            transform.Translate(column * 1.5f, row * -1.5f, 0);
                            selectedCharacterOption = totalChamp - 1;
                            characterSelection.GetComponent<CharacterManager>().CharacterOption(selectedCharacterOption);
                        }
                        check = false;
                        if (AudioManager.Instance)
                            AudioManager.Instance.PlaySFX("Move");
                    }
                    if (Input.GetAxisRaw("Horizontal 1") == 1)
                    {
                        if (temp + 1 <= totalChamp)
                        {
                            if (temp % 5 == 0)
                            {
                                temp += 1;
                                transform.Translate(4 * -1.5f, -1.5f, 0);
                                selectedCharacterOption++;
                                characterSelection.GetComponent<CharacterManager>().CharacterOption(selectedCharacterOption);
                            }
                            else
                            {
                                temp += 1;
                                transform.Translate(1.5f, 0, 0);
                                selectedCharacterOption++;
                                characterSelection.GetComponent<CharacterManager>().CharacterOption(selectedCharacterOption);
                            }
                        }
                        else
                        {
                            temp = 1;
                            transform.position = new Vector3(-3.03f, 1.92f, 0);
                            selectedCharacterOption = 0;
                            characterSelection.GetComponent<CharacterManager>().CharacterOption(selectedCharacterOption);
                        }
                        check = false;
                        if (AudioManager.Instance)
                            AudioManager.Instance.PlaySFX("Move");
                    }
                    if (Input.GetAxisRaw("Vertical 1") == -1 && temp - 5 >= 1)
                    {
                        temp -= 5;
                        transform.Translate(0, 1.5f, 0);
                        selectedCharacterOption -= 5;
                        characterSelection.GetComponent<CharacterManager>().CharacterOption(selectedCharacterOption);
                        check = false;
                        if (AudioManager.Instance)
                            AudioManager.Instance.PlaySFX("Move");
                    }
                    if (Input.GetAxisRaw("Vertical 1") == 1 && temp + 5 <= totalChamp)
                    {
                        temp += 5;
                        transform.Translate(0, -1.5f, 0);
                        selectedCharacterOption += 5;
                        characterSelection.GetComponent<CharacterManager>().CharacterOption(selectedCharacterOption);
                        check = false;
                        if (AudioManager.Instance)
                            AudioManager.Instance.PlaySFX("Move");
                    }
                    if (Input.GetButtonDown("Fire1") || Input.GetKeyDown("joystick 1 button 0"))
                    {
                        animator.SetTrigger("Ready");
                        chooseSkin = true;
                        buttonReady.interactable = true;
                        if (AudioManager.Instance)
                            AudioManager.Instance.PlaySFX("Confirm");
                    }
                }
                else if (chooseSkin)
                {
                    if (Input.GetAxisRaw("Horizontal 1") == -1)
                    {
                        characterSelection.GetComponent<CharacterManager>().BackSkinOption();
                        check = false;
                        if (AudioManager.Instance)
                            AudioManager.Instance.PlaySFX("Move");
                    }
                    if (Input.GetAxisRaw("Horizontal 1") == 1)
                    {
                        characterSelection.GetComponent<CharacterManager>().NextSkinOption();
                        check = false;
                        if (AudioManager.Instance)
                            AudioManager.Instance.PlaySFX("Move");
                    }
                    if (Input.GetButtonDown("Fire1") || Input.GetKeyDown("joystick 1 button 0"))
                    {
                        Ready();
                        if (AudioManager.Instance)
                            AudioManager.Instance.PlaySFX("Confirm");
                    }
                }
            }
            else
            {
                if (!chooseSkin)
                {
                    if (Input.GetAxisRaw("Horizontal 2") == -1)
                    {
                        if (temp - 1 > 0)
                        {
                            if ((temp - 1) % 5 == 0)
                            {
                                temp -= 1;
                                transform.Translate(4 * 1.5f, 1.5f, 0);
                                selectedCharacterOption--;
                                characterSelection.GetComponent<CharacterManager>().CharacterOption(selectedCharacterOption);
                            }
                            else
                            {
                                temp -= 1;
                                transform.Translate(-1.5f, 0, 0);
                                selectedCharacterOption--;
                                characterSelection.GetComponent<CharacterManager>().CharacterOption(selectedCharacterOption);
                            }
                        }
                        else
                        {
                            temp = totalChamp;
                            int row = totalChamp / 5;
                            int column = (totalChamp % 5) - 1;
                            if (column < 0) column = 0;
                            transform.Translate(column * 1.5f, row * -1.5f, 0);
                            selectedCharacterOption = totalChamp - 1;
                            characterSelection.GetComponent<CharacterManager>().CharacterOption(selectedCharacterOption);
                        }
                        check = false;
                        if (AudioManager.Instance)
                            AudioManager.Instance.PlaySFX("Move");
                    }
                    if (Input.GetAxisRaw("Horizontal 2") == 1)
                    {
                        if (temp + 1 <= totalChamp)
                        {
                            if (temp % 5 == 0)
                            {
                                temp += 1;
                                transform.Translate(4 * -1.5f, -1.5f, 0);
                                selectedCharacterOption++;
                                characterSelection.GetComponent<CharacterManager>().CharacterOption(selectedCharacterOption);
                            }
                            else
                            {
                                temp += 1;
                                transform.Translate(1.5f, 0, 0);
                                selectedCharacterOption++;
                                characterSelection.GetComponent<CharacterManager>().CharacterOption(selectedCharacterOption);
                            }
                        }
                        else
                        {
                            temp = 1;
                            transform.position = new Vector3(-3.03f, 1.92f, 0);
                            selectedCharacterOption = 0;
                            characterSelection.GetComponent<CharacterManager>().CharacterOption(selectedCharacterOption);
                        }
                        check = false;
                        if (AudioManager.Instance)
                            AudioManager.Instance.PlaySFX("Move");
                    }
                    if (Input.GetAxisRaw("Vertical 2") == -1 && temp - 5 >= 1)
                    {
                        temp -= 5;
                        transform.Translate(0, 1.5f, 0);
                        selectedCharacterOption -= 5;
                        characterSelection.GetComponent<CharacterManager>().CharacterOption(selectedCharacterOption);
                        check = false;
                        if (AudioManager.Instance)
                            AudioManager.Instance.PlaySFX("Move");
                    }
                    if (Input.GetAxisRaw("Vertical 2") == 1 && temp + 5 <= totalChamp)
                    {
                        temp += 5;
                        transform.Translate(0, -1.5f, 0);
                        selectedCharacterOption += 5;
                        characterSelection.GetComponent<CharacterManager>().CharacterOption(selectedCharacterOption);
                        check = false;
                        if (AudioManager.Instance)
                            AudioManager.Instance.PlaySFX("Move");
                    }
                    if (Input.GetButtonDown("Fire2") || Input.GetKeyDown("joystick 2 button 0"))
                    {
                        animator.SetTrigger("Ready");
                        chooseSkin = true;
                        buttonReady.interactable = true;
                        if (AudioManager.Instance)
                            AudioManager.Instance.PlaySFX("Confirm");
                    }
                }
                else if (chooseSkin)
                {
                    if (Input.GetAxisRaw("Horizontal 2") == -1)
                    {
                        characterSelection.GetComponent<CharacterManager>().BackSkinOption();
                        check = false;
                        if (AudioManager.Instance)
                            AudioManager.Instance.PlaySFX("Move");
                    }
                    if (Input.GetAxisRaw("Horizontal 2") == 1)
                    {
                        characterSelection.GetComponent<CharacterManager>().NextSkinOption();
                        check = false;
                        if (AudioManager.Instance)
                            AudioManager.Instance.PlaySFX("Move");
                    }
                    if (Input.GetButtonDown("Fire2") || Input.GetKeyDown("joystick 2 button 0"))
                    {
                        Ready();
                        if (AudioManager.Instance)
                            AudioManager.Instance.PlaySFX("Confirm");
                    }
                }
            }
        }
    }

    private void FixedUpdate()
    {
        if(!check)
        {
            cooldown += Time.fixedDeltaTime;
            if (cooldown>.2f)
            {
                check = true;
                cooldown = 0;
            }
        }       
    }
    public void Ready()
    {
        ready = true;
        buttonReady.interactable = false;
    }

}
