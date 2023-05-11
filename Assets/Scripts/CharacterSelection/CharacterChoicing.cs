using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterChoicing : MonoBehaviour
{
    public GameObject characterSelection;
    public Button buttonReady;
    int selectedCharacterOption;
    bool chooseSkin = false;
    public bool ready = false;
    Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        transform.localPosition = new Vector3(-3.03f, 1.92f, 0f);
        selectedCharacterOption = characterSelection.GetComponent<CharacterManager>().selectedCharacterOption;
        characterSelection.GetComponent<CharacterManager>().CharacterOption(selectedCharacterOption);

        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (tag == "Player 1")
        {
            if (!chooseSkin)
            {
                if (Input.GetKeyDown(KeyCode.A) && transform.localPosition.x > -3)
                {
                    transform.Translate(-1.5f, 0, 0);
                    selectedCharacterOption--;
                    characterSelection.GetComponent<CharacterManager>().CharacterOption(selectedCharacterOption);
                }
                if (Input.GetKeyDown(KeyCode.D) && transform.localPosition.x < 2.9)
                {
                    transform.Translate(1.5f, 0, 0);
                    selectedCharacterOption++;
                    characterSelection.GetComponent<CharacterManager>().CharacterOption(selectedCharacterOption);
                }
                if (Input.GetKeyDown(KeyCode.W) && transform.localPosition.y < 1.9)
                {
                    transform.Translate(0, 1.5f, 0);
                    selectedCharacterOption -= 5;
                    characterSelection.GetComponent<CharacterManager>().CharacterOption(selectedCharacterOption);
                }
                if (Input.GetKeyDown(KeyCode.S) && transform.localPosition.y > 0.5)
                {
                    transform.Translate(0, -1.5f, 0);
                    selectedCharacterOption += 5;
                    characterSelection.GetComponent<CharacterManager>().CharacterOption(selectedCharacterOption);
                }
                if (Input.GetKeyDown(KeyCode.G))
                {
                    animator.SetTrigger("Ready");
                    chooseSkin = true;
                    buttonReady.interactable = true;
                }
            }
            else if (chooseSkin)
            {
                if (Input.GetKeyDown(KeyCode.A))
                {
                    characterSelection.GetComponent<CharacterManager>().BackSkinOption();
                }
                if (Input.GetKeyDown(KeyCode.D))
                {
                    characterSelection.GetComponent<CharacterManager>().NextSkinOption();
                }
                if (Input.GetKeyDown(KeyCode.G))
                {
                    Ready();
                }
            }
        }
        else
        {
            if(!chooseSkin)
            {
                if (Input.GetKeyDown(KeyCode.LeftArrow) && transform.localPosition.x > -3)
                {
                    transform.Translate(-1.5f, 0, 0);
                    selectedCharacterOption--;
                    characterSelection.GetComponent<CharacterManager>().CharacterOption(selectedCharacterOption);
                }
                if (Input.GetKeyDown(KeyCode.RightArrow) && transform.localPosition.x < 2.9)
                {
                    transform.Translate(1.5f, 0, 0);
                    selectedCharacterOption++;
                    characterSelection.GetComponent<CharacterManager>().CharacterOption(selectedCharacterOption);
                }
                if (Input.GetKeyDown(KeyCode.UpArrow) && transform.localPosition.y < 1.9)
                {
                    transform.Translate(0, 1.5f, 0);
                    selectedCharacterOption -= 5;
                    characterSelection.GetComponent<CharacterManager>().CharacterOption(selectedCharacterOption);
                }
                if (Input.GetKeyDown(KeyCode.DownArrow) && transform.localPosition.y > 0.5)
                {
                    transform.Translate(0, -1.5f, 0);
                    selectedCharacterOption += 5;
                    characterSelection.GetComponent<CharacterManager>().CharacterOption(selectedCharacterOption);
                }
                if (Input.GetKeyDown(KeyCode.Keypad0))
                {
                    animator.SetTrigger("Ready");
                    chooseSkin = true;
                    buttonReady.interactable = true;
                }
            }
            else if (chooseSkin)
            {
                if (Input.GetKeyDown(KeyCode.LeftArrow))
                {
                    characterSelection.GetComponent<CharacterManager>().BackSkinOption();
                }
                if (Input.GetKeyDown(KeyCode.RightArrow))
                {
                    characterSelection.GetComponent<CharacterManager>().NextSkinOption();
                }
                if (Input.GetKeyDown(KeyCode.Keypad0))
                {
                    Ready();
                }
            }
        }      
    }
    public void Ready()
    {
        ready = true;
        buttonReady.interactable = false;
    }
}
