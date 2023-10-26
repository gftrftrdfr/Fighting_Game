using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BackButton : MonoBehaviour
{
    public GameObject P1;
    public GameObject P2;
    public Button readyP1;
    public Button readyP2;
    // Start is called before the first frame update
    void Update()
    {
        if(Input.GetButtonDown("Cancel"))
        {
            Back();
            if (AudioManager.Instance)
                AudioManager.Instance.PlaySFX("Back");
        }
    }

    public void Back()
    {
        if(P1.GetComponent<CharacterChoicing>().chooseSkin || P2.GetComponent<CharacterChoicing>().chooseSkin)
        {
            P1.GetComponent<CharacterChoicing>().chooseSkin = false;
            P2.GetComponent<CharacterChoicing>().chooseSkin = false;
        }
        else
        {
            SceneManager.LoadScene("SelectMap");
        }
    }
}
