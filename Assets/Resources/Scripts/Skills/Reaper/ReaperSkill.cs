using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class ReaperSkill : MonoBehaviour
{
    GameObject enemy;
    private bool canUseSkill1;
    private bool canUseSkill2;
    private bool canUseUlti;

    private Animator animator;
    private Rigidbody2D rb;
    public GameObject skullPrefab;

    public Transform leftHand;
    public Transform rightHand;

    [SerializeField] private GameObject buffEffect;
    [SerializeField] private GameObject zoneEffect;
    [SerializeField] private GameObject bloodEffect;
    [SerializeField] private GameObject powEffect;
    [SerializeField] private GameObject powTauntEffect;
    [SerializeField] private GameObject slashEffect;

    public GameObject skill1;
    public GameObject skill2;

    float damageScale = 1;

    int[] currnetHeathTmp = new int[2];

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
        canUseSkill1 = false;
        GetComponent<CharacterController>().canNotMove(.5f);
        GetComponent<CharacterController>().LoadEffect(zoneEffect, GetComponent<CharacterController>().m_GroundCheck.position, Quaternion.Euler(0f, 0f, 0f), 5f, new Vector3(2.7f, 2.7f, 2.7f));
        yield return new WaitForSeconds(0.5f);

        GetComponent<CharacterController>().IncreaseATK((int)(20 * damageScale));
        GameObject leftEffect = Instantiate(buffEffect, new Vector3(leftHand.position.x, leftHand.position.y, 2), Quaternion.Euler(0, 0, 0));
        leftEffect.transform.parent = leftHand;
        Destroy(leftEffect, 5f);
        GameObject rightEffect = Instantiate(buffEffect, new Vector3(rightHand.position.x, rightHand.position.y, -2), Quaternion.Euler(0, 0, 0));
        rightEffect.transform.parent = rightHand;
        Destroy(rightEffect, 5f);

        int count = 0;
        Collider2D[] hits = Physics2D.OverlapCircleAll(GetComponent<CharacterController>().m_GroundCheck.position, 5f, GetComponent<CharacterController>().enemyLayers);
        foreach (Collider2D hit in hits)
        {
            hit.GetComponent<CharacterController>().LoadEffect(skullPrefab, hit.GetComponent<CharacterController>().m_CeilingCheck.position , Quaternion.Euler(0f, 0f, 0f), 5f, Vector3.one);
            currnetHeathTmp[count] = hit.GetComponent<CharacterController>().currentHealth;  
            count++;
        }

        yield return new WaitForSeconds(5f);
        count = 0;
        foreach (Collider2D hit in hits)
        {
            if (hit.GetComponent<CharacterController>().currentHealth < currnetHeathTmp[count])
            {
                hit.GetComponent<CharacterController>().TakeDamage((int)((currnetHeathTmp[count] - hit.GetComponent<CharacterController>().currentHealth) * 0.2*damageScale));
            }
            count++;
        }
        GetComponent<CharacterController>().DecreaseATK((int)(20 * damageScale));

        skill1.GetComponent<SkillCooldown>().UseSkill(cooldown);
        yield return new WaitForSeconds(cooldown);
        canUseSkill1 = true;
    }


    private IEnumerator Skill2(float cooldown)
    {
        canUseSkill2 = false;
        GetComponent<CharacterController>().canNotMove(.2f);

        yield return new WaitForSeconds(.2f);

        Collider2D[] hits = Physics2D.OverlapCircleAll(GetComponent<CharacterController>().attackPoint.position, 4f, GetComponent<CharacterController>().enemyLayers);
        foreach (Collider2D hit in hits)
        {
            Vector2 newVector = GetComponent<CharacterController>().m_GroundCheck.position - hit.GetComponent<CharacterController>().m_GroundCheck.position;
            hit.GetComponent<Rigidbody2D>().AddForce(newVector * hit.GetComponent<Rigidbody2D>().mass * 1300);
            hit.GetComponent<CharacterController>().TakeDamage((int)(150 * damageScale));
            if(GetComponent<CharacterController>().m_FacingRight)
            {
                hit.GetComponent<CharacterController>().LoadEffect(bloodEffect, new Vector3(hit.GetComponent<CharacterController>().m_GroundCheck.position.x, (hit.GetComponent<CharacterController>().m_GroundCheck.position.y + hit.GetComponent<CharacterController>().m_CeilingCheck.position.y) / 2, -2), Quaternion.Euler(0, 90, -90), 2f, Vector3.one);
            }
            else
            {
                hit.GetComponent<CharacterController>().LoadEffect(bloodEffect, new Vector3(hit.GetComponent<CharacterController>().m_GroundCheck.position.x, (hit.GetComponent<CharacterController>().m_GroundCheck.position.y + hit.GetComponent<CharacterController>().m_CeilingCheck.position.y) / 2, -2), Quaternion.Euler(0, -90, 90), 2f, Vector3.one);
            }
        }

        skill2.GetComponent<SkillCooldown>().UseSkill(cooldown);
        yield return new WaitForSeconds(cooldown);
        canUseSkill2 = true;
    }

    private IEnumerator Ultimate()
    {
        canUseUlti = false;
        GetComponent<CharacterController>().canNotMove(.5f);
        GetComponent<CharacterController>().canDash = false;
        GetComponent<CharacterController>().canNotAttack(.5f);
        GetComponent<CharacterController>().canDefen = false;
        GetComponent<CharacterController>().canUseSkill = false;

        GetComponent<CharacterController>().LoadEffect(powEffect, new Vector3(transform.position.x, GetComponent<CharacterController>().m_CeilingCheck.position.y, -2), Quaternion.Euler(-90, 0, 0), 1f, new Vector3(4, 4, 4));
        GetComponent<CharacterController>().LoadEffect(powTauntEffect, new Vector3(GetComponent<CharacterController>().m_GroundCheck.position.x, GetComponent<CharacterController>().m_GroundCheck.position.y, -2), Quaternion.Euler(0, 0, 0), 1f, new Vector3(2, 2, 2));
        yield return new WaitForSeconds(0.5f);

        GetComponent<CharacterController>().canDash = true;
        GetComponent<CharacterController>().canDefen = true;
        GetComponent<CharacterController>().canUseSkill = true;

        GetComponent<CharacterController>().LoadEffect(slashEffect, new Vector3(GetComponent<CharacterController>().attackPoint.position.x, GetComponent<CharacterController>().attackPoint.position.y, -2), Quaternion.Euler(0, 0, 0), 3f, new Vector3(5f, 5f, 5f));
        Collider2D[] hits = Physics2D.OverlapCircleAll(GetComponent<CharacterController>().attackPoint.position, 2.5f, GetComponent<CharacterController>().enemyLayers);
        foreach (Collider2D hit in hits)
        {
            if (hit.GetComponent<CharacterController>().currentHealth > hit.GetComponent<CharacterController>().maxHealth * .15f)
            {
                hit.GetComponent<CharacterController>().TakeDamage((int)(650 * damageScale));
            }
            else
            {
                hit.GetComponent<CharacterController>().TakeTrueDamage(9999);
            }
        }
        GetComponent<CharacterController>().currentPower = 0;
        canUseUlti = true;
    }

}
