using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class Mace : MonoBehaviour
{
    private float speed = 10f;
    private Rigidbody2D rb;
    public bool check;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();       
        if(check)
        {
            rb.velocity = transform.right * speed;
            transform.localScale *= 1.5f; 
        }
        else
        {
            rb.velocity = transform.right * speed * 2;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (tag == "Player 1 Skill")
        {
            if (collision.tag == "Player 2")
            {
                if (check)
                {
                    collision.GetComponent<CharacterController>().TakeDamage(200);
                    Vector2 newVector = transform.position - collision.GetComponent<CharacterController>().m_GroundCheck.position;
                    if (newVector.x < 0)
                    {
                        collision.GetComponent<Rigidbody2D>().AddForce(new Vector2(collision.GetComponent<Rigidbody2D>().mass * 1500, collision.GetComponent<Rigidbody2D>().mass * 100));
                    }
                    else
                    {
                        collision.GetComponent<Rigidbody2D>().AddForce(new Vector2(collision.GetComponent<Rigidbody2D>().mass * -1500, collision.GetComponent<Rigidbody2D>().mass * 100));
                    }
                }
                else
                {
                    collision.GetComponent<CharacterController>().TakeDamage(100);
                }
                Destroy(gameObject);
            }
        }
        else if (tag == "Player 2 Skill")
        {
            if (collision.tag == "Player 1")
            {
                if (check)
                {
                    collision.GetComponent<CharacterController>().TakeDamage(200);
                    Vector2 newVector = transform.position - collision.GetComponent<CharacterController>().m_GroundCheck.position;
                    if (newVector.x < 0)
                    {
                        collision.GetComponent<Rigidbody2D>().AddForce(new Vector2(collision.GetComponent<Rigidbody2D>().mass * 1500, collision.GetComponent<Rigidbody2D>().mass * 100));
                    }
                    else
                    {
                        collision.GetComponent<Rigidbody2D>().AddForce(new Vector2(collision.GetComponent<Rigidbody2D>().mass * -1500, collision.GetComponent<Rigidbody2D>().mass * 100));
                    }
                }
                else
                {
                    collision.GetComponent<CharacterController>().TakeDamage(100);
                }
                Destroy(gameObject);
            }
        }
    }
}
