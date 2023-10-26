using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class GolemSkill : MonoBehaviour
{
    GameObject enemy;
    private bool canUseSkill1;
    private bool canUseSkill2;
    private bool canUseUlti;
    private bool isUlti;

    private Animator animator;
    private Rigidbody2D rb;
    public GameObject macePrefab;  

    [SerializeField] private GameObject skill1Effect;
    [SerializeField] private GameObject explosionEffect;
    [SerializeField] private GameObject powTauntEffect;
    [SerializeField] private GameObject powExplosionEffect;
    [SerializeField] private GameObject powEffect;
    [SerializeField] private GameObject handEffect;


    public GameObject skill1;
    public GameObject skill2;

    [Header("Body Parts")]
    public GameObject head;
    public GameObject weapon;
    public UnityEngine.Transform leftHand;
    public UnityEngine.Transform rightHand;

    List<Vector3> vector = new List<Vector3>();
    List<float> index = new List<float>();

    float damageScale = 1;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        canUseSkill1 = true;
        canUseSkill2 = true;
        canUseUlti = true;
        isUlti = false;
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
                    if (canUseUlti) //&& GetComponent<CharacterController>().currentPower == 100)
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

        macePrefab.tag = tag + " Skill";
        macePrefab.GetComponent<Mace>().check = isUlti;
        if (GetComponent<CharacterController>().m_FacingRight)
        {
            GetComponent<CharacterController>().canNotMove(.5f);
            GetComponent<CharacterController>().LoadEffect(skill1Effect, new Vector3(GetComponent<CharacterController>().attackPoint.position.x, GetComponent<CharacterController>().attackPoint.position.y, -2), Quaternion.identity, 1f, Vector3.one);
            yield return new WaitForSeconds(0.1f);
            GameObject gObject = Instantiate(macePrefab, (GetComponent<CharacterController>().m_CeilingCheck.position+ GetComponent<CharacterController>().m_GroundCheck.position)/2, transform.rotation);
            gObject.GetComponent<Mace>().damage = (int)(100 * damageScale);
            Destroy(gObject, 1f);
        }
        else
        {
            GetComponent<CharacterController>().canNotMove(.5f);
            GetComponent<CharacterController>().LoadEffect(skill1Effect, new Vector3(GetComponent<CharacterController>().attackPoint.position.x, GetComponent<CharacterController>().attackPoint.position.y, -2), Quaternion.Euler(0,-180,0), 1f, Vector3.one);
            yield return new WaitForSeconds(0.1f);
            GameObject gObject = Instantiate(macePrefab, (GetComponent<CharacterController>().m_CeilingCheck.position + GetComponent<CharacterController>().m_GroundCheck.position) / 2, Quaternion.Euler(180, 0, 180));
            gObject.GetComponent<Mace>().damage = (int)(100 * damageScale);
            Destroy(gObject, 1f);
        }
        canUseSkill1 = true;
    }


    private IEnumerator Skill2(float cooldown)
    {
        canUseSkill2 = false;
        GetComponent<CharacterController>().canNotMove(.7f);
        GetComponent<CharacterController>().canDash = false;
        GetComponent<CharacterController>().canNotAttack(.7f);
        GetComponent<CharacterController>().canDefen = false;
        GetComponent<CharacterController>().canUseSkill = false;

        if(isUlti)
        {
            if (GetComponent<CharacterController>().m_FacingRight)
            {
                if (GetComponent<CharacterController>().isGrounded)
                {
                    GetComponent<Rigidbody2D>().AddForce(new Vector2(8000f, 6000f));
                }
                else
                {
                    GetComponent<Rigidbody2D>().AddForce(new Vector2(8000f, -6000f));
                }
            }
            else
            {
                if (GetComponent<CharacterController>().isGrounded)
                {
                    GetComponent<Rigidbody2D>().AddForce(new Vector2(-8000f, 6000f));
                }
                else
                {
                    GetComponent<Rigidbody2D>().AddForce(new Vector2(-8000f, -6000f));
                }
            }
        }
        else
        {
            if (GetComponent<CharacterController>().m_FacingRight)
            {
                if (GetComponent<CharacterController>().isGrounded)
                {
                    GetComponent<Rigidbody2D>().AddForce(new Vector2(6000f, 5000f));
                }
                else
                {
                    GetComponent<Rigidbody2D>().AddForce(new Vector2(6000f, -5000f));
                }
            }
            else
            {
                if (GetComponent<CharacterController>().isGrounded)
                {
                    GetComponent<Rigidbody2D>().AddForce(new Vector2(-6000f, 5000f));
                }
                else
                {
                    GetComponent<Rigidbody2D>().AddForce(new Vector2(-6000f, -5000f));
                }
            }
        }
        yield return new WaitForSeconds(0.7f);
        GetComponent<CharacterController>().canDash = true;
        GetComponent<CharacterController>().canDefen = true;
        GetComponent<CharacterController>().canUseSkill = true;       

        if(isUlti)
        {
            while(true)
            {
                if (GetComponent<CharacterController>().isGrounded)
                {
                    Collider2D[] hits = Physics2D.OverlapCircleAll(GetComponent<CharacterController>().m_GroundCheck.position, 3f, GetComponent<CharacterController>().enemyLayers);
                    foreach (Collider2D hit in hits)
                    {                      
                        hit.GetComponent<CharacterController>().TakeDamage((int)(250 * damageScale));
                        hit.GetComponent<CharacterController>().Stun(2f, 0, 7000);
                    }
                    GameObject gObject = Instantiate(explosionEffect, new Vector3(GetComponent<CharacterController>().m_GroundCheck.position.x, GetComponent<CharacterController>().m_GroundCheck.position.y, -2), Quaternion.identity);
                    gObject.transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
                    Destroy(gObject,3f);
                    break;
                }
                else
                {
                    yield return new WaitForSeconds(0.1f);
                }
            }
        }
        else
        {
            while (true)
            {
                if (GetComponent<CharacterController>().isGrounded)
                {
                    Collider2D[] hits = Physics2D.OverlapCircleAll(GetComponent<CharacterController>().m_GroundCheck.position, 1f, GetComponent<CharacterController>().enemyLayers);
                    foreach (Collider2D hit in hits)
                    {                      
                        hit.GetComponent<CharacterController>().TakeDamage((int)(150 * damageScale));
                    }
                    GameObject gObject = Instantiate(explosionEffect, new Vector3(GetComponent<CharacterController>().m_GroundCheck.position.x, GetComponent<CharacterController>().m_GroundCheck.position.y, -2), Quaternion.identity);
                    gObject.transform.localScale = new Vector3(.5f, .5f, .5f);
                    Destroy(gObject, 3f);
                    break;
                }
                else
                {
                    yield return new WaitForSeconds(0.1f);
                }
            }         
        }
        //skill2.GetComponent<SkillCooldown>().UseSkill(cooldown);
        //yield return new WaitForSeconds(cooldown);
        canUseSkill2 = true;
    }

    private IEnumerator Ultimate()
    {
        canUseUlti = false;
        GetComponent<CharacterController>().canNotMove(.7f);
        GetComponent<CharacterController>().LoadEffect(powEffect, new Vector3(transform.position.x, GetComponent<CharacterController>().m_CeilingCheck.position.y, -2), Quaternion.Euler(-90, 0, 0), 1f, new Vector3(4, 4, 4));
        GetComponent<CharacterController>().LoadEffect(powTauntEffect, new Vector3(GetComponent<CharacterController>().m_GroundCheck.position.x, GetComponent<CharacterController>().m_GroundCheck.position.y, -2), Quaternion.Euler(0, 0, 0), 1f, new Vector3(2, 2, 2));
        yield return new WaitForSeconds(.7f);

        GetComponent<CharacterController>().LoadEffect(powExplosionEffect, new Vector3(GetComponent<CharacterController>().m_GroundCheck.position.x, GetComponent<CharacterController>().m_GroundCheck.position.y, -2), Quaternion.Euler(0, 0, 0), 2f, new Vector3(2, 2, 2));
        Collider2D[] hits = Physics2D.OverlapCircleAll(GetComponent<CharacterController>().m_GroundCheck.position, 2f, GetComponent<CharacterController>().enemyLayers);
        foreach (Collider2D hit in hits)
        {
            Vector2 newVector = GetComponent<CharacterController>().m_GroundCheck.position - hit.GetComponent<CharacterController>().m_GroundCheck.position;
            if (newVector.x >= 0)
            {
                hit.GetComponent<CharacterController>().TakeDamage((int)(150 * damageScale));
                hit.GetComponent<CharacterController>().Stun(1f, -1000f, 7000f);
            }
            else
            {
                hit.GetComponent<CharacterController>().TakeDamage((int)(150 * damageScale));
                hit.GetComponent<CharacterController>().Stun(1f, 1000f, 7000f);
            }
        }

        GameObject leftEffect = Instantiate(handEffect, new Vector3(leftHand.position.x, leftHand.position.y, 2), Quaternion.Euler(-90, 0, 0));
        leftEffect.transform.parent = leftHand;
        leftEffect.transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
        Destroy(leftEffect, 10f);
        GameObject rightEffect = Instantiate(handEffect, new Vector3(rightHand.position.x, rightHand.position.y, -2), Quaternion.Euler(-90, 0, 0));
        rightEffect.transform.parent = rightHand;
        rightEffect.transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
        Destroy(rightEffect, 10f);

        vector.Add(transform.localScale);
        vector.Add(GetComponent<BoxCollider2D>().size);
        vector.Add(GetComponent<Collider2D>().offset);
        vector.Add(head.transform.position);
        vector.Add(head.transform.localScale);

        index.Add((float)GetComponent<CharacterController>().attackDmg);
        index.Add((float)GetComponent<CharacterController>().armor);
        index.Add((float)GetComponent<CharacterController>().walkSpeed);
        index.Add((float)GetComponent<CharacterController>().attackSpeed);


        //Transform
        isUlti = true;
        if (GetComponent<CharacterController>().m_FacingRight)
        {
            transform.localScale = new Vector3(1.8f, 1.8f, 1.8f);
        }
        else
        {
            transform.localScale = new Vector3(-1.8f, 1.8f, 1.8f);
        }
        head.transform.position = new Vector3(0.93f, -0.3f, 0);
        head.transform.localScale = new Vector3(0.3f, 0.3f, 1);
        weapon.SetActive(false);
        GetComponent<BoxCollider2D>().size = new Vector2(2.63875151f, 2.99374962f);
        GetComponent<Collider2D>().offset = new Vector2(-0.0662674904f, 1.32302356f);
        GetComponent<CharacterController>().attackDmg = 150;
        GetComponent<CharacterController>().armor = 50;
        GetComponent<CharacterController>().walkSpeed = 17;
        GetComponent<CharacterController>().attackSpeed = 1.5f;
        GetComponent<CharacterController>().m_CeilingCheck.localPosition = new Vector3(0, 2.65f, 0);
        animator.speed = 0.7f;
        rb.mass = 10;

        yield return new WaitForSeconds(10f);
        
        if (GetComponent<CharacterController>().m_FacingRight)
        {
            transform.localScale = new Vector3(Mathf.Abs(vector[0].x), vector[0].y, vector[0].z);
        }
        else
        {
            transform.localScale = new Vector3(-Mathf.Abs(vector[0].x), vector[0].y, vector[0].z);
        }
        head.transform.position = vector[3];
        head.transform.localScale = vector[4];
        weapon.SetActive(true);
        GetComponent<BoxCollider2D>().size = vector[1];
        GetComponent<Collider2D>().offset = vector[2];
        GetComponent<CharacterController>().m_CeilingCheck.localPosition = new Vector3(0, 3.98f, 0);

        GetComponent<CharacterController>().attackDmg = (int)index[0];
        GetComponent<CharacterController>().armor = (int)index[1];
        GetComponent<CharacterController>().walkSpeed = index[2];
        GetComponent<CharacterController>().attackSpeed = index[3];
        animator.speed = 1;
        rb.mass = 7;
        isUlti = false;
        GetComponent<CharacterController>().canNotMove(.1f);

        GetComponent<CharacterController>().currentPower = 0;
        canUseUlti = true;
    }
}
