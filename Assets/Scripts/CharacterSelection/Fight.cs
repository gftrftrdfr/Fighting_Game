using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fight : MonoBehaviour
{
    public GameObject P1;
    public GameObject P2;

    public GameObject PlayerSelection;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(P1.GetComponent<CharacterChoicing>().ready && P2.GetComponent<CharacterChoicing>().ready)
        {
            GetComponent<Button>().interactable = true;
            if (Input.GetKeyDown(KeyCode.G) || Input.GetKeyDown(KeyCode.Keypad0))
            {
                PlayerSelection.GetComponent<CharacterManager>().ChangeScene();
            }
        }      
    }
}
