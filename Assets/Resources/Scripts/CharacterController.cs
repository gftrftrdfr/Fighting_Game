using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class CharacterController : MonoBehaviour
{
    [Header("Point Settings")]
    CharacterMovement movement;
    Animator animator;
    GameObject player;
    private Rigidbody2D rb;
    public LayerMask m_WhatIsGround;
    public Transform m_GroundCheck;
    public Transform m_CeilingCheck;
    private Component[] cpn;
    public Transform attackPoint;
    public float attackRange = 0.5f;
    public LayerMask enemyLayers;

    //[Header("Audio Settings")]
    //public AudioClip attackSound1;
    //public AudioClip attackSound2;
    //public AudioClip attackSound3;
    //public AudioClip attackSound4;
    //public AudioClip jumpSound;
    //public AudioClip runSound;
    //public AudioClip hurtSound;
    //public AudioClip dashSound;
    //public AudioClip defendSound;
    //public AudioClip stunSound;
    //public AudioClip dieSound;

    //public AudioClip atkUpSound;
    //public AudioClip atkSpeedUpSound;
    //public AudioClip speedUpSound;
    //public AudioClip ammorUpSound;
    //public AudioClip staminaUpSound;
    //public AudioClip healthSound;

    [Header("Effects Settings")]
    [SerializeField] private GameObject jumpEffect;
    [SerializeField] private GameObject runEffect;
    [SerializeField] private GameObject hurtEffect1;
    [SerializeField] private GameObject hurtEffect2;
    [SerializeField] private GameObject hurtEffect3;
    [SerializeField] private GameObject dieEffect;   
    [SerializeField] private GameObject bleedEffect;
    [SerializeField] private GameObject slashEffect;
    [SerializeField] private GameObject slashEffect2;
    [SerializeField] private GameObject slashEffect3;
    [SerializeField] private GameObject hitEffect;
    [SerializeField] private GameObject healEffect;
    [SerializeField] private GameObject defenEffect;

    [SerializeField] private GameObject atkUpEffect;
    [SerializeField] private GameObject atkSpeedUpEffect;
    [SerializeField] private GameObject speedUpEffect;
    [SerializeField] private GameObject ammorUpEffect;
    [SerializeField] private GameObject staminaUpEffect;
    [SerializeField] private GameObject healthUpEffect;

    [SerializeField] private GameObject stunEffect;
    [SerializeField] private GameObject stunHitEffect;
    [SerializeField] private GameObject intoxicatedEffect;
    [SerializeField] private GameObject silencedEffect;

    [Header("Index Settings")]
    public float walkSpeed = 8f;
    public int maxHealth = 100;
    public int currentHealth;
    public float maxStamina = 1000000;
    public float currentStamina;
    public int maxPower = 100;
    public int currentPower;
    public int attackDmg = 10;
    public float attackSpeed = 1f;
    public int armor = 20;

    float horizontalMove = 0f;
    float horizontalRun = 0f;   

    private bool checkP2;
    const float k_GroundedRadius = 0.2f;

    [Header("Controller Settings")]
    public bool jump = false;
    public bool canTakeDame = true;
    public bool canDash = true;
    public bool canUseSkill = true;
    public bool canMove = true;
    public bool canAttack = true;
    public bool cooldownAttack = true;
    public bool canDefen = true;   
    public bool canStun = true;
    public bool canReflect = false;
    public bool canHeal = false;
    public bool canHealWhenATK = false;
    public bool isAttacking = false;
    public bool isDefending = false;

    public bool isStun = false;
    public bool isIntoxicated = false;

    public bool isGrounded;
    public bool m_FacingRight = true;
    public bool isBleeding = false;
    private float lerpSpeed;
    public bool isDead = false;

    [Header("Time Settings")]
    private bool isDashing;
    private float dashingTime = 0.2f;
    public float dashCooldown = 10f;
    public float stunTime = 0;
    public float intoxicatedTime = 0;
    public float dashTime = 0;
    public float runTime = 0;
    public float healTime = 0;

    public int count = 1;
    float time = 0;
    public float timeCanNotAttack = 0;
    public float timeCanNotMove = 0;
   
    GameObject enemy;
    Transform followPoint;

    [Header("Text Settings")]
    [SerializeField] private GameObject textPopup;
    public GameObject hitTextP1;
    public GameObject hitTextP2;
    public GameObject dash;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        movement = GetComponent<CharacterMovement>();
        player = this.gameObject;
        currentHealth = maxHealth;
        currentStamina = maxStamina;
        currentPower = 0;
        animator.SetBool("isDead", false); 
        checkP2 = true;
        Collider2D[] parts = GetComponentsInChildren<PolygonCollider2D>();
        foreach (PolygonCollider2D part in parts)
        {
            part.isTrigger = true;
        }

        m_WhatIsGround.value = 2048;
        if (player.tag == "Player 2")
        {        
            m_WhatIsGround |= (1 << LayerMask.NameToLayer("Land"));
            m_WhatIsGround |= (1 << LayerMask.NameToLayer("Water"));
            enemy = GameObject.FindGameObjectWithTag("Player 1");
            enemyLayers |= (1 << LayerMask.NameToLayer("Player 1"));
            dash = GameObject.FindGameObjectWithTag("Dash P2");
        }
        else if (player.tag == "Player 1")
        {
            m_WhatIsGround |= (1 << LayerMask.NameToLayer("Land"));
            m_WhatIsGround |= (1 << LayerMask.NameToLayer("Water"));
            enemy = GameObject.FindGameObjectWithTag("Player 2");
            enemyLayers |= (1 << LayerMask.NameToLayer("Player 2"));
            dash = GameObject.FindGameObjectWithTag("Dash P1");
        }

        hitTextP1 = GameObject.FindGameObjectWithTag("Hit P1");
        hitTextP2 = GameObject.FindGameObjectWithTag("Hit P2");       
    }

    // Update is called once per frame
    void Update()
    {
        if(!isDead)
        {
            if (player.tag == "Player 1")
            {
                if (canMove)
                {
                    horizontalMove = Input.GetAxisRaw("Horizontal 1") * walkSpeed;
                    horizontalRun = 0;

                    animator.SetFloat("walkSpeed", Mathf.Abs(horizontalMove));
                    animator.SetFloat("runSpeed", Mathf.Abs(horizontalRun));

                    if (Input.GetButtonDown("Jump 1") && (currentStamina >= 25) && isGrounded)
                    {
                        Jump();
                    }
                    else if (Input.GetButtonDown("Jump 1") && (currentStamina < 25))
                    {
                        Show("Not enough stamina", Color.green);
                    }

                    if ((Input.GetAxis("Horizontal 1") < -0.1) && Input.GetKey(KeyCode.LeftShift) && currentStamina > 0 && isGrounded)
                    {
                        RunLeft();
                    }
                    if ((Input.GetAxis("Horizontal 1") > 0.1) && Input.GetKey(KeyCode.LeftShift) && currentStamina > 0 && isGrounded)
                    {
                        RunRight();
                    }
                }

                if (canDash && !isDashing)
                {
                    if (Input.GetButtonDown("Dash 1") && (currentStamina >= 20))
                    {
                        StartCoroutine(Dash());
                    }
                    else if (Input.GetButtonDown("Dash 1") && (currentStamina < 20))
                    {
                        Show("Not enough stamina", Color.green);
                    }

                }
                else if (Input.GetButtonDown("Dash 1") && !canDash)
                {
                    Show("On cooldown", Color.cyan);
                }

                if (isDashing)
                {
                    animator.SetBool("isSliding", true);
                    return;
                }

                if ((Input.GetButtonDown("Fire1")) && canAttack && cooldownAttack)
                {
                    StartCoroutine(Attack());                 
                }

                if ((Input.GetButton("Defen 1")) && currentStamina >= 30 && canDefen)
                {
                    canDefen = false;
                    canMove = false;
                    canDash = false;
                    canAttack = false;
                    canUseSkill = false;
                    isDefending = true;
                    animator.SetBool("isDefending", true);
                }
                else if ((Input.GetButtonDown("Defen 1")) && currentStamina < 30)
                {
                    Show("Not enough stamina", Color.green);
                }
                if (Input.GetButtonUp("Defen 1"))
                {
                    Defend();
                }
            }

            else if (player.tag == "Player 2")
            {
                if (canMove)
                {
                    horizontalMove = Input.GetAxisRaw("Horizontal 2") * walkSpeed;
                    horizontalRun = 0;

                    animator.SetFloat("walkSpeed", Mathf.Abs(horizontalMove));
                    animator.SetFloat("runSpeed", Mathf.Abs(horizontalRun));

                    if (Input.GetButtonDown("Jump 2") && (currentStamina >= 25) && isGrounded)
                    {
                        Jump();
                    }
                    else if (Input.GetButtonDown("Jump 2") && (currentStamina < 25))
                    {
                        Show("Not enough stamina", Color.green);
                    }

                    if ((Input.GetAxis("Horizontal 2") < -0.1) && Input.GetKey(KeyCode.RightShift) && currentStamina > 0 && isGrounded)
                    {
                        RunLeft();
                    }
                    if ((Input.GetAxis("Horizontal 2") > 0.1) && Input.GetKey(KeyCode.RightShift) && currentStamina > 0 && isGrounded)
                    {
                        RunRight();
                    }
                }

                if (canDash && !isDashing)
                {
                    if (Input.GetButtonDown("Dash 2") && (currentStamina >= 20))
                    {
                        StartCoroutine(Dash());
                    }
                    else if (Input.GetButtonDown("Dash 2") && (currentStamina < 20))
                    {
                        Show("Not enough stamina", Color.green);
                    }
                }
                else if (Input.GetButtonDown("Dash 2") && !canDash)
                {
                    Show("On cooldown", Color.cyan);
                }

                if (isDashing)
                {
                    animator.SetBool("isSliding", true);
                    return;
                }

                if ((Input.GetButtonDown("Fire2")) && canAttack && cooldownAttack)
                {
                    StartCoroutine(Attack());
                }

                if ((Input.GetButton("Defen 2")) && currentStamina >= 30 && canDefen)
                {
                    canDefen = false;
                    canMove = false;
                    canDash = false;
                    canAttack = false;
                    canUseSkill = false;
                    isDefending = true;
                    animator.SetBool("isDefending", true);
                }
                else if ((Input.GetButtonDown("Defen 2")) && currentStamina < 30)
                {
                    Show("Not enough stamina", Color.green);
                }
                if (Input.GetButtonUp("Defen 2"))
                {
                    Defend();
                }
            }
        }
    }

    void FixedUpdate()
    {     
        //Time controll
        time += Time.fixedDeltaTime;
        if (time > 2)
        {
            time = 0;
            count = 1;
        }

        stunTime -= Time.fixedDeltaTime;
        if(stunTime > 0 )
        {           
            isStun = true;
            canAttack = false;
            canMove = false;
            canDefen = false;
            canDash = false;
            canUseSkill = false;
            isDefending = false;
            animator.SetBool("isDefending", false);
        }
        else if(stunTime < 0 )
        {           
            stunTime = 0f;
            if(isStun)
            {
                isStun = false;
                canAttack = true;
                canMove = true;
                canDefen = true;               
                canUseSkill = true;
            }                
        }

        intoxicatedTime -= Time.fixedDeltaTime;
        if (intoxicatedTime > 0)
        {
            isIntoxicated = true;
            canMove = false;
            canDash = false;
            canUseSkill = false;
            canDefen = false;
            GetComponent<CharacterController>().canAttack = false;

            Vector2 newVector = followPoint.position - m_GroundCheck.position;
            if (newVector.x < 0)
            {
                GetComponent<CharacterMovement>().Move(-20 * Time.fixedDeltaTime, false, false);
                GetComponent<Animator>().SetFloat("walkSpeed", 20); ;
            }
            else
            {
                GetComponent<CharacterMovement>().Move(20 * Time.fixedDeltaTime, false, false);
                GetComponent<Animator>().SetFloat("walkSpeed", 20);
            }
        }
        else if (intoxicatedTime < 0)
        {
            intoxicatedTime = 0f;
            if (isIntoxicated && !isStun)
            {
                isIntoxicated = false;
                canMove = true;
                canDash = true;
                canUseSkill = true;
                canDefen = true;
                canAttack = true;
            }
        }

        timeCanNotAttack -= Time.fixedDeltaTime;
        if (timeCanNotAttack > 0)
        {
            canAttack = false;
        }
        else if (timeCanNotAttack < 0)
        {
            timeCanNotAttack = 0f;
            if (!isStun && !isDefending && !isIntoxicated)
            {
                canAttack = true;
            }
        }

        timeCanNotMove -= Time.fixedDeltaTime;
        if (timeCanNotMove > 0)
        {
            canMove = false;
        }
        else if (timeCanNotMove < 0)
        {
            timeCanNotMove = 0f;
            if (!isStun && !isDefending && !isIntoxicated)
            {
                canMove = true;
            }
        }

        dashTime -= Time.fixedDeltaTime;
        if( dashTime <= 0 )
        {
            dashTime = 0f;
            if (!isStun)
            {
                canDash = true;
            }               
        }
        else
        {
            canDash = false;
        }

        healTime -= Time.fixedDeltaTime;
        if(healTime <= 0 )
        {
            healTime = 0f;
            canHeal = true;
        }

        animator.SetFloat("stunTime", stunTime);
        if (player.tag == "Player 2" && checkP2)
        {
            StartCoroutine(P2Facing());
            movement.Move(-1f * Time.fixedDeltaTime, false, jump);

        }
        if (!canMove)
        {
            horizontalMove = 0;
            horizontalRun = 0;
            animator.SetFloat("walkSpeed", 0);
            animator.SetFloat("runSpeed", 0);
        }
        // Move our character
        movement.Move(horizontalMove * Time.fixedDeltaTime, false, jump);
        movement.Move(horizontalRun * Time.fixedDeltaTime, false, jump);
                
        animator.SetBool("isSliding", false);
        if (isDashing)
        {
            animator.SetBool("isSliding", true);
            return;
        }

        jump = false;
        isGrounded = false;
        animator.SetBool("isGrounded", false) ;
        Collider2D[] colliders = Physics2D.OverlapCircleAll(m_GroundCheck.position, k_GroundedRadius, m_WhatIsGround);
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].gameObject != this.gameObject)
            {
                isGrounded = true;
                animator.SetBool("isGrounded", true);
            }
        }

        if (horizontalMove > 0)
        {
            m_FacingRight = true;
        }
        if(horizontalMove <0)
        {
            m_FacingRight = false;
        }

        lerpSpeed = 0.1f * Time.deltaTime;
        currentStamina = Mathf.Lerp(currentStamina, maxStamina, lerpSpeed);

        if (currentPower > maxPower)
        {
            currentPower = maxPower;           
        }
        if (currentHealth <= 0)
        {
            canAttack = false;
            canDash = false;
            canDefen = false;
            canMove = false;
            canTakeDame = false;
            canStun = false;
            canUseSkill = false;
            if (!isDead)
            {
                currentHealth = 0;
                StartCoroutine(Die());
            }
        }
    }

    private void Jump()
    {
        float temp = this.transform.position.y;
        jump = true;
        animator.SetTrigger("isJumping");
        GameObject obj = Instantiate(jumpEffect, new Vector3(this.transform.position.x, temp, -2), Quaternion.Euler(-90, 0, 0));
        obj.transform.localScale = new Vector3(1f, 1f, 1f);
        Destroy(obj, 2f);
        currentStamina -= 10;
    }

    private void RunLeft()
    {
        horizontalRun = -2 * walkSpeed;
        animator.SetFloat("runSpeed", Mathf.Abs(horizontalRun));
        runTime -= Time.deltaTime;
        if (runTime < 0)
        {
            runTime = 0.1f;
            GameObject obj = Instantiate(runEffect, new Vector3(this.transform.position.x, m_GroundCheck.position.y, -2), Quaternion.Euler(0, 90, 0));
            obj.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
            Destroy(obj, 2f);
        }
        currentStamina -= 5f * Time.deltaTime;
    }

    private void RunRight()
    {

        horizontalRun = 2 * walkSpeed;
        animator.SetFloat("runSpeed", Mathf.Abs(horizontalRun));
        runTime -= Time.deltaTime;
        if (runTime < 0)
        {
            runTime = 0.1f;
            GameObject obj = Instantiate(runEffect, new Vector3(this.transform.position.x, m_GroundCheck.position.y, -2), Quaternion.Euler(0, -90, 0));
            obj.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
            Destroy(obj, 2f);
        }
        currentStamina -= 5f * Time.deltaTime;
    }

    private void Defend()
    {
        animator.SetBool("isDefending", false);
        isDefending = false;
        if (!isStun)
        {
            canDefen = true;
            canUseSkill = true;
            canDash = true;
            if (timeCanNotAttack <= 0)
            {
                canAttack = true;
            }
            if (timeCanNotMove <= 0)
            {
                canMove = true;
            }
        }
    }

    private IEnumerator Dash()
    {     
        isDashing = true;
        float originalGravity = rb.gravityScale;
        rb.gravityScale = 0f;

        if (!m_FacingRight)
        {
            rb.transform.position = new Vector2(rb.position.x - 6, rb.position.y);
        }
        else
        {
            rb.transform.position = new Vector2(rb.position.x + 6, rb.position.y);
        }

        currentStamina -= 20;
        dash.GetComponent<SkillCooldown>().UseSkill(dashCooldown);
        yield return new WaitForSeconds(dashingTime);

        rb.gravityScale = originalGravity;
        isDashing = false;
        dashTime = 10f;
    }

    private IEnumerator Attack()
    {
        cooldownAttack = false;
        isAttacking = true;
        float x = attackPoint.position.x;
        float y = attackPoint.position.y;

        if (m_FacingRight)
        {
            switch (count)
            {
                case 2:
                case 5:
                    animator.SetTrigger("isAttacking 2");
                    break;
                case 3:
                case 6:
                    animator.SetTrigger("isAttacking 3");
                    break;
                default:
                    animator.SetTrigger("isAttacking 1");
                    break;
            }
        }
        else
        {
            switch (count)
            {
                case 2:
                case 5:
                    animator.SetTrigger("isAttacking 2");
                    break;
                case 3:
                case 6:
                    animator.SetTrigger("isAttacking 3");
                    break;
                default:
                    animator.SetTrigger("isAttacking 1");
                    break;
            }
        }
        yield return new WaitForSeconds((1 / attackSpeed )* 0.3f);
        if (m_FacingRight)
        {
            switch (count)
            {
                case 2:case 5:
                    LoadEffect(slashEffect2, new Vector3(x - 1 + attackRange, y, transform.position.z - 2), Quaternion.Euler(0, 0, 0), 3f, new Vector3(1f, 1f, 1f));
                    break;
                case 3:
                case 6:
                    LoadEffect(slashEffect3, new Vector3(x + attackRange - 2, y, transform.position.z - 2), Quaternion.Euler(0, 0, -90), 3f, new Vector3(1f, 1f, 1f));
                    break;
                case 7:
                    ManySlashEffect();
                    break;
                default:
                    LoadEffect(slashEffect, new Vector3(x - 1 + attackRange, y, transform.position.z - 2), Quaternion.Euler(0, 0, 0), 3f, new Vector3(1f, 1f, 1f));
                    break;
            }            
        }
        else
        {
            switch (count)
            {
                case 2:
                case 5:
                    LoadEffect(slashEffect2, new Vector3(x - attackRange + 1, y, transform.position.z - 2), Quaternion.Euler(180, 0, 180), 3f, new Vector3(1f, 1f, 1f));
                    break;
                case 3:
                case 6:
                    LoadEffect(slashEffect3, new Vector3(x - attackRange + 2, y, transform.position.z - 2), Quaternion.Euler(180, 0,90), 3f, new Vector3(1f, 1f, 1f));
                    break;
                case 7:
                    ManySlashEffect();
                    break;
                default:
                    LoadEffect(slashEffect, new Vector3(x - attackRange / 4, y, transform.position.z - 2), Quaternion.Euler(180, 0, 180), 3f, new Vector3(1f, 1f, 1f));
                    break;
            }           
        }        
        Collider2D[] hits = Physics2D.OverlapCircleAll(new Vector2(x,y), attackRange, enemyLayers);
        foreach (Collider2D hit in hits)
        {
            dashTime -= 0.5f;
            dash.GetComponent<SkillCooldown>().coolDownTimer = dashTime;
            if (!isGrounded)
            {
                StartCoroutine(OnTheAir());
;           }
            time = 0f;
            if (m_FacingRight)
            {
                LoadEffect(hitEffect, new Vector3(x + Random.Range(1.8f, 2.2f), ((m_GroundCheck.position.y + m_CeilingCheck.position.y) / 2) + Random.Range(-0.2f, 0.2f), transform.position.z - 2), Quaternion.Euler(0, 0, 0), 1f, new Vector3(1f, 1f, 1f));
            }
            else
            {
                LoadEffect(hitEffect, new Vector3(x - Random.Range(1.8f, 2.2f), ((m_GroundCheck.position.y + m_CeilingCheck.position.y) / 2) + Random.Range(-0.2f, 0.2f), transform.position.z - 2), Quaternion.Euler(0, 0, 0), 1f, new Vector3(1f, 1f, 1f));
            }           
            if (hit.GetComponent<CharacterController>().canReflect)
            {
                TakeDamage((int)((float)attackDmg * 0.3f));
            }
            if (count == 3)
            {
                if (m_FacingRight)
                {
                    hit.GetComponent<Rigidbody2D>().AddForce(new Vector2(hit.GetComponent<Rigidbody2D>().mass * 200, hit.GetComponent<Rigidbody2D>().mass * 500));
                }
                else
                {
                    hit.GetComponent<Rigidbody2D>().AddForce(new Vector2(hit.GetComponent<Rigidbody2D>().mass * -200,hit.GetComponent<Rigidbody2D>().mass * 500));
                }
                hit.GetComponent<CharacterController>().TakeDamage((int)(attackDmg*1.3));
                if(canHealWhenATK)
                {
                    healthWhenATK(attackDmg * 0.3f);
                }
            }
            else if (count == 6)
            {
                if (m_FacingRight)
                {
                    hit.GetComponent<Rigidbody2D>().AddForce(new Vector2(hit.GetComponent<Rigidbody2D>().mass * 50, hit.GetComponent<Rigidbody2D>().mass * 1000));
                }
                else
                {
                    hit.GetComponent<Rigidbody2D>().AddForce(new Vector2(hit.GetComponent<Rigidbody2D>().mass * -50, hit.GetComponent<Rigidbody2D>().mass * 1000));

                }
                hit.GetComponent<CharacterController>().TakeDamage(attackDmg * 2);
                if (canHealWhenATK)
                {
                    healthWhenATK(attackDmg * 1f);
                }
            }
            else if (count == 7)
            {
                if (hit.GetComponent<CharacterController>().isGrounded)
                {
                    hit.GetComponent<CharacterController>().TakeDamage(attackDmg * 3);
                    if (canHealWhenATK)
                    {
                        healthWhenATK(attackDmg * 2f);
                    }
                    if (m_FacingRight)
                    {
                        hit.GetComponent<Rigidbody2D>().velocity = new Vector2(400, 5);
                        hit.GetComponent<CharacterController>().Stun(2f, 0,0);
                    }
                    else
                    {
                        hit.GetComponent<Rigidbody2D>().velocity = new Vector2(-400, 5);
                        hit.GetComponent<CharacterController>().Stun(2f,0,0);
                    }
                }
                else
                {
                    hit.GetComponent<CharacterController>().TakeDamage(attackDmg * 3);
                    if (canHealWhenATK)
                    {
                        healthWhenATK(attackDmg * 2f);
                    }
                    hit.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
                    hit.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
                    if (m_FacingRight)
                    {
                        hit.GetComponent<Rigidbody2D>().velocity = new Vector2(200, -200);
                        hit.GetComponent<CharacterController>().Stun(2f,0,0);
                    }
                    else
                    {
                        hit.GetComponent<Rigidbody2D>().velocity = new Vector2(-200, -200);
                        hit.GetComponent<CharacterController>().Stun(2f, 0, 0);
                    }
                }                
            }
            else
            {
                hit.GetComponent<CharacterController>().TakeDamage(attackDmg);
            }
            currentPower += 13;
            if (hit.GetComponent<CharacterController>().isBleeding)
            {
                StartCoroutine(hit.GetComponent<CharacterController>().Hemorrhage());
                hit.GetComponent<CharacterController>().isBleeding = false;
            }
            count++;
            if (count > 7)
            {
                count = 1;
            }
        }   
        if(hits.Length == 0)
        {
            count = 1;
            if(player.tag == "Player 1")
            {
                hitTextP1.GetComponent<HitCount>().count = 0;
                hitTextP1.GetComponent<HitCount>().Show();
            }
            else if (player.tag == "Player 2")
            {
                hitTextP2.GetComponent<HitCount>().count = 0;
                hitTextP2.GetComponent<HitCount>().Show();
            }
        }
        isAttacking = false;
        yield return new WaitForSeconds(1/attackSpeed);       
        cooldownAttack = true;
    }

    private IEnumerator OnTheAir()
    {
        rb.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
        yield return new WaitForSeconds(0.5f);
        rb.constraints = RigidbodyConstraints2D.None;
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
    }
    private IEnumerator P2Facing()
    {
        yield return new WaitForSeconds(1f);
        checkP2 = false;
    }
    public void Stun(float time, float x, float y)
    {
        if(canStun)
        {
            GameObject obj = Instantiate(jumpEffect, new Vector3(transform.position.x, transform.position.y, -2), Quaternion.Euler(-90, 0, 0));
            obj.transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
            Destroy(obj, 3f);

            Show("Stun", Color.magenta);
            animator.SetTrigger("isStun");
            stunTime += time;

            LoadEffect(stunEffect, m_CeilingCheck.position, Quaternion.Euler(-90, 0, 0), stunTime + 0.2f, new Vector3(2f, 2f, 2f));
            LoadEffect(stunHitEffect, m_GroundCheck.position, Quaternion.Euler(-90, 0, 0), 1f, Vector3.one);
            GetComponent<Rigidbody2D>().AddForce(new Vector3(x, y));
        }
    }
    
    public void Intoxicated(float time, Transform point)
    {
        if(canStun)
        {
            intoxicatedTime += time;
            followPoint = point;
            LoadEffect(intoxicatedEffect, m_CeilingCheck.position, Quaternion.Euler(-90, 0, 0), intoxicatedTime, new Vector3(2f, 2f, 2f));
        }
    }

    public void canNotAttack(float time)
    {
        timeCanNotAttack = time;
    }
    public void canNotMove(float time)
    {
        timeCanNotMove = time;
    }
    public void TakeDamage(int dmg)
    {
        if(canTakeDame)
        {
            Vector2 newVector = GetComponent<CharacterController>().m_GroundCheck.position - enemy.GetComponent<CharacterController>().m_GroundCheck.position;
            if((isDefending) && ((!m_FacingRight && newVector.x > 0) || (m_FacingRight && newVector.x < 0)) && currentStamina >= 30)
            {
                Show("0", Color.white);
                enemy.GetComponent<CharacterController>().canNotAttack(0.7f);
                enemy.GetComponent<CharacterController>().count = 0;
                rb.AddForce(newVector * 1000);
                currentStamina -= 30;
                if (player.tag == "Player 1")
                {
                    hitTextP2.GetComponent<HitCount>().count = 0;
                    hitTextP2.GetComponent<HitCount>().Show();
                }
                else if (player.tag == "Player 2")
                {
                    hitTextP1.GetComponent<HitCount>().count = 0;
                    hitTextP1.GetComponent<HitCount>().Show();
                }
                if(m_FacingRight)
                {
                    LoadEffect(defenEffect, new Vector3(attackPoint.position.x, attackPoint.position.y,-2), Quaternion.Euler(0, -90, 0), 2f, new Vector3(1f, 1f, 1f));
                }
                else
                {
                    LoadEffect(defenEffect, new Vector3(attackPoint.position.x, attackPoint.position.y, -2), Quaternion.Euler(0, 90, 0), 2f, new Vector3(1f, 1f, 1f));                   
                }
            }
            else
            {
                if ((isDefending) && currentStamina <= 30)
                {
                    Stun(1.5f, newVector.x, newVector.y);
                }
                if (!isGrounded)
                {
                    StartCoroutine(OnTheAir());
                }
                canNotAttack(0.3f);
                int temp = dmg - (dmg * armor / 100);
                if (temp <= 60)
                {
                    LoadEffect(hurtEffect1, new Vector3(this.transform.position.x, m_CeilingCheck.position.y - 2, transform.position.z - 2), Quaternion.Euler(0, 0, 0), 3f, new Vector3(1, 1, 1));
                }
                else if ((60 < temp) && (temp <= 120))
                {
                    LoadEffect(hurtEffect2, new Vector3(this.transform.position.x, m_CeilingCheck.position.y - 2, transform.position.z - 2), Quaternion.Euler(-90, 0, 0), 3f, new Vector3(1, 1, 1));
                }
                else
                {
                    LoadEffect(hurtEffect3, new Vector3(this.transform.position.x, m_CeilingCheck.position.y - 2, transform.position.z - 2), Quaternion.Euler(-90, 0, 0), 3f, new Vector3(1, 1, 1));
                }

                currentPower += (int)(dmg * 0.2);
                animator.SetTrigger("Hurt");
                currentHealth -= temp;
                Show((dmg - (dmg * armor / 100)).ToString(), Color.red);

                if (player.tag == "Player 2")
                {
                    hitTextP1.GetComponent<HitCount>().count ++;
                    hitTextP1.GetComponent<HitCount>().Show();
                    hitTextP2.GetComponent<HitCount>().count = 0;
                    hitTextP2.GetComponent<HitCount>().Show();
                }
                else if (player.tag == "Player 1")
                {
                    hitTextP2.GetComponent<HitCount>().count ++;
                    hitTextP2.GetComponent<HitCount>().Show();
                    hitTextP1.GetComponent<HitCount>().count = 0;
                    hitTextP1.GetComponent<HitCount>().Show();
                }

                if (currentHealth <= 0)
                {
                    currentHealth = 0;
                    StartCoroutine(Die());
                }
            }      
        }     
    }
    public void TakeTrueDamage(int dmg)
    {
        if (canTakeDame)
        {
            if (!isGrounded)
            {
                StartCoroutine(OnTheAir());
            }
            canNotAttack(0.3f);
            if (dmg <= 5)
            {
                LoadEffect(hurtEffect1, new Vector3(this.transform.position.x, m_CeilingCheck.position.y - 2, transform.position.z - 2), Quaternion.Euler(0, 0, 0), 3f, new Vector3(1, 1, 1));
            }
            else if ((5 < dmg) && (dmg <= 20))
            {
                LoadEffect(hurtEffect2, new Vector3(this.transform.position.x, m_CeilingCheck.position.y - 2, transform.position.z - 2), Quaternion.Euler(-90, 0, 0), 3f, new Vector3(1, 1, 1));
            }
            else
            {
                LoadEffect(hurtEffect3, new Vector3(this.transform.position.x, m_CeilingCheck.position.y - 2, transform.position.z - 2), Quaternion.Euler(-90, 0, 0), 3f, new Vector3(1, 1, 1));
            }

            currentPower += (int)(dmg * 1.7);
            animator.SetTrigger("Hurt");
            currentHealth -= dmg;
            Show((dmg).ToString(), Color.white);

            if (player.tag == "Player 2")
            {
                hitTextP1.GetComponent<HitCount>().count++;
                hitTextP1.GetComponent<HitCount>().Show();
                hitTextP2.GetComponent<HitCount>().count = 0;
                hitTextP2.GetComponent<HitCount>().Show();
            }
            else if (player.tag == "Player 1")
            {
                hitTextP2.GetComponent<HitCount>().count++;
                hitTextP2.GetComponent<HitCount>().Show();
                hitTextP1.GetComponent<HitCount>().count = 0;
                hitTextP1.GetComponent<HitCount>().Show();
            }
        }
    }  

    public IEnumerator Die()
    {
        animator.SetBool("isDead", true);
        isDead = true;       

        yield return new WaitForSeconds(1f);
        while (true)
        {
            if(isGrounded)
            {
                float temp = this.transform.position.y;

                //dieSound.Play();

                this.GetComponent<Collider2D>().enabled = false;

                cpn = this.GetComponentsInChildren<PolygonCollider2D>();
                foreach (PolygonCollider2D c in cpn)
                {
                    c.enabled = false;
                }
                rb.drag = 10f;
                rb.gravityScale = 0f;
                this.enabled = false;
                yield return new WaitForSeconds(0.5f);
                LoadEffect(dieEffect, new Vector3(this.transform.position.x, temp, transform.position.z - 2), Quaternion.Euler(-90, 0, 0), 3f, new Vector3(2, 2, 2));
                break;
            }
        }
    }

    public void Show(string text, UnityEngine.Color color)
    {
        if (textPopup)
        {
            GameObject pbObject = Instantiate(textPopup, new Vector3(m_CeilingCheck.position.x + Random.Range(-0.2f,0.2f), m_CeilingCheck.position.y - 4.5f, m_CeilingCheck.position.z), Quaternion.identity);
            pbObject.GetComponentInChildren<TMPro.TextMeshPro>().text = text;
            pbObject.GetComponentInChildren<TMPro.TextMeshPro>().color = color;
            pbObject.transform.parent = this.transform;
        }
    }

    public void LoadEffect(GameObject effect, Vector3 vector3, Quaternion euler, float time, Vector3 scale)
    {      
        GameObject gObject = Instantiate(effect, vector3, euler);
        gObject.transform.localScale = scale;
        gObject.transform.parent = this.transform;
        Destroy(gObject, time);
    }

    public void IncreaseATK(int atk)
    {
        attackDmg += atk;
        Show("ATK + " + atk.ToString(), Color.yellow);       
        LoadEffect(atkUpEffect, new Vector3(this.transform.position.x, m_CeilingCheck.position.y + 1, transform.position.z - 2), Quaternion.Euler(-90, 0, 0), 3f, new Vector3(1, 1, 1));
    }
    public void DecreaseATK(int atk)
    {
        attackDmg -= atk;
        Show("ATK - " + atk.ToString(), Color.gray);
    }
    public void IncreaseArmor(int ammor)
    {
        armor += ammor;
        Show("Armor + " + ammor.ToString(), Color.cyan);
        LoadEffect(ammorUpEffect, new Vector3(this.transform.position.x, m_CeilingCheck.position.y + 1, transform.position.z - 2), Quaternion.Euler(-90, 0, 0), 3f, new Vector3(1, 1, 1));
    }
    public void DecreaseArmor(int ammor)
    {
        armor -= ammor;
        Show("Armor - " + ammor.ToString(), Color.gray);
    }
    public void IncreaseATKSpeed(float speed)
    {
        attackSpeed += speed;
        Show("ATK Speed + " + speed.ToString(), Color.magenta);
        LoadEffect(atkSpeedUpEffect, new Vector3(this.transform.position.x, m_CeilingCheck.position.y + 1, transform.position.z - 2), Quaternion.Euler(-90, 0, 0), 3f, new Vector3(1, 1, 1));
    }
    public void DecreaseATKSpeed(float speed)
    {
        attackSpeed -= speed;
        Show("ATK Speed - " + speed.ToString(), Color.gray);
    }
    public void IncreaseSpeed(int speed)
    {
        walkSpeed += speed;
        Show("Speed + " + speed.ToString(), Color.cyan);
        LoadEffect(speedUpEffect, new Vector3(this.transform.position.x, m_CeilingCheck.position.y + 1, transform.position.z - 2), Quaternion.Euler(-90, 0, 0), 3f, new Vector3(1, 1, 1));
    }
    public void DecreaseSpeed(int speed)
    {
        walkSpeed -= speed;
        Show("Speed - " + speed.ToString(), Color.gray);
    }
    public void healthSkill(int health)
    {
        if (canHeal)
        {
            currentHealth += health;
            if (currentHealth > maxHealth)
            {
                currentHealth = maxHealth;
            }
            Show(health.ToString(), Color.green);
            LoadEffect(healEffect, new Vector3(this.transform.position.x, (m_GroundCheck.position.y + m_CeilingCheck.position.y) / 2, transform.position.z - 2), Quaternion.Euler(-90, 0, 0), 3f, new Vector3(3, 3, 3));
            canHeal = false;
            healTime = .5f;
        }
    }
    public void healthWhenATK(float heal)
    {
        currentHealth += (int)(heal);
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
        Show(((int)(heal)).ToString(), Color.green);
        LoadEffect(healEffect, new Vector3(this.transform.position.x, (m_GroundCheck.position.y + m_CeilingCheck.position.y)/2, transform.position.z - 2), Quaternion.Euler(-90, 0, 0), 3f, new Vector3(3, 3, 3));
    }

    public IEnumerator Hemorrhage()
    {
       int tmp = maxHealth / 100;     
       for (int i = 0; i < 10; i++)
       {
            GameObject gObject = Instantiate(bleedEffect, new Vector3(this.transform.position.x, m_CeilingCheck.position.y - 2, transform.position.z - 2), Quaternion.Euler(-90, 0, 0));
            gObject.transform.parent = this.transform;
            Destroy(gObject,1f);
            currentHealth -= tmp;
            Show(tmp.ToString(), Color.red);
            yield return new WaitForSeconds(1f);
       }          
    }

    public void ManySlashEffect()
    {
        for (int i = 0; i < 7; i++)
        {
            if(m_FacingRight)
            {
                LoadEffect(slashEffect, new Vector3(attackPoint.transform.position.x + Random.Range(-1, 1), attackPoint.position.y + Random.Range(-1, 1), transform.position.z - 2), Quaternion.Euler(Random.Range(-180, 180), Random.Range(-180, 180), Random.Range(-180, 180)), 1f, new Vector3(1.5f, 1.5f, 1.5f));
            }
            else
            {
                LoadEffect(slashEffect, new Vector3(attackPoint.transform.position.x - Random.Range(-1, 1), attackPoint.position.y + Random.Range(-1, 1), transform.position.z - 2), Quaternion.Euler(Random.Range(-180, 180), Random.Range(-180, 180), Random.Range(-180, 180)), 1f, new Vector3(1.5f, 1.5f, 1.5f));
            }
        }
    }
}
