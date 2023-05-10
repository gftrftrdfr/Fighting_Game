using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterChoicing : MonoBehaviour
{
    public GameObject characterSelection;
    int selectedCharacterOption;
    // Start is called before the first frame update
    void Start()
    {
        transform.localPosition = new Vector3(-3.03f, 1.92f, 0f);
        selectedCharacterOption = characterSelection.GetComponent<CharacterManager>().selectedCharacterOption;
        characterSelection.GetComponent<CharacterManager>().CharacterOption(selectedCharacterOption);
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(selectedCharacterOption);
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
    }
}
