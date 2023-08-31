using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
using UnityEngine.UIElements;

public class SharpOrb : MonoBehaviour
{
    public float speed;
    private Rigidbody2D rb;

    [SerializeField] private GameObject OrbEffect;

    public Transform centerPoint;
    public int dmg;
    public bool buff = false;

    // Start is called before the first frame update
    void Start()
    {        
        GameObject gObject = Instantiate(OrbEffect, centerPoint.position, transform.rotation);
        gObject.transform.parent = this.transform;
        gObject.transform.localScale = new Vector3(2,2,2);
        if(buff)
        {
            speed = 20f;
            gObject.transform.localScale = new Vector3(3, 3, 3);
        }
        else
        {
            speed = 10f;
            gObject.transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
        }
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.right * speed;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(buff)
        {
            if (tag == "Player 1 Skill")
            {
                if (collision.tag == "Player 2")
                {
                    collision.GetComponent<CharacterController>().TakeTrueDamage(dmg);
                }
            }
            else if (this.tag == "Player 2 Skill")
            {
                if (collision.tag == "Player 1")
                {
                    collision.GetComponent<CharacterController>().TakeTrueDamage(dmg);
                }
            }
        }
        else
        {
            if (tag == "Player 1 Skill")
            {
                if (collision.tag == "Player 2")
                {
                    collision.GetComponent<CharacterController>().TakeDamage(dmg);
                }
            }
            else if (tag == "Player 2 Skill")
            {
                if (collision.tag == "Player 1")
                {
                    collision.GetComponent<CharacterController>().TakeDamage(dmg);
                }
            }
        }
    }
}
