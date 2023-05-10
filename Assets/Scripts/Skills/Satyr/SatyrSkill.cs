using System;
using System.Collections;
using System.Collections.Generic;
using System.Resources;
using Unity.Burst.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;
using Random = UnityEngine.Random;

public class SatyrSkill : MonoBehaviour
{

    GameObject enemy;
    private bool canUseSkill1;
    private bool canUseSkill2;
    private bool canUseUlti;
    private bool isUsingSkill2 = false;

    private Animator animator;
    private Rigidbody2D rb;
    public GameObject treePrefab;

    public Transform leftHand;
    public Transform rightHand;

    [SerializeField] private GameObject musicEffect;
    [SerializeField] private GameObject showEffect;
    [SerializeField] private GameObject handEffect;
    [SerializeField] private GameObject powEffect;

    Collider2D[] hits;
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
                    if (canUseUlti) //&& GetComponent<CharacterMovement>().currentPower == 100)
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

    private void FixedUpdate()
    {
        if(isUsingSkill2)
        {
            foreach (Collider2D hit in hits)
            {
                if(hit.GetComponent<CharacterMovement>().canStun)
                {
                    hit.GetComponent<CharacterMovement>().canMove = false;
                    hit.GetComponent<CharacterMovement>().canDash = false;
                    hit.GetComponent<CharacterMovement>().canUseSkill = false;
                    hit.GetComponent<CharacterMovement>().canDefen = false;
                    hit.GetComponent<CharacterMovement>().canAttack = false;

                    Vector2 newVector = GetComponent<CharacterMovement>().m_GroundCheck.position - hit.GetComponent<CharacterMovement>().m_GroundCheck.position;
                    if (newVector.x < 0)
                    {
                        hit.GetComponent<CharacterController2D>().Move(-20 * Time.fixedDeltaTime, false, false);
                        hit.GetComponent<Animator>().SetFloat("walkSpeed", 20); ;
                    }
                    else
                    {
                        hit.GetComponent<CharacterController2D>().Move(20 * Time.fixedDeltaTime, false, false);
                        hit.GetComponent<Animator>().SetFloat("walkSpeed", 20);
                    }
                }
            }
        }
    }

    private IEnumerator Skill1(float cooldown)
    {
        canUseSkill1 = false;
        GetComponent<CharacterMovement>().canMove = false;
        GetComponent<CharacterMovement>().LoadEffect(musicEffect, new Vector3(GetComponent<CharacterMovement>().m_CeilingCheck.position.x, GetComponent<CharacterMovement>().m_CeilingCheck.position.y, -3), Quaternion.Euler(-90, 0, 0), 0.5f, new Vector3(2f, 2f, 2f));

        yield return new WaitForSeconds(1f);

        int atkTemp = 0;
        int armorTemp = 0;
        GetComponent<CharacterMovement>().canMove = true;
        Collider2D[] hits = Physics2D.OverlapCircleAll(new Vector3(GetComponent<CharacterMovement>().m_GroundCheck.position.x + 2, GetComponent<CharacterMovement>().m_GroundCheck.position.y, GetComponent<CharacterMovement>().m_GroundCheck.position.z), 6f, GetComponent<CharacterMovement>().enemyLayers);
        foreach (Collider2D hit in hits)
        {
            int temp1 = (int)(hit.GetComponent<CharacterMovement>().attackDmg * 0.2);
            int temp2 = (int)(hit.GetComponent<CharacterMovement>().armor * 0.2);
            hit.GetComponent<CharacterMovement>().attackDmg -= temp1;
            hit.GetComponent<CharacterMovement>().armor -= temp2;
            atkTemp += temp1;
            armorTemp += temp2;
        }

        GetComponent<CharacterMovement>().attackDmg += atkTemp;
        GetComponent<CharacterMovement>().armor += armorTemp;

        GetComponent<CharacterMovement>().IncreaseATK(atkTemp.ToString());

        yield return new WaitForSeconds(1f);
        GetComponent<CharacterMovement>().IncreaseArmor(armorTemp.ToString());

        yield return new WaitForSeconds(4f);
        foreach (Collider2D hit in hits)
        {
            hit.GetComponent<CharacterMovement>().attackDmg += (int)(hit.GetComponent<CharacterMovement>().attackDmg * 0.2);
            hit.GetComponent<CharacterMovement>().armor += (int)(hit.GetComponent<CharacterMovement>().armor * 0.2);
        }
        GetComponent<CharacterMovement>().attackDmg -= atkTemp;
        GetComponent<CharacterMovement>().armor -= armorTemp;

        yield return new WaitForSeconds(cooldown);
        canUseSkill1 = true;
    }


    private IEnumerator Skill2(float cooldown)
    {
        canUseSkill2 = false;
        GetComponent<CharacterMovement>().canMove = false;
        GetComponent<CharacterMovement>().LoadEffect(musicEffect, new Vector3(GetComponent<CharacterMovement>().m_CeilingCheck.position.x, GetComponent<CharacterMovement>().m_CeilingCheck.position.y, -3), Quaternion.Euler(-90, 0, 0), 0.5f, new Vector3(2f, 2f, 2f));

        yield return new WaitForSeconds(1f);

        GetComponent<CharacterMovement>().canMove = true;
        isUsingSkill2 = true;
        hits = Physics2D.OverlapCircleAll(new Vector3(GetComponent<CharacterMovement>().m_GroundCheck.position.x + 2, GetComponent<CharacterMovement>().m_GroundCheck.position.y, GetComponent<CharacterMovement>().m_GroundCheck.position.z), 6f, GetComponent<CharacterMovement>().enemyLayers);

        yield return new WaitForSeconds(1.5f);
        isUsingSkill2 = false;
        foreach (Collider2D hit in hits)
        {
            hit.GetComponent<CharacterMovement>().canMove = true;
            hit.GetComponent<CharacterMovement>().canDash = true;
            hit.GetComponent<CharacterMovement>().canUseSkill = true;
            hit.GetComponent<CharacterMovement>().canDefen = true;
            hit.GetComponent<CharacterMovement>().canAttack = true;
        }

        yield return new WaitForSeconds(cooldown);
        canUseSkill2 = true;
    }

    private IEnumerator Ultimate()
    {
        canUseUlti = false;
        GetComponent<CharacterMovement>().canMove = false;
        GetComponent<CharacterMovement>().LoadEffect(powEffect, new Vector3(transform.position.x, GetComponent<CharacterMovement>().m_CeilingCheck.position.y, -2), Quaternion.Euler(-90, 0, 0), 1f, new Vector3(4, 4, 4));

        yield return new WaitForSeconds(0.7f);
        //GetComponent<CharacterMovement>().LoadEffect(showEffect, new Vector3(GetComponent<CharacterMovement>().attackPoint.position.x, GetComponent<CharacterMovement>().m_GroundCheck.position.y, -2), Quaternion.Euler(-90, 0, 0), 3f, new Vector3(1.5f, 1.5f, 1));
        GetComponent<CharacterMovement>().canMove = true;

        treePrefab.tag = tag;
        GameObject gObject = Instantiate(treePrefab, new Vector3(GetComponent<CharacterMovement>().m_GroundCheck.position.x, GetComponent<CharacterMovement>().m_GroundCheck.position.y, 0), transform.rotation);
        Destroy(gObject, 5f);

        yield return new WaitForSeconds(1f);
        canUseUlti = true;
        GetComponent<CharacterMovement>().canMove = true;     
    }

}
