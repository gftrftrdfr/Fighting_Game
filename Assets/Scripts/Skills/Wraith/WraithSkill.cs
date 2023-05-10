using System;
using System.Collections;
using System.Collections.Generic;
using System.Resources;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;
using Random = UnityEngine.Random;

public class WraithSkill : MonoBehaviour
{

    GameObject enemy;
    private bool canUseSkill1;
    private bool canUseSkill2;
    private bool canUseUlti;
    private bool buffOrb = false;

    private Animator animator;
    private Rigidbody2D rb;
    public GameObject orbPrefab;
    public GameObject slowZonePrefab;
    public GameObject lightningStrikePrefab;

    public Transform leftHand;
    public Transform rightHand;

    [SerializeField] private GameObject buffEffect;
    [SerializeField] private GameObject atkBuffEffect;
    [SerializeField] private GameObject powEffect;

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
        if (tag == "Player 1")
        {
            if ((Input.GetButtonDown("Fire1")) && GetComponent<CharacterMovement>().isAttacking)
            {
                Attack();
            }
        }
        if (tag == "Player 2")
        {
            if ((Input.GetButtonDown("Fire2")) && GetComponent<CharacterMovement>().isAttacking)
            {
                Attack();
            }
        }

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

    private void Attack()
    {
        GameObject gObject;
        orbPrefab.tag = tag;
        if (GetComponent<CharacterMovement>().m_FacingRight)
        {
            gObject = Instantiate(orbPrefab, new Vector3(GetComponent<CharacterMovement>().attackPoint.position.x, GetComponent<CharacterMovement>().attackPoint.position.y, -2), transform.rotation);
            Destroy(gObject, .5f);
        }
        else
        {
            gObject = Instantiate(orbPrefab, new Vector3(GetComponent<CharacterMovement>().attackPoint.position.x, GetComponent<CharacterMovement>().attackPoint.position.y, -2), Quaternion.Euler(180, 0, 180));
            Destroy(gObject, .5f);
        }
        gObject.GetComponent<SharpOrb>().buff = buffOrb; 
        switch (GetComponent<CharacterMovement>().count)
        {
            case 3:
                gObject.GetComponent<SharpOrb>().dmg = (int)(GetComponent<CharacterMovement>().attackDmg * 1.3 / 3);
                gObject.transform.localScale = Vector3.one; 
                break;
            case 6:
                gObject.GetComponent<SharpOrb>().dmg = (int)(GetComponent<CharacterMovement>().attackDmg * 2 / 3);
                gObject.transform.localScale = new Vector3(1.25f,1.25f,1.25f);
                break;
            case 7:
                gObject.GetComponent<SharpOrb>().dmg = (int)(GetComponent<CharacterMovement>().attackDmg);
                gObject.transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
                break;
            default:
                gObject.GetComponent<SharpOrb>().dmg = (int)(GetComponent<CharacterMovement>().attackDmg * 1 / 3);
                gObject.transform.localScale = new Vector3(.75f, .75f, .75f);
                break;
        }
    }

    private IEnumerator Skill1(float cooldown)
    {
        canUseSkill1 = false;

        //GameObject effect = Instantiate(atkBuffEffect, GetComponent<CharacterMovement>().m_GroundCheck);
        //effect.transform.localScale = new Vector3(4, 2, 1);
        //Destroy(effect, 1f);
        yield return new WaitForSeconds(0.5f);

        GameObject leftEffect = Instantiate(buffEffect, new Vector3(leftHand.position.x, leftHand.position.y, 2), Quaternion.Euler(0,0,0));
        leftEffect.transform.parent = leftHand;
        Destroy(leftEffect, 5f);
        GameObject rightEffect = Instantiate(buffEffect, new Vector3(rightHand.position.x, rightHand.position.y, - 2), Quaternion.Euler(0, 0, 0));
        rightEffect.transform.parent = rightHand;
        Destroy(rightEffect, 5f);

        buffOrb = true;
        float tmp = GetComponent<CharacterMovement>().attackSpeed;
        GetComponent<CharacterMovement>().IncreaseATKSpeed(tmp.ToString());
        GetComponent<CharacterMovement>().attackSpeed = tmp * 2;

        yield return new WaitForSeconds(5f);
        buffOrb = false;
        enemy.GetComponent<CharacterMovement>().isBleeding = false;
        GetComponent<CharacterMovement>().attackSpeed = tmp;
        yield return new WaitForSeconds(cooldown);        
        canUseSkill1 = true;
    }


    private IEnumerator Skill2(float cooldown)
    {
        canUseSkill2 = false;
        yield return new WaitForSeconds(0.5f);

        float tmp = enemy.GetComponent<Rigidbody2D>().drag;
        slowZonePrefab.tag = tag;
        GameObject gObject = Instantiate(slowZonePrefab, new Vector3(enemy.GetComponent<CharacterMovement>().m_GroundCheck.position.x, -5.9f, -2), Quaternion.Euler(0, 0, 0));
        Destroy(gObject,5f);

        yield return new WaitForSeconds(5f);
        enemy.GetComponent<Rigidbody2D>().drag = tmp;

        yield return new WaitForSeconds(cooldown);
        canUseSkill2 = true;
    }


    private IEnumerator Ultimate()
    {
        canUseUlti = false;
        GetComponent<CharacterMovement>().canMove = false;
        GetComponent<CharacterMovement>().LoadEffect(powEffect, new Vector3(transform.position.x, GetComponent<CharacterMovement>().m_CeilingCheck.position.y, -2), Quaternion.Euler(-90, 0, 0), 1f, new Vector3(4, 4, 4));
        yield return new WaitForSeconds(0.5f);

        GetComponent<CharacterMovement>().canMove = true;
        float x = GetComponent<CharacterMovement>().attackPoint.position.x;
        bool checkFacing = GetComponent<CharacterMovement>().m_FacingRight;
        lightningStrikePrefab.tag = tag;
        for (int i = 0; i < 10; i++)
        {
            if (checkFacing)
            {
                GameObject gObject = Instantiate(lightningStrikePrefab, new Vector3(x + i * 1.5f, -5.9f, -2), transform.rotation);
                Destroy(gObject, .5f);
            }
            else
            {
                GameObject gObject = Instantiate(lightningStrikePrefab, new Vector3(x - i * 1.5f, -5.9f, -2), Quaternion.Euler(180, 0, 180));
                Destroy(gObject, 0.5f);
            }
            yield return new WaitForSeconds(0.3f);
        }
        canUseUlti = true;
    }
}
