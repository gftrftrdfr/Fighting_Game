using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
using UnityEngine.UIElements;

public class LightningStrike : MonoBehaviour
{
    [SerializeField] private GameObject fireEffect;
    [SerializeField] private GameObject smokeEffect;

    public Transform centerPoint;

    // Start is called before the first frame update
    void Start()
    {
        GameObject gObject = Instantiate(fireEffect, centerPoint.position, Quaternion.Euler(-90,0,0));
        gObject.transform.localScale = new Vector3(5, 1, 1);
        GameObject gObject2 = Instantiate(smokeEffect, centerPoint.position, Quaternion.Euler(-90, 0, 0));       
        Destroy(gObject,0.3f);       
        Destroy(gObject2, 2f);
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
                collision.GetComponent<CharacterController>().TakeTrueDamage(350);
            }
        }
        else if (this.tag == "Player 2 Skill")
        {
            if (collision.tag == "Player 1")
            {
                collision.GetComponent<CharacterController>().TakeTrueDamage(350);
            }
        }
    }
}
