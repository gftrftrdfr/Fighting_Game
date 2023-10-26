using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Numerics;
using UnityEngine;
using UnityEngine.UIElements;

public class TreeController : MonoBehaviour
{
    [SerializeField] private GameObject healEffect;
    public int heal;
    Animator animator;
    float time = 0;
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (tag == "Player 1 Skill")
        {
            if (collision.tag == "Player 1")
            {
                time = 0;
            }
        }
        else if (tag == "Player 2 Skill")
        {
            if (collision.tag == "Player 2")
            {
                time = 0;
            }
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        time += Time.deltaTime;
        if(time > 1)
        {
            if (tag == "Player 1 Skill")
            {
                if (collision.tag == "Player 1")
                {
                    StartCoroutine(collision.GetComponent<CharacterController>().HealthSkill(100,1));
                }
            }
            else if (tag == "Player 2 Skill")
            {
                if (collision.tag == "Player 2")
                {
                    StartCoroutine(collision.GetComponent<CharacterController>().HealthSkill(100, 1));
                }
            }
            time = 0;
        }
        
    }
}
