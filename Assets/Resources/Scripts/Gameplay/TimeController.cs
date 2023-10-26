using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TimeController : MonoBehaviour
{
    public GameObject mainCam;
    public TMP_Text txtTime;
    float timeGameplay;
    public float timeInGame;
    // Start is called before the first frame update
    void Start()
    {
        timeGameplay = PlayerPrefs.GetFloat("time");
        timeInGame = timeGameplay;
    }

    // Update is called once per frame
    void Update()
    {
        if(timeGameplay != 0)
        {
            timeInGame -= Time.deltaTime;
            
            if(timeInGame <= 0)
            {
                timeInGame = 0;
                foreach(Transform player in mainCam.GetComponent<MainCamera>().players)
                {
                    player.gameObject.GetComponent<CharacterController>().canTakeDame = false;
                }
            }
            txtTime.text = ((int)timeInGame).ToString();
        }
    }
}
