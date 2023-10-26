using System;
using System.Collections;
using System.Collections.Generic;
using System.Resources;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;
using Random = UnityEngine.Random;

public class FASkill : MonoBehaviour
{
    GameObject enemy;
    private bool canUseSkill1;
    private bool canUseSkill2;
    private bool canUseUlti;
    private bool isUsingSkill2 = false;

    private Animator animator;
    private Rigidbody2D rb;
    public GameObject swordPrefab;

    public Transform leftHand;
    public Transform rightHand;

    [SerializeField] private GameObject dashEffect;
    [SerializeField] private GameObject slashEffect;
    [SerializeField] private GameObject portEffect;
    [SerializeField] private GameObject buffEffect;
    [SerializeField] private GameObject atkBuffEffect;
    [SerializeField] private GameObject powSlashEffect;
    [SerializeField] private GameObject powEffect;

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

    private IEnumerator Skill1(float cooldown)
    {
        GameObject effect = Instantiate(atkBuffEffect, GetComponent<CharacterController>().m_GroundCheck);
        effect.transform.localScale = new Vector3(4, 2, 1);
        Destroy(effect, 1f);

        canUseSkill1 = false;

        GameObject leftEffect = Instantiate(buffEffect, new Vector3(leftHand.position.x, leftHand.position.y, 2), Quaternion.Euler(0,0,0));
        leftEffect.transform.parent = leftHand;
        Destroy(leftEffect, 5f);
        GameObject rightEffect = Instantiate(buffEffect, new Vector3(rightHand.position.x, rightHand.position.y, - 2), Quaternion.Euler(0, 0, 0));
        rightEffect.transform.parent = rightHand;
        Destroy(rightEffect, 5f);

        float tmp = GetComponent<CharacterController>().dashCooldown;
        GetComponent<CharacterController>().IncreaseATK((int)(10 * damageScale));
        GetComponent<CharacterController>().dashCooldown = 3f;
        enemy.GetComponent<CharacterController>().isBleeding = true ;

        yield return new WaitForSeconds(5f);
        enemy.GetComponent<CharacterController>().isBleeding = false;
        GetComponent<CharacterController>().DecreaseATK((int)(10 * damageScale));
        GetComponent<CharacterController>().dashCooldown = tmp;

        skill1.GetComponent<SkillCooldown>().UseSkill(cooldown);
        yield return new WaitForSeconds(cooldown);        
        canUseSkill1 = true;
    }


    private IEnumerator Skill2(float cooldown)
    {
        canUseSkill2 = false;
        isUsingSkill2 = true;
        GameObject obj = Instantiate(dashEffect, new Vector3(GetComponent<CharacterController>().m_GroundCheck.position.x, GetComponent<CharacterController>().m_GroundCheck.position.y, -1), Quaternion.Euler(-90, 0, 0));
        obj.transform.localScale = new Vector2(3f, 2f);
        Destroy(obj, 1f);
        float originalGravity = rb.gravityScale;
        rb.gravityScale = 0f;
        
        if (GetComponent<CharacterController>().isGrounded)
        { 
            rb.velocity = new Vector2(transform.localScale.x * 600, 0f); 
        }
        else
        {
            rb.velocity = new Vector2(transform.localScale.x * 30, 0f);
        }
        
        yield return new WaitForSeconds(0.3f);
        GetComponent<CharacterController>().ManySlashEffect();
        isUsingSkill2 = false;
        rb.gravityScale = originalGravity;

        skill2.GetComponent<SkillCooldown>().UseSkill(cooldown);
        yield return new WaitForSeconds(cooldown);
        canUseSkill2 = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(isUsingSkill2)
        {
            if(collision = enemy.GetComponent<Collider2D>())
            {
                enemy.GetComponent<CharacterController>().TakeDamage((int)(150 * damageScale));
                isUsingSkill2 = false;
            }
        }
    }

    private IEnumerator Ultimate()
    {
        canUseUlti = false;
        GetComponent<CharacterController>().canNotMove(1f);
        GetComponent<CharacterController>().LoadEffect(powSlashEffect, new Vector3(transform.position.x, GetComponent<CharacterController>().m_CeilingCheck.position.y, -2), Quaternion.Euler(-90, 0, 0), 1f, new Vector3(4, 4, 4));
        GetComponent<CharacterController>().LoadEffect(powEffect, new Vector3(GetComponent<CharacterController>().m_GroundCheck.position.x, GetComponent<CharacterController>().m_GroundCheck.position.y, -2), Quaternion.Euler(0, 0, 0), 1f, new Vector3(2, 2, 2));

        for (int i = 0; i < 5; i++)
        {
            GetComponent<CharacterController>().LoadEffect(slashEffect, new Vector3(GetComponent<CharacterController>().attackPoint.transform.position.x + Random.Range(-1, 1), GetComponent<CharacterController>().attackPoint.position.y + Random.Range(-1, 1), -2), Quaternion.Euler(Random.Range(-180, 180), Random.Range(-180, 180), Random.Range(-180, 180)), 1f, new Vector3(1.5f, 1.5f, 1.5f));
        }

        GetComponent<CharacterController>().LoadEffect(portEffect, GetComponent<CharacterController>().attackPoint.position, Quaternion.Euler(0, 15, 90), 3f, new Vector3(2, 1, 1));
        yield return new WaitForSeconds(1f);

        swordPrefab.tag = tag + " Skill";      
        for (int i = 0; i < 15;i++) 
        {
            if (GetComponent<CharacterController>().m_FacingRight)
            {
                GameObject gObject = Instantiate(swordPrefab, new Vector3(GetComponent<CharacterController>().attackPoint.position.x, GetComponent<CharacterController>().attackPoint.position.y + Random.Range(-1f,1f), -2), transform.rotation);
                gObject.GetComponent<Sword>().damage = (int)(40 * damageScale);
                Destroy(gObject,2f);
            }
            else
            {
                GameObject gObject = Instantiate(swordPrefab, new Vector3(GetComponent<CharacterController>().attackPoint.position.x, GetComponent<CharacterController>().attackPoint.position.y + Random.Range(-1f, 1f), -2), Quaternion.Euler(180, 0, 180));
                gObject.GetComponent<Sword>().damage = (int)(40 * damageScale);
                Destroy(gObject, 2f);
            }
            yield return new WaitForSeconds(0.1f);
        }
        GetComponent<CharacterController>().currentPower = 0;
        canUseUlti = true;
    }

}
