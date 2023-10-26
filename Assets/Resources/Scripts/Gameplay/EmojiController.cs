using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmojiController : MonoBehaviour
{
    GameObject emoji;

    public GameObject emoji1;
    public GameObject emoji2;
    public GameObject emoji3;
    public GameObject emoji4;
    public GameObject emoji5;
    public GameObject emoji6;
    public GameObject emoji7;
    public GameObject emoji8;
    public GameObject emoji9;

    bool check = true;
    float time = 1.5f;
    // Update is called once per frame
    void Update()
    {
        if(check)
        {
            if (tag == "Player 1")
            {
                if (Input.GetKeyDown(KeyCode.F1))
                {
                    emoji = Instantiate(emoji1);
                    Destroy(emoji, 1.5f);
                    check = false;
                }
                else if (Input.GetKeyDown(KeyCode.F2))
                {
                    emoji = Instantiate(emoji2);
                    Destroy(emoji, 1.5f);
                    check = false;
                }
                else if (Input.GetKeyDown(KeyCode.F3))
                {
                    emoji = Instantiate(emoji3);
                    Destroy(emoji, 1.5f);
                    check = false;
                }
                else if (Input.GetKeyDown(KeyCode.F4))
                {
                    emoji = Instantiate(emoji4);
                    Destroy(emoji, 1.5f);
                    check = false;
                }
                else if (Input.GetKeyDown(KeyCode.F5))
                {
                    emoji = Instantiate(emoji5);
                    Destroy(emoji, 1.5f);
                    check = false;
                }
                else if (Input.GetKeyDown(KeyCode.F6))
                {
                    emoji = Instantiate(emoji6);
                    Destroy(emoji, 1.5f);
                    check = false;
                }
                else if (Input.GetKeyDown(KeyCode.F7))
                {
                    emoji = Instantiate(emoji7);
                    Destroy(emoji, 1.5f);
                    check = false;
                }
                else if (Input.GetKeyDown(KeyCode.F8))
                {
                    emoji = Instantiate(emoji8);
                    Destroy(emoji, 1.5f);
                    check = false;
                }
                else if (Input.GetKeyDown(KeyCode.F9))
                {
                    emoji = Instantiate(emoji9);
                    Destroy(emoji, 1.5f);
                    check = false;
                }
            }
            else if (tag == "Player 2")
            {
                if (Input.GetKeyDown("[1]"))
                {
                    emoji = Instantiate(emoji1);
                    Destroy(emoji, 1.5f);
                    check = false;
                }
                else if (Input.GetKeyDown("[2]"))
                {
                    emoji = Instantiate(emoji2);
                    Destroy(emoji, 1.5f);
                    check = false;
                }
                else if (Input.GetKeyDown("[3]"))
                {
                    emoji = Instantiate(emoji3);
                    Destroy(emoji, 1.5f);
                    check = false;
                }
                else if (Input.GetKeyDown("[4]"))
                {
                    emoji = Instantiate(emoji4);
                    Destroy(emoji, 1.5f);
                    check = false;
                }
                else if (Input.GetKeyDown("[5]"))
                {
                    emoji = Instantiate(emoji5);
                    Destroy(emoji, 1.5f);
                    check = false;
                }
                else if (Input.GetKeyDown("[6]"))
                {
                    emoji = Instantiate(emoji6);
                    Destroy(emoji, 1.5f);
                    check = false;
                }
                else if (Input.GetKeyDown("[7]"))
                {
                    emoji = Instantiate(emoji7);
                    Destroy(emoji, 1.5f);
                    check = false;
                }
                else if (Input.GetKeyDown("[8]"))
                {
                    emoji = Instantiate(emoji8);;
                    Destroy(emoji, 1.5f);
                    check = false;
                }
                else if (Input.GetKeyDown("[9]"))
                {
                    emoji = Instantiate(emoji9);
                    Destroy(emoji, 1.5f);
                    check = false;
                }
            }
        }
        if(emoji)
        {
            emoji.transform.position = new Vector3(GetComponent<CharacterController>().m_CeilingCheck.position.x, GetComponent<CharacterController>().m_CeilingCheck.position.y + .5f, GetComponent<CharacterController>().m_CeilingCheck.position.z);
        }
    }

    private void FixedUpdate()
    {
        if(!check)
        {
            time -= Time.deltaTime;
            if(time <= 0 )
            {
                time = 1.5f;
                check = true;
            }
        }
    }

}
