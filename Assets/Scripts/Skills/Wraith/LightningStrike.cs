using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
using UnityEngine.UIElements;

public class LightningStrike : MonoBehaviour
{
    [SerializeField] private GameObject lightningEffect;

    public Transform centerPoint;

    // Start is called before the first frame update
    void Start()
    {
        GameObject gObject = Instantiate(lightningEffect, centerPoint.position, Quaternion.Euler(-90,0,0));
        gObject.transform.localScale = new Vector3(4,4,4);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (this.tag == "Player 1")
        {
            if (collision.tag == "Player 2")
            {
                collision.GetComponent<CharacterMovement>().TakeTrueDamage(200);
            }
        }
        else if (this.tag == "Player 2")
        {
            if (collision.tag == "Player 1")
            {
                collision.GetComponent<CharacterMovement>().TakeTrueDamage(200);
            }
        }
    }
}
