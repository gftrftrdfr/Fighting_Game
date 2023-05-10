using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReaperSkill : MonoBehaviour
{
    GameObject enemy;
    private bool canUseSkill1;
    private bool canUseSkill2;
    private bool canUseUlti;
    float time = 0;

    private Animator animator;
    private Rigidbody2D rb;
    public GameObject sicklePrefab;

    public Transform leftHand;
    public Transform rightHand;

    [SerializeField] private GameObject buffEffect;
    [SerializeField] private GameObject powEffect;
    [SerializeField] private GameObject slashEffect;
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
                if(Input.GetButtonDown("Cast 3 P1"))
                {
                    if (canUseUlti&& GetComponent<CharacterMovement>().currentPower == 100)
                    {
                        GetComponent<CharacterMovement>().LoadEffect(powEffect, new Vector3(transform.position.x, GetComponent<CharacterMovement>().m_CeilingCheck.position.y, -2), Quaternion.Euler(-90, 0, 0), 1f, new Vector3(4, 4, 4));
                    }
                }
                if (Input.GetButton("Cast 3 P1"))
                {
                    if (canUseUlti && GetComponent<CharacterMovement>().currentPower == 100)
                    {
                        animator.SetTrigger("isCasting3");
                        time += Time.deltaTime;                     
                    }
                    else
                    {
                        GetComponent<CharacterMovement>().Show("Can't use now", Color.grey);
                    }
                }
                if(Input.GetButtonUp("Cast 3 P1") && time > 0)
                {
                    StartCoroutine(Ultimate());
                    GetComponent<CharacterMovement>().currentPower = 0;
                    canUseUlti = true;
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
                        GetComponent<CharacterMovement>().LoadEffect(powEffect, new Vector3(transform.position.x, GetComponent<CharacterMovement>().m_CeilingCheck.position.y, -2), Quaternion.Euler(-90, 0, 0), 1f, new Vector3(4, 4, 4));
                    }
                }
                if (Input.GetButton("Cast 3 P2"))
                {
                    if (canUseUlti && GetComponent<CharacterMovement>().currentPower == 100)
                    {
                        animator.SetTrigger("isCasting3");
                        time += Time.deltaTime;
                    }
                    else
                    {
                        GetComponent<CharacterMovement>().Show("Can't use now", Color.grey);
                    }
                }
                if (Input.GetButtonUp("Cast 3 P2") && time > 0)
                {
                    StartCoroutine(Ultimate());
                    GetComponent<CharacterMovement>().currentPower = 0;
                    canUseUlti = true;
                }
            }
        }
    }

    private void FixedUpdate()
    {
        if(!GetComponent<CharacterMovement>().canUseSkill)
        {
            time = 0;
        }
    }

    private IEnumerator Skill1(float cooldown)
    {
        canUseSkill1 = false;
        GetComponent<CharacterMovement>().canMove = false;
        yield return new WaitForSeconds(0.5f);

        GetComponent<CharacterMovement>().canMove = true;
        GameObject leftEffect = Instantiate(buffEffect, new Vector3(leftHand.position.x, leftHand.position.y, 2), Quaternion.Euler(0, 0, 0));
        leftEffect.transform.parent = leftHand;
        Destroy(leftEffect, 5f);
        GameObject rightEffect = Instantiate(buffEffect, new Vector3(rightHand.position.x, rightHand.position.y, -2), Quaternion.Euler(0, 0, 0));
        rightEffect.transform.parent = rightHand;
        Destroy(rightEffect, 5f);

        enemy.GetComponent<CharacterMovement>().LoadEffect(sicklePrefab, enemy.GetComponent<CharacterMovement>().m_CeilingCheck.position, Quaternion.Euler(0f,0f,0f), 5f, Vector3.one);

        int currnetHeathTmp = enemy.GetComponent<CharacterMovement>().currentHealth;
        int tmp = GetComponent<CharacterMovement>().attackDmg;
        GetComponent<CharacterMovement>().IncreaseATK("20");
        GetComponent<CharacterMovement>().attackDmg = tmp + 20;

        yield return new WaitForSeconds(5f);
        if (enemy.GetComponent<CharacterMovement>().currentHealth < currnetHeathTmp)
        {
            enemy.GetComponent<CharacterMovement>().TakeDamage((int)((currnetHeathTmp - enemy.GetComponent<CharacterMovement>().currentHealth) * 0.3));
        }
        GetComponent<CharacterMovement>().attackDmg = tmp;

        yield return new WaitForSeconds(cooldown);
        canUseSkill1 = true;
    }


    private IEnumerator Skill2(float cooldown)
    {
        canUseSkill2 = false;
        GetComponent<CharacterMovement>().canMove = false;

        yield return new WaitForSeconds(.2f);
        GetComponent<CharacterMovement>().canMove = true;
        Collider2D[] hits = Physics2D.OverlapCircleAll(GetComponent<CharacterMovement>().attackPoint.position, 4f, GetComponent<CharacterMovement>().enemyLayers);
        foreach (Collider2D hit in hits)
        {
            Vector2 newVector = GetComponent<CharacterMovement>().m_GroundCheck.position - hit.GetComponent<CharacterMovement>().m_GroundCheck.position;
            hit.GetComponent<Rigidbody2D>().AddForce(newVector * hit.GetComponent<Rigidbody2D>().mass * 1300);
            hit.GetComponent<CharacterMovement>().TakeDamage(150);          
        }
        yield return new WaitForSeconds(cooldown);
        canUseSkill2 = true;
    }

    private IEnumerator Ultimate()
    {
        canUseUlti = false;
        canUseUlti = false;
        GetComponent<CharacterMovement>().canMove = false;
        GetComponent<CharacterMovement>().canDash = false;
        GetComponent<CharacterMovement>().canAttack = false;
        GetComponent<CharacterMovement>().canDefen = false;
        GetComponent<CharacterMovement>().canUseSkill = false;

        GetComponent<CharacterMovement>().LoadEffect(slashEffect, new Vector3(GetComponent<CharacterMovement>().attackPoint.position.x, GetComponent<CharacterMovement>().attackPoint.position.y, -2), Quaternion.Euler(0, 0, -90), 3f, new Vector3(time * 0.8f, time * 0.8f, time * 0.8f));
        Collider2D[] hits = Physics2D.OverlapCircleAll(GetComponent<CharacterMovement>().m_GroundCheck.position, time * 2, GetComponent<CharacterMovement>().enemyLayers);
        foreach (Collider2D hit in hits)
        {
            if (hit.GetComponent<CharacterMovement>().currentHealth > hit.GetComponent<CharacterMovement>().maxHealth * 0.15)
            {
                hit.GetComponent<CharacterMovement>().TakeDamage((int)(time * 200));
            }
            else
            {
                hit.GetComponent<CharacterMovement>().TakeTrueDamage(9999);
            }
        }
        yield return new WaitForSeconds(0.5f);
        canUseUlti = true;
        GetComponent<CharacterMovement>().canMove = true;
        GetComponent<CharacterMovement>().canDash = true;
        GetComponent<CharacterMovement>().canAttack = true;
        GetComponent<CharacterMovement>().canDefen = true;
        GetComponent<CharacterMovement>().canUseSkill = true;
        time = 0;
    }

}
