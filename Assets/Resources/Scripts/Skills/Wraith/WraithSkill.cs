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
    [SerializeField] private GameObject powEffect;
    [SerializeField] private GameObject powTauntEffect;

    public GameObject skill1;
    public GameObject skill2;

    float damageScale = 1;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        canUseSkill1 = true;
        canUseSkill2 = true;
        canUseUlti = true;
        if (tag == "Player 1")
        {
            skill1 = GameObject.FindGameObjectWithTag("Skill 1 P1");
            skill2 = GameObject.FindGameObjectWithTag("Skill 2 P1");
        }
        else if (tag == "Player 2")
        {
            skill1 = GameObject.FindGameObjectWithTag("Skill 1 P2");
            skill2 = GameObject.FindGameObjectWithTag("Skill 2 P2");
        }

        damageScale = GetComponent<CharacterController>().dameSkill;
    }

    // Update is called once per frame
    void Update()
    {
        if (tag == "Player 1")
        {
            if ((Input.GetButtonDown("Fire1")) && GetComponent<CharacterController>().isAttacking)
            {
                Attack();
            }
        }
        if (tag == "Player 2")
        {
            if ((Input.GetButtonDown("Fire2")) && GetComponent<CharacterController>().isAttacking)
            {
                Attack();
            }
        }

        if (GetComponent<CharacterController>().canUseSkill)
        {
            if (tag == "Player 1")
            {
                enemy = GameObject.FindGameObjectWithTag("Player 2");

                if (Input.GetButtonDown("Cast 1 P1"))
                {
                    if (canUseSkill1)
                    {
                        animator.SetTrigger("isCasting1");
                        StartCoroutine(Skill1(10f));
                    }
                    else
                    {
                        GetComponent<CharacterController>().Show("On cooldown", Color.cyan);
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
                        GetComponent<CharacterController>().Show("On cooldown", Color.cyan);
                    }

                }

                if (Input.GetButtonDown("Cast 3 P1"))
                {
                    if (canUseUlti && GetComponent<CharacterController>().currentPower == 100)
                    {
                        animator.SetTrigger("isCasting3");
                        StartCoroutine(Ultimate());
                    }
                    else
                    {
                        GetComponent<CharacterController>().Show("Can't use now", Color.grey);
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
                        StartCoroutine(Skill1(10f));
                    }
                    else
                    {
                        GetComponent<CharacterController>().Show("On cooldown", Color.cyan);
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
                        GetComponent<CharacterController>().Show("On cooldown", Color.cyan);
                    }

                }

                if (Input.GetButtonDown("Cast 3 P2"))
                {
                    if (canUseUlti && GetComponent<CharacterController>().currentPower == 100)
                    {
                        animator.SetTrigger("isCasting3");
                        StartCoroutine(Ultimate());
                    }
                    else
                    {
                        GetComponent<CharacterController>().Show("Can't use now", Color.grey);
                    }
                }
            }
        }       
    }

    private void Attack()
    {
        GameObject gObject;
        orbPrefab.tag = tag + " Skill";
        if (GetComponent<CharacterController>().m_FacingRight)
        {
            gObject = Instantiate(orbPrefab, new Vector3(GetComponent<CharacterController>().attackPoint.position.x, GetComponent<CharacterController>().attackPoint.position.y, -2), transform.rotation);
            Destroy(gObject, .5f);
        }
        else
        {
            gObject = Instantiate(orbPrefab, new Vector3(GetComponent<CharacterController>().attackPoint.position.x, GetComponent<CharacterController>().attackPoint.position.y, -2), Quaternion.Euler(180, 0, 180));
            Destroy(gObject, .5f);
        }
        gObject.GetComponent<SharpOrb>().buff = buffOrb; 
        switch (GetComponent<CharacterController>().count)
        {
            case 3:
                gObject.GetComponent<SharpOrb>().dmg = (int)(GetComponent<CharacterController>().attackDmg * (1.3 / 3) * damageScale);
                gObject.transform.localScale = Vector3.one; 
                break;
            case 6:
                gObject.GetComponent<SharpOrb>().dmg = (int)(GetComponent<CharacterController>().attackDmg * (2 / 3) * damageScale);
                gObject.transform.localScale = new Vector3(1.25f,1.25f,1.25f);
                break;
            case 7:
                gObject.GetComponent<SharpOrb>().dmg = (int)(GetComponent<CharacterController>().attackDmg * damageScale);
                gObject.transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
                break;
            default:
                gObject.GetComponent<SharpOrb>().dmg = (int)(GetComponent<CharacterController>().attackDmg * (1 / 3) * damageScale);
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
        float tmp = GetComponent<CharacterController>().attackSpeed;
        GetComponent<CharacterController>().IncreaseATKSpeed((int)(tmp * damageScale));

        yield return new WaitForSeconds(5f);
        buffOrb = false;
        GetComponent<CharacterController>().DecreaseATKSpeed((int)(tmp * damageScale));

        skill1.GetComponent<SkillCooldown>().UseSkill(cooldown);
        yield return new WaitForSeconds(cooldown);        
        canUseSkill1 = true;
    }


    private IEnumerator Skill2(float cooldown)
    {
        canUseSkill2 = false;
        yield return new WaitForSeconds(0.5f);

        float tmp = enemy.GetComponent<Rigidbody2D>().drag;
        slowZonePrefab.tag = tag + " Skill";
        GameObject gObject = Instantiate(slowZonePrefab, new Vector3(enemy.GetComponent<CharacterController>().m_GroundCheck.position.x, -5.9f, -2), Quaternion.Euler(0, 0, 0));
        Destroy(gObject,5f);

        yield return new WaitForSeconds(5f);
        enemy.GetComponent<Rigidbody2D>().drag = tmp;

        skill2.GetComponent<SkillCooldown>().UseSkill(cooldown);
        yield return new WaitForSeconds(cooldown);
        canUseSkill2 = true;
    }


    private IEnumerator Ultimate()
    {
        canUseUlti = false;
        GetComponent<CharacterController>().canNotAttack(.5f);
        GetComponent<CharacterController>().LoadEffect(powTauntEffect, new Vector3(GetComponent<CharacterController>().m_GroundCheck.position.x, GetComponent<CharacterController>().m_GroundCheck.position.y, -2), Quaternion.Euler(0, 0, 0), 1f, new Vector3(2, 2, 2)); GetComponent<CharacterController>().LoadEffect(powEffect, new Vector3(transform.position.x, GetComponent<CharacterController>().m_CeilingCheck.position.y, -2), Quaternion.Euler(-90, 0, 0), 1f, new Vector3(4, 4, 4));
        yield return new WaitForSeconds(0.5f);

        float x = GetComponent<CharacterController>().attackPoint.position.x;
        bool checkFacing = GetComponent<CharacterController>().m_FacingRight;
        lightningStrikePrefab.tag = tag + " Skill";
        for (int i = 0; i < 10; i++)
        {
            if (checkFacing)
            {
                GameObject gObject = Instantiate(lightningStrikePrefab, new Vector3(x + i * 1.5f, -5.9f, -2), transform.rotation);
                gObject.GetComponent<LightningStrike>().damage = (int)(350 * damageScale);
                Destroy(gObject, .5f);
            }
            else
            {
                GameObject gObject = Instantiate(lightningStrikePrefab, new Vector3(x - i * 1.5f, -5.9f, -2), Quaternion.Euler(180, 0, 180));
                gObject.GetComponent<LightningStrike>().damage = (int)(350 * damageScale);
                Destroy(gObject, 0.5f);
            }
            yield return new WaitForSeconds(0.3f);
        }
        GetComponent<CharacterController>().currentPower = 0;
        canUseUlti = true;
    }
}
