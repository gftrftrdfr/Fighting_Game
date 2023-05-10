using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeController : MonoBehaviour
{
    Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (this.tag == "Player 1")
        {
            if (collision.tag == "Player 1")
            {
                collision.GetComponent<CharacterMovement>().healthSkill(40);
            }
        }
        else if (this.tag == "Player 2")
        {
            if (collision.tag == "Player 2")
            {
                collision.GetComponent<CharacterMovement>().healthSkill(40);
            }
        }
    }
}
