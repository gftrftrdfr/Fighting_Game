using Spriter2UnityDX;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class StoneGiantSkill: MonoBehaviour
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
    [SerializeField] private GameObject powEffect2;
    [SerializeField] private GameObject explosionEffect;

    public GameObject skill1;
    public GameObject skill2;

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
                        StartCoroutine(Skill1(7f));
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
                        StartCoroutine(Skill1(7f));
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
        canUseSkill1 = false;

        GetComponent<CharacterController>().LoadEffect(ammorBuffEffect, new Vector3(GetComponent<CharacterController>().m_GroundCheck.position.x, (GetComponent<CharacterController>().m_GroundCheck.position.y + GetComponent<CharacterController>().m_CeilingCheck.position.y) / 2, -3), Quaternion.Euler(-90, 0, 0), 0.5f, new Vector2(2f,2f));
        
        yield return new WaitForSeconds(0.5f);

        GetComponent<EntityRenderer>().Color = new Color(0.4292453f, 1, 0.785162f);
        GetComponent<CharacterController>().IncreaseArmor(30);

        GetComponent<CharacterController>().canReflect = true;
        yield return new WaitForSeconds(5f);

        GetComponent<CharacterController>().DecreaseArmor(30);
        GetComponent<EntityRenderer>().Color = Color.white;

        skill1.GetComponent<SkillCooldown>().UseSkill(cooldown);
        yield return new WaitForSeconds(cooldown);
        canUseSkill1 = true;
    }


    private IEnumerator Skill2(float cooldown)
    {
        canUseSkill2 = false;
        GetComponent<CharacterController>().canNotMove(0.7f);
        GetComponent<CharacterController>().LoadEffect(shieldEffect, new Vector3(GetComponent<CharacterController>().m_GroundCheck.position.x, (GetComponent<CharacterController>().m_GroundCheck.position.y + GetComponent<CharacterController>().m_CeilingCheck.position.y) / 2, -3), Quaternion.Euler(-90, 0, 0), 0.7f, new Vector3(3f, 3f, 3));

        yield return new WaitForSeconds(0.7f);
        GetComponent<CharacterController>().LoadEffect(showEffect, new Vector3(GetComponent<CharacterController>().attackPoint.position.x, GetComponent<CharacterController>().m_GroundCheck.position.y, -2), Quaternion.Euler(0, 0, 0), 3f, new Vector3(1.5f, 1.5f, 1.5f));

        rockPrefab.tag = tag + " Skill";
        if (GetComponent<CharacterController>().m_FacingRight)
        {
            GameObject gObject = Instantiate(rockPrefab, new Vector3(GetComponent<CharacterController>().m_GroundCheck.position.x, GetComponent<CharacterController>().m_GroundCheck.position.y - 0.8f , 2), transform.rotation);
            gObject.GetComponent<Rigidbody2D>().velocity = new Vector2(15.0f, 0.0f);
            Destroy(gObject, 8f);

        }
        else
        {
            GameObject gObject = Instantiate(rockPrefab, new Vector3(GetComponent<CharacterController>().m_GroundCheck.position.x, GetComponent<CharacterController>().m_GroundCheck.position.y - 0.8f, 2), Quaternion.Euler(new Vector3(0f,180f,0f)));
            gObject.GetComponent<Rigidbody2D>().velocity = new Vector2(-15.0f, 0.0f);
            Destroy(gObject, 8f);
        }

        skill2.GetComponent<SkillCooldown>().UseSkill(cooldown);
        yield return new WaitForSeconds(cooldown);
        canUseSkill2 = true;
    }

    private IEnumerator Ultimate()
    {
        canUseUlti = false;
        GetComponent<CharacterController>().canNotMove(1f);
        GetComponent<CharacterController>().canDash = false;
        GetComponent<CharacterController>().canNotAttack(1f);
        GetComponent<CharacterController>().canDefen = false;
        GetComponent<CharacterController>().canUseSkill = false;

        GetComponent<CharacterController>().LoadEffect(powEffect, new Vector3(transform.position.x, GetComponent<CharacterController>().m_CeilingCheck.position.y, -2), Quaternion.Euler(-90, 0, 0), 1f, new Vector3(4, 4, 4));
        GetComponent<CharacterController>().LoadEffect(powEffect2, new Vector3(GetComponent<CharacterController>().m_GroundCheck.position.x, GetComponent<CharacterController>().m_GroundCheck.position.y, -2), Quaternion.Euler(0, 0, 0), 1f, new Vector3(2, 2, 2));
        if (GetComponent<CharacterController>().isGrounded)
        {
            GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, 10500f));
            yield return new WaitForSeconds(0.5f);            
        }
        GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, -20000f));
        yield return new WaitForSeconds(0.3f);
        GetComponent<CharacterController>().canDash = true;
        GetComponent<CharacterController>().canDefen = true;
        GetComponent<CharacterController>().canUseSkill = true;

        GetComponent<CharacterController>().LoadEffect(ultiEffect, new Vector3(GetComponent<CharacterController>().m_GroundCheck.position.x, GetComponent<CharacterController>().m_GroundCheck.position.y, -2), Quaternion.Euler(-90, 0, 0), 3f, new Vector3(4f, 4f, 4f));
        GetComponent<CharacterController>().LoadEffect(explosionEffect, new Vector3(GetComponent<CharacterController>().m_GroundCheck.position.x, GetComponent<CharacterController>().m_GroundCheck.position.y, -2), Quaternion.Euler(0, 0, 0), 3f, new Vector3(3f, 3f, 3f));
        Collider2D[] hits = Physics2D.OverlapCircleAll(GetComponent<CharacterController>().m_GroundCheck.position, 7f, GetComponent<CharacterController>().enemyLayers);
        foreach (Collider2D hit in hits)
        {
            Vector2 newVector = GetComponent<CharacterController>().m_GroundCheck.position - hit.GetComponent<CharacterController>().m_GroundCheck.position;
            if (newVector.x >= 0)
            {
                hit.GetComponent<CharacterController>().TakeDamage(350);
                hit.GetComponent<CharacterController>().Stun(4f, -1500f, 8000f);
            }
            else
            {
                hit.GetComponent<CharacterController>().TakeDamage(350);
                hit.GetComponent<CharacterController>().Stun(4f, 1500f, 8000f);
            }
        }
        GetComponent<CharacterController>().currentPower = 0;
        canUseUlti = true;
    }
}
