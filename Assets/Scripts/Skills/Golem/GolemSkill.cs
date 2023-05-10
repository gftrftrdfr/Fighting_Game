using Spriter2UnityDX;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class GolemSkill : MonoBehaviour
{
    GameObject enemy;
    private bool canUseSkill1;
    private bool canUseSkill2;
    private bool canUseUlti;

    private Animator animator;
    private Rigidbody2D rb;
    public GameObject rockPrefab;

    [SerializeField] private GameObject shieldEffect;
    [SerializeField] private GameObject ammorBuffEffect;
    [SerializeField] private GameObject showEffect;
    [SerializeField] private GameObject ultiEffect;
    [SerializeField] private GameObject powEffect;
    [SerializeField] private GameObject explosionEffect;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        canUseSkill1 = true;
        canUseSkill2 = true;
        canUseUlti = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (GetComponent<CharacterMovement>().canUseSkill)
        {
            if (tag == "Player 1")
            {
                enemy = GameObject.FindGameObjectWithTag("Player 2");
                if (Input.GetButtonDown("Cast 1 P1"))
                {
                    if (canUseSkill1)
                    {
                        animator.SetTrigger("isCasting1");
                        StartCoroutine(Skill1(5f));
                    }
                    else
                    {
                        GetComponent<CharacterMovement>().Show("On cooldown", Color.cyan);
                    }

                }

                if (Input.GetButtonDown("Cast 2 P1"))
                {
                    if (canUseSkill2)
                    {
                        animator.SetTrigger("isCasting2");
                        StartCoroutine(Skill2(7f));
                    }
                    else
                    {
                        GetComponent<CharacterMovement>().Show("On cooldown", Color.cyan);
                    }
                }

                if (Input.GetButtonDown("Cast 3 P1"))
                {
                    if (canUseUlti && GetComponent<CharacterMovement>().currentPower == 100)
                    {
                        animator.SetTrigger("isCasting3");
                        StartCoroutine(Ultimate());
                        GetComponent<CharacterMovement>().currentPower = 0;
                        canUseUlti = true;
                    }
                    else
                    {
                        GetComponent<CharacterMovement>().Show("Can't use now", Color.grey);
                    }
                }
            }
            else if (tag == "Player 2")
            {
                enemy = GameObject.FindGameObjectWithTag("Player 1");
                if (Input.GetButtonDown("Cast 1 P2"))
                {
                    if (canUseSkill1)
                    {
                        animator.SetTrigger("isCasting1");
                        StartCoroutine(Skill1(5f));
                    }
                    else
                    {
                        GetComponent<CharacterMovement>().Show("On cooldown", Color.cyan);
                    }
                }

                if (Input.GetButtonDown("Cast 2 P2"))
                {
                    if (canUseSkill2)
                    {
                        animator.SetTrigger("isCasting2");
                        StartCoroutine(Skill2(7f));
                    }
                    else
                    {
                        GetComponent<CharacterMovement>().Show("On cooldown", Color.cyan);
                    }
                }

                if (Input.GetButtonDown("Cast 3 P2"))
                {
                    if (canUseUlti && GetComponent<CharacterMovement>().currentPower == 100)
                    {
                        animator.SetTrigger("isCasting3");
                        StartCoroutine(Ultimate());
                        GetComponent<CharacterMovement>().currentPower = 0;
                        canUseUlti = true;
                    }
                    else
                    {
                        GetComponent<CharacterMovement>().Show("Can't use now", Color.grey);
                    }
                }
            }
        }
    }

    private IEnumerator Skill1(float cooldown)
    {
        canUseSkill1 = false;

        GetComponent<CharacterMovement>().LoadEffect(ammorBuffEffect, new Vector3(GetComponent<CharacterMovement>().m_GroundCheck.position.x, (GetComponent<CharacterMovement>().m_GroundCheck.position.y + GetComponent<CharacterMovement>().m_CeilingCheck.position.y) / 2, -3), Quaternion.Euler(-90, 0, 0), 0.5f, new Vector2(2f,2f));
        
        yield return new WaitForSeconds(0.5f);

        GetComponent<EntityRenderer>().Color = new Color(0.4292453f, 1, 0.785162f);
        int tmp = GetComponent<CharacterMovement>().armor;
        GetComponent<CharacterMovement>().IncreaseArmor("30");
        GetComponent<CharacterMovement>().armor = tmp + 30;

        GetComponent<CharacterMovement>().canReflect = true;
        yield return new WaitForSeconds(5f);
        
        GetComponent<CharacterMovement>().armor = tmp;
        GetComponent<EntityRenderer>().Color = Color.white;

        yield return new WaitForSeconds(cooldown);
        canUseSkill1 = true;
    }


    private IEnumerator Skill2(float cooldown)
    {
        canUseSkill2 = false;
        GetComponent<CharacterMovement>().canMove = false;
        GetComponent<CharacterMovement>().LoadEffect(shieldEffect, new Vector3(GetComponent<CharacterMovement>().m_GroundCheck.position.x, (GetComponent<CharacterMovement>().m_GroundCheck.position.y + GetComponent<CharacterMovement>().m_CeilingCheck.position.y) / 2, -3), Quaternion.Euler(-90, 0, 0), 0.7f, new Vector3(3f, 3f, 3));


        yield return new WaitForSeconds(0.7f);
        GetComponent<CharacterMovement>().LoadEffect(showEffect, new Vector3(GetComponent<CharacterMovement>().attackPoint.position.x, GetComponent<CharacterMovement>().m_GroundCheck.position.y, -2), Quaternion.Euler(-90, 0, 0), 3f, new Vector3(1.5f, 1.5f, 1));
        GetComponent<CharacterMovement>().canMove = true;

        rockPrefab.tag = tag;
        if (GetComponent<CharacterMovement>().m_FacingRight)
        {
            GameObject gObject = Instantiate(rockPrefab, new Vector3(GetComponent<CharacterMovement>().m_GroundCheck.position.x, GetComponent<CharacterMovement>().m_GroundCheck.position.y - 0.8f , 2), transform.rotation);
            gObject.GetComponent<Rigidbody2D>().velocity = new Vector2(15.0f, 0.0f);
            Destroy(gObject, 8f);

        }
        else
        {
            GameObject gObject = Instantiate(rockPrefab, new Vector3(GetComponent<CharacterMovement>().m_GroundCheck.position.x, GetComponent<CharacterMovement>().m_GroundCheck.position.y - 0.8f, 2), Quaternion.Euler(new Vector3(0f,180f,0f)));
            gObject.GetComponent<Rigidbody2D>().velocity = new Vector2(-15.0f, 0.0f);
            Destroy(gObject, 8f);
        }

        yield return new WaitForSeconds(cooldown);
        canUseSkill2 = true;
    }

    private IEnumerator Ultimate()
    {
        canUseUlti = false;

        GetComponent<CharacterMovement>().LoadEffect(powEffect, new Vector3(transform.position.x, GetComponent<CharacterMovement>().m_CeilingCheck.position.y, -2), Quaternion.Euler(-90, 0, 0), 1f, new Vector3(4, 4, 4));
        if (GetComponent<CharacterMovement>().isGrounded)
        {
            GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, 10500f));
            yield return new WaitForSeconds(0.5f);            
        }
        GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, -20000f));
        yield return new WaitForSeconds(0.3f);
        GetComponent<CharacterMovement>().LoadEffect(ultiEffect, new Vector3(GetComponent<CharacterMovement>().m_GroundCheck.position.x, GetComponent<CharacterMovement>().m_GroundCheck.position.y, -2), Quaternion.Euler(-90, 0, 0), 3f, new Vector3(4f, 4f, 4f));
        GetComponent<CharacterMovement>().LoadEffect(explosionEffect, new Vector3(GetComponent<CharacterMovement>().m_GroundCheck.position.x, GetComponent<CharacterMovement>().m_GroundCheck.position.y, -2), Quaternion.Euler(0, 0, 0), 3f, new Vector3(3f, 3f, 3f));
        Collider2D[] hits = Physics2D.OverlapCircleAll(GetComponent<CharacterMovement>().m_GroundCheck.position, 7f, GetComponent<CharacterMovement>().enemyLayers);
        foreach (Collider2D hit in hits)
        {
            Vector2 newVector = GetComponent<CharacterMovement>().m_GroundCheck.position - hit.GetComponent<CharacterMovement>().m_GroundCheck.position;
            if (newVector.x >= 0)
            {
                hit.GetComponent<CharacterMovement>().TakeDamage(550);
                hit.GetComponent<CharacterMovement>().Stun(4f, -1500f, 8000f);
            }
            else
            {
                hit.GetComponent<CharacterMovement>().TakeDamage(550);
                hit.GetComponent<CharacterMovement>().Stun(4f, 1500f, 8000f);
            }
        }
    }
}
