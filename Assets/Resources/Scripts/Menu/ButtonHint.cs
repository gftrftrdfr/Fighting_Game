using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ButtonHint : MonoBehaviour
{
    Gamepad gamepad;
    public GameObject gamepadHint;
    public GameObject keyboardHint;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        gamepad = Gamepad.current;
        if (gamepad != null)
        {
            gamepadHint.SetActive(true);
            keyboardHint.SetActive(false);
        }
        else
        {
            gamepadHint.SetActive(false);
            keyboardHint.SetActive(true) ;
        }

    }
}
