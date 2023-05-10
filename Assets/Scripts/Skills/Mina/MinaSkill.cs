using Spriter2UnityDX;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class MinaSkill : MonoBehaviour
{
    GameObject enemy;
    private bool canUseSkill1;
    private bool canUseSkill2;
    private bool canUseUlti;
    private bool usingUlti = false;
    private bool canDamage = true;
    float timeDamage = 0;

    private Animator animator;
    private Rigidbody2D rb;

    public Transform leftHand;
    public Transform rightHand;

    [SerializeField] private GameObject buffEffect;
    [SerializeField] private GameObject handEffect;
    [SerializeField] private GameObject showEffect;
    [SerializeField] private GameObject powEffect;
    [SerializeField] private GameObject explosionEffect;
    [SerializeField] private GameObject runEffect;
    [SerializeField] private GameObject smokeEffect;

    float horizontalMove = 0f;

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
            }
        }
        timeDamage -= Time.deltaTime;
        if(timeDamage < 0)
        {
            timeDamage = 0;
            canDamage = true;
        }

        if (tag == "Player 1")
        {
            enemy = GameObject.FindGameObjectWithTag("Player 2");
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
            if (usingUlti)
            {
                horizontalMove = Input.GetAxisRaw("Horizontal 1") * 150;

                animator.SetFloat("walkSpeed", Mathf.Abs(horizontalMove));
                if (canDamage)
                {
                    Collider2D[] hits = Physics2D.OverlapCircleAll(GetComponent<CharacterMovement>().attackPoint.position, 1f, GetComponent<CharacterMovement>().enemyLayers);
                    foreach (Collider2D hit in hits)
                    {
                        if (this.GetComponent<CharacterMovement>().m_FacingRight)
                        {
                            hit.GetComponent<CharacterMovement>().TakeDamage(120);
                            hit.GetComponent<Rigidbody2D>().AddForce(new Vector2(3000f, 5000f));
                        }
                        else
                        {
                            hit.GetComponent<CharacterMovement>().TakeDamage(120);
                            hit.GetComponent<Rigidbody2D>().AddForce(new Vector2(-3000f, 5000f));
                        }
                        canDamage = false;
                        timeDamage = 1f;
                    }
                }
            }            
        }
        else if (tag == "Player 2")
        {
            enemy = GameObject.FindGameObjectWithTag("Player 1");         
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
            if (usingUlti)
            {
                horizontalMove = Input.GetAxisRaw("Horizontal 2") *150;

                animator.SetFloat("walkSpeed", Mathf.Abs(horizontalMove));
                if (canDamage)
                {
                    Collider2D[] hits = Physics2D.OverlapCircleAll(GetComponent<CharacterMovement>().attackPoint.position, 1f, GetComponent<CharacterMovement>().enemyLayers);
                    foreach (Collider2D hit in hits)
                    {
                        if (this.GetComponent<CharacterMovement>().m_FacingRight)
                        {
                            hit.GetComponent<CharacterMovement>().TakeDamage(120);
                            hit.GetComponent<Rigidbody2D>().AddForce(new Vector2(3000f, 5000f));
                        }
                        else
                        {
                            hit.GetComponent<CharacterMovement>().TakeDamage(120);
                            hit.GetComponent<Rigidbody2D>().AddForce(new Vector2(-3000f, 5000f));
                        }
                        canDamage = false;
                        timeDamage = 1f;
                    }
                }
            }            
        }      
    }

    private void FixedUpdate()
    {
        if (usingUlti)
        {
            GetComponent<CharacterController2D>().Move(horizontalMove * Time.fixedDeltaTime, false, false);
            if(horizontalMove != 0f)
            {
                GameObject obj = Instantiate(runEffect, new Vector3(this.transform.position.x, GetComponent<CharacterMovement>().m_GroundCheck.position.y, -2), Quaternion.Euler(0, 90, 0));
                obj.transform.localScale = new Vector3(0.7f, 0.7f, 0.7f);
                Destroy(obj, 2f);
            }
            if (horizontalMove > 0)
            {
                GetComponent<CharacterMovement>().m_FacingRight = true;
            }
            if (horizontalMove < 0)
            {
                GetComponent<CharacterMovement>().m_FacingRight = false;
            }
        }      
    }

    private IEnumerator Skill1(float cooldown)
    {
        canUseSkill1 = false;

        GetComponent<CharacterMovement>().LoadEffect(buffEffect, new Vector3(GetComponent<CharacterMovement>().m_GroundCheck.position.x, GetComponent<CharacterMovement>().m_GroundCheck.position.y, -3), Quaternion.Euler(-90, 0, 0), 0.5f, new Vector3(2f,2f,2f));
        
        yield return new WaitForSeconds(0.5f);

        GameObject leftEffect = Instantiate(handEffect, new Vector3(leftHand.position.x, leftHand.position.y, 2), Quaternion.Euler(0, 0, 0));
        leftEffect.transform.localScale = new Vector3(2f, 2f, 2f);
        leftEffect.transform.parent = leftHand;
        Destroy(leftEffect, 5f);
        GameObject rightEffect = Instantiate(handEffect, new Vector3(rightHand.position.x, rightHand.position.y, -2), Quaternion.Euler(0, 0, 0));
        rightEffect.transform.localScale = new Vector3(2f, 2f, 2f);
        rightEffect.transform.parent = rightHand;
        Destroy(rightEffect, 5f);

        float tmp1 = GetComponent<CharacterMovement>().attackSpeed;
        float tmp2 = GetComponent<CharacterMovement>().walkSpeed;
        GetComponent<CharacterMovement>().canHealWhenATK = true;

        GetComponent<CharacterMovement>().attackSpeed = tmp1 + 0.5f;
        GetComponent<CharacterMovement>().walkSpeed = tmp2 + 5f;

        GetComponent<CharacterMovement>().IncreaseATKSpeed("0.5");

        yield return new WaitForSeconds(1f);
        GetComponent<CharacterMovement>().IncreaseSpeed("5");

        yield return new WaitForSeconds(4f);

        GetComponent<CharacterMovement>().attackSpeed = tmp1;
        GetComponent<CharacterMovement>().walkSpeed = tmp2;
        GetComponent<CharacterMovement>().canHealWhenATK = false;

        yield return new WaitForSeconds(cooldown);
        canUseSkill1 = true;
    }


    private IEnumerator Skill2(float cooldown)
    {
        canUseSkill2 = false;
        GetComponent<CharacterMovement>().canMove = false;
        GetComponent<CharacterMovement>().LoadEffect(showEffect, new Vector3(GetComponent<CharacterMovement>().m_GroundCheck.position.x, (GetComponent<CharacterMovement>().m_GroundCheck.position.y + GetComponent<CharacterMovement>().m_CeilingCheck.position.y) / 2, -3), Quaternion.Euler(-90, 0, 0), 0.7f, new Vector3(3f, 3f, 3));
        
        yield return new WaitForSeconds(0.5f);

        GetComponent<CharacterMovement>().canMove = true;
        if (GetComponent<CharacterMovement>().m_FacingRight)
        {
            GameObject explsion = Instantiate(explosionEffect, new Vector3(GetComponent<CharacterMovement>().m_GroundCheck.position.x + 2, GetComponent<CharacterMovement>().m_GroundCheck.position.y, -2), Quaternion.Euler(0, 0, 0));
            explsion.transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
            Collider2D[] hits = Physics2D.OverlapCircleAll(new Vector3(GetComponent<CharacterMovement>().m_GroundCheck.position.x + 2, GetComponent<CharacterMovement>().m_GroundCheck.position.y, GetComponent<CharacterMovement>().m_GroundCheck.position.z), 3f, GetComponent<CharacterMovement>().enemyLayers);
            foreach (Collider2D hit in hits)
            {
                if (!GetComponent<CharacterMovement>().m_FacingRight)
                {
                    hit.GetComponent<CharacterMovement>().TakeDamage(350);
                    hit.GetComponent<CharacterMovement>().Stun(2f, -1000f, 6000f);
                }
                else
                {
                    hit.GetComponent<CharacterMovement>().TakeDamage(350);
                    hit.GetComponent<CharacterMovement>().Stun(2f, 1000f, 6000f);
                }
            }
        }
        else
        {
            GameObject explsion = Instantiate(explosionEffect, new Vector3(GetComponent<CharacterMovement>().m_GroundCheck.position.x - 2, GetComponent<CharacterMovement>().m_GroundCheck.position.y, -2), Quaternion.Euler(0, 0, 0));
            explsion.transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
            Collider2D[] hits = Physics2D.OverlapCircleAll(new Vector3(GetComponent<CharacterMovement>().m_GroundCheck.position.x - 2, GetComponent<CharacterMovement>().m_GroundCheck.position.y, GetComponent<CharacterMovement>().m_GroundCheck.position.z), 3f, GetComponent<CharacterMovement>().enemyLayers);
            foreach (Collider2D hit in hits)
            {
                if (!GetComponent<CharacterMovement>().m_FacingRight)
                {
                    hit.GetComponent<CharacterMovement>().TakeDamage(350);
                    hit.GetComponent<CharacterMovement>().Stun(2f, -1000f, 6000f);
                }
                else
                {
                    hit.GetComponent<CharacterMovement>().TakeDamage(350);
                    hit.GetComponent<CharacterMovement>().Stun(2f, 1000f, 6000f);
                }
            }
        }
        
        yield return new WaitForSeconds(cooldown);
        canUseSkill2 = true;
    }

    private IEnumerator Ultimate()
    {
        canUseUlti = false;

        GetComponent<CharacterMovement>().stunTime = 0;
        GetComponent<CharacterMovement>().canMove = false;
        GetComponent<CharacterMovement>().canDash = false;
        GetComponent<CharacterMovement>().canAttack = false;
        GetComponent<CharacterMovement>().canDefen = false;
        GetComponent<CharacterMovement>().canStun = false;
        GetComponent<CharacterMovement>().canUseSkill = false;
        GetComponent<CharacterMovement>().LoadEffect(powEffect, new Vector3(transform.position.x, GetComponent<CharacterMovement>().m_CeilingCheck.position.y, -2), Quaternion.Euler(-90, 0, 0), 1f, new Vector3(4, 4, 4));      

        yield return new WaitForSeconds(0.25f);
        if (GetComponent<CharacterMovement>().m_FacingRight)
        {
            GameObject explsion = Instantiate(smokeEffect, new Vector3(GetComponent<CharacterMovement>().m_GroundCheck.position.x + 2, GetComponent<CharacterMovement>().m_GroundCheck.position.y, -2), Quaternion.Euler(0, 0, 0));
        }
        else
        {
            GameObject explsion = Instantiate(smokeEffect, new Vector3(GetComponent<CharacterMovement>().m_GroundCheck.position.x - 2, GetComponent<CharacterMovement>().m_GroundCheck.position.y, -2), Quaternion.Euler(0, 0, 0));
        }

        yield return new WaitForSeconds(0.25f);
        usingUlti = true;
        animator.SetBool("usingUlti", true);
        yield return new WaitForSeconds(5f);

        animator.SetBool("usingUlti", false);
        GetComponent<CharacterMovement>().canMove = true;
        GetComponent<CharacterMovement>().canDash = true;
        GetComponent<CharacterMovement>().canAttack = true;
        GetComponent<CharacterMovement>().canDefen = true;
        GetComponent<CharacterMovement>().canStun = true;
        GetComponent<CharacterMovement>().canUseSkill = true;
        usingUlti = false;
        canUseUlti = true;

    }
}
