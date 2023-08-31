using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Numerics;
using UnityEngine;
using UnityEngine.UIElements;

public class TreeController : MonoBehaviour
{
    [SerializeField] private GameObject healEffect;

    Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        GameObject gObject = Instantiate(healEffect, new UnityEngine.Vector3(transform.position.x, transform.position.y + 3, -2), UnityEngine.Quaternion.identity);
        gObject.transform.localScale = new UnityEngine.Vector3(1.7f, 1.7f, 1.7f);
        Destroy(gObject, 5f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (this.tag == "Player 1 Skill")
        {
            if (collision.tag == "Player 1")
            {
                collision.GetComponent<CharacterController>().healthSkill(40);
            }
        }
        else if (this.tag == "Player 2 Skill")
        {
            if (collision.tag == "Player 2")
            {
                collision.GetComponent<CharacterController>().healthSkill(40);
            }
        }
    }
}
