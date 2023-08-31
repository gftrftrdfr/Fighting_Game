using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowZoneController : MonoBehaviour
{
    float tmp;
    public Transform centerPoint;

    [SerializeField] private GameObject zoneEffect;

    // Start is called before the first frame update
    void Start()
    {
        GameObject gObject = Instantiate(zoneEffect, centerPoint.position, transform.rotation);
        gObject.transform.localScale = new Vector3(2f,2f,2f);
        Destroy(gObject,5f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (this.tag == "Player 1 Skill")
        {
            if (collision.tag == "Player 2")
            {
                tmp = collision.GetComponent<Rigidbody2D>().drag;
                if (collision.GetComponent<CharacterController>().canStun)
                {
                    collision.GetComponent<Rigidbody2D>().drag = 100;
                }
            }
        }
        else if (this.tag == "Player 2 Skill")
        {
            if (collision.tag == "Player 1")
            {
                tmp = collision.GetComponent<Rigidbody2D>().drag;
                if (collision.GetComponent<CharacterController>().canStun)
                {
                    collision.GetComponent<Rigidbody2D>().drag = 100;
                }
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (this.tag == "Player 1 Skill")
        {
            if (collision.tag == "Player 2")
            {
                if (!collision.GetComponent<CharacterController>().canStun)
                {
                    collision.GetComponent<Rigidbody2D>().drag = tmp;
                }
            }
        }
        else if (this.tag == "Player 2 Skill")
        {
            if (collision.tag == "Player 1")
            {
                if (!collision.GetComponent<CharacterController>().canStun)
                {
                    collision.GetComponent<Rigidbody2D>().drag = tmp;
                }
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (this.tag == "Player 1 Skill")
        {
            if (collision.tag == "Player 2")
            {
                collision.GetComponent<Rigidbody2D>().drag = tmp;
            }
        }
        else if (this.tag == "Player 2 Skill")
        {
            if (collision.tag == "Player 1")
            {
                collision.GetComponent<Rigidbody2D>().drag = tmp;
            }
        }
    }
}
