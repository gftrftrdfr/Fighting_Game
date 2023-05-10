using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{
    private float speed = 15f;
    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); 
        rb.velocity = transform.right * speed;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(this.tag == "Player 1")
        {
            if(collision.tag == "Player 2")
            {
                collision.GetComponent<CharacterMovement>().TakeTrueDamage(50);
            }
        }
        else if (this.tag == "Player 2")
        {
            if (collision.tag == "Player 1")
            {
                collision.GetComponent<CharacterMovement>().TakeTrueDamage(50);
            }
        }
    }
}
