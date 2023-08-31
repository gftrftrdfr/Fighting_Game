using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class RockController : MonoBehaviour
{
    private bool check = false;
    private bool check2 = true;
    private bool check3 = true;
    private bool canDame = true;
    float time = 0;
    float time2 = 0;

    [SerializeField] private GameObject showEffect;
    [SerializeField] private GameObject runEffect;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        time2 += Time.deltaTime;
        
        if ((time2 > 0.1f)&&check3)
        {           
            GameObject obj = Instantiate(runEffect, new Vector3(transform.position.x, transform.position.y + 1, -2), Quaternion.Euler(-90, 0, 0));
            obj.transform.localScale = new Vector3(1f, 1f, 1f);
            Destroy(obj, 2f);
            time2 = 0;
        }
        
        if ((check) || (time > 1))
        {
            check3 = false;
            if (check2)
            {
                check2 = false;              
                GameObject obj1 = Instantiate(showEffect, new Vector3(transform.position.x, transform.position.y - 1, -2), Quaternion.Euler(-90, 0, 0));
                obj1.transform.localScale = new Vector3(1f, 1f, 1f);
                obj1.transform.parent = transform;
                Destroy(obj1, 2f);
            }
            check = false;
            GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
            GetComponent<Rigidbody2D>().MovePosition(new Vector3(0, -4.8f, 0));
            canDame = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(canDame) 
        {
            if (tag == "Player 1 Skill")
            {
                if (collision.gameObject.tag == "Player 2")
                {
                    collision.gameObject.GetComponent<CharacterController>().TakeDamage(150);
                    collision.gameObject.GetComponent<CharacterController>().Stun(1f, 0, collision.gameObject.GetComponent<Rigidbody2D>().mass * 100);
                    check = true;
                    canDame = false;
                }
            }
            else if (tag == "Player 2 Skill")
            {
                if (collision.gameObject.tag == "Player 1")
                {
                    collision.gameObject.GetComponent<CharacterController>().TakeDamage(150);
                    collision.gameObject.GetComponent<CharacterController>().Stun(1f, 0, collision.gameObject.GetComponent<Rigidbody2D>().mass * 100);
                    check = true;
                    canDame = false;
                }
            }
            if (collision.gameObject.tag == "LimitMap")
            {
                check = true;
                canDame = false;
            }
        }
              
    }

}
