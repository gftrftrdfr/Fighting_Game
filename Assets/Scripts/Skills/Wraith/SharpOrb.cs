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
        rb = GetComponent<Rigidbody2D>(); 
        rb.velocity = transform.right * speed;
        GameObject gObject = Instantiate(OrbEffect, centerPoint.position, transform.rotation);
        gObject.transform.parent = this.transform;
        gObject.transform.localScale = new Vector3(1,1,1);
        if(buff)
        {
            speed = 20f;
        }
        else
        {
            speed = 10f;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(buff)
        {
            if (this.tag == "Player 1")
            {
                if (collision.tag == "Player 2")
                {
                    collision.GetComponent<CharacterMovement>().TakeTrueDamage(dmg);
                }
            }
            else if (this.tag == "Player 2")
            {
                if (collision.tag == "Player 1")
                {
                    collision.GetComponent<CharacterMovement>().TakeTrueDamage(dmg);
                }
            }
        }
        else
        {
            if (this.tag == "Player 1")
            {
                if (collision.tag == "Player 2")
                {
                    collision.GetComponent<CharacterMovement>().TakeDamage(dmg);
                }
            }
            else if (this.tag == "Player 2")
            {
                if (collision.tag == "Player 1")
                {
                    collision.GetComponent<CharacterMovement>().TakeDamage(dmg);
                }
            }
        }
    }
}
