using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditorInternal;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class CharacterMovement : MonoBehaviour
{
    public CharacterController2D controller;
    public Animator animator;
    GameObject player;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private TrailRenderer tr;
    [SerializeField] private LayerMask m_WhatIsGround;
    [SerializeField] private Transform m_GroundCheck;
    [SerializeField] private GameObject textPopup;
    private Component[] cpn;
    public Transform attackPoint;
    public float attackRange = 0.5f;
    public LayerMask enemyLayers;

    public float walkSpeed = 8f;
    public int maxHealth = 100;
    public int currentHealth;
    public float maxStamina = 100;
    public float currentStamina;
    public int maxPower = 100;
    public int currentPower;
    public int attackDmg = 10;
    public float attackSpeed = 1f;
    public int armor = 20;

    float horizontalMove = 0f;
    float horizontalRun = 0f;
    bool jump = false;

    const float k_GroundedRadius = .2f;
    private bool canDash = true;
    private bool canAttack = true;
    private bool isDashing;
    private float dashingPower = 25f;
    private float dashingTime = 0.1f;
    private float dashCooldown = 5f;
    private bool isGrounded = false;
    private bool m_FacingRight = true;
    private float lerpSpeed;


    private void Start()
    {
        player = this.gameObject;
        currentHealth = maxHealth;
        currentStamina = maxStamina;
        currentPower = 0;
        animator.SetBool("isDead", false);
    }

    // Update is called once per frame
    void Update()
    {
        if (player.tag == "Player 1")
        {

            horizontalMove = Input.GetAxisRaw("Horizontal 1") * walkSpeed;
            horizontalRun = 0;

            animator.SetFloat("walkSpeed", Mathf.Abs(horizontalMove));
            animator.SetFloat("runSpeed", Mathf.Abs(horizontalRun));

            if (Input.GetButtonDown("Jump 1") && (currentStamina >= 25))
            {
                jump = true;
                animator.SetBool("isJumping", true);
                currentStamina -= 25;

            } else if (Input.GetButtonDown("Jump 1") && (currentStamina < 25))
            {
                Show("Not enough stamina");

            }

            if (Input.GetButtonDown("Dash 1") && canDash && (currentStamina >= 50))
            {
                StartCoroutine(Dash());
            }
            else if (Input.GetButtonDown("Dash 1") && (currentStamina < 50))
            {
                Show("Not enough stamina");

            }

            if (isDashing)
            {
                animator.SetBool("isSliding", true);
                return;
            }

            if ((Input.GetAxis("Horizontal 1") < -0.1) && Input.GetKey(KeyCode.LeftShift) && currentStamina > 0)
            {
                horizontalRun = -5 * walkSpeed;
                animator.SetFloat("runSpeed", Mathf.Abs(horizontalRun));
                currentStamina -= 15f * Time.deltaTime;
            }
            if ((Input.GetAxis("Horizontal 1") > 0.1) && Input.GetKey(KeyCode.LeftShift) && currentStamina > 0)
            {
                horizontalRun = 5 * walkSpeed;
                animator.SetFloat("runSpeed", Mathf.Abs(horizontalRun));
                currentStamina -= 15f * Time.deltaTime;
            }

            if ((Input.GetButton("Fire1")) && canAttack)
            {             
                StartCoroutine(Attack());
            }

            if ((Input.GetKeyDown(KeyCode.K)))
            {
                animator.SetBool("isDefending", true);
            }

            if ((Input.GetKey(KeyCode.Alpha1)))
            {
                animator.SetBool("isCasting1", true);
            }
            if ((Input.GetKey(KeyCode.Alpha2)))
            {
                animator.SetBool("isCasting2", true);
            }
            if ((Input.GetKey(KeyCode.Alpha3)))
            {
                animator.SetBool("isCasting3", true);
            }
        }
        else if (player.tag == "Player 2")
        {
            horizontalMove = Input.GetAxisRaw("Horizontal 2") * walkSpeed;
            horizontalRun = 0;

            animator.SetFloat("walkSpeed", Mathf.Abs(horizontalMove));
            animator.SetFloat("runSpeed", Mathf.Abs(horizontalRun));

            if (Input.GetButtonDown("Jump 2") && (currentStamina >= 25))
            {
                jump = true;
                animator.SetBool("isJumping", true);
                currentStamina -= 25;

            }
            else if (Input.GetButtonDown("Jump 2") && (currentStamina < 25))
            {
                Show("Not enough stamina");

            }

            if (Input.GetButtonDown("Dash 2") && canDash && (currentStamina >= 50))
            {
                StartCoroutine(Dash());
            }
            else if (Input.GetButtonDown("Dash 2") && (currentStamina < 50))
            {
                Show("Not enough stamina");

            }

            if (isDashing)
            {
                animator.SetBool("isSliding", true);
                return;
            }

            if ((Input.GetAxis("Horizontal 2") < -0.1) && Input.GetKey(KeyCode.RightShift) && currentStamina > 0)
            {
                horizontalRun = -5 * walkSpeed;
                animator.SetFloat("runSpeed", Mathf.Abs(horizontalRun));
                currentStamina -= 15f * Time.deltaTime;
            }
            if ((Input.GetAxis("Horizontal 2") > 0.1) && Input.GetKey(KeyCode.RightShift) && currentStamina > 0)
            {
                horizontalRun = 5 * walkSpeed;
                animator.SetFloat("runSpeed", Mathf.Abs(horizontalRun));
                currentStamina -= 15f * Time.deltaTime;
            }

            if ((Input.GetButton("Fire2")) && canAttack)
            {
                StartCoroutine(Attack());
            }

            //if ((Input.GetMouseButton(1)))
            //{
            //    animator.SetBool("isDefending", true);
            //}

            //if ((Input.GetKey(KeyCode.Alpha1)))
            //{
            //    animator.SetBool("isCasting1", true);
            //}
            //if ((Input.GetKey(KeyCode.Alpha2)))
            //{
            //    animator.SetBool("isCasting2", true);
            //}
            //if ((Input.GetKey(KeyCode.Alpha3)))
            //{
            //    animator.SetBool("isCasting3", true);
            //}
        }

    }

    public void OnLanding()
    {
        animator.SetBool("isJumping", false);
        isGrounded = true;
    }

    void FixedUpdate()
    {      

        // Move our character
        controller.Move(horizontalMove * Time.fixedDeltaTime, false, jump);
        controller.Move(horizontalRun * Time.fixedDeltaTime, false, jump);

        if (rb.velocity.y < -0.1)
        {
            animator.SetBool("isFalling", true);
        }
        else
        {
            animator.SetBool("isFalling", false);
        }
        jump = false;
        isGrounded = false;

        animator.SetBool("isSliding", false);
        if (isDashing)
        {
            animator.SetBool("isSliding", true);
            return;
        }

        Collider2D[] colliders = Physics2D.OverlapCircleAll(m_GroundCheck.position, k_GroundedRadius, m_WhatIsGround);
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].gameObject != gameObject)
            {
                isGrounded = true;
            }
        }

        if(horizontalMove > 0)
        {
            m_FacingRight = true;
        }
        if(horizontalMove <0)
        {
            m_FacingRight = false;
        }

        animator.SetBool("isAttacking", false);
        animator.SetBool("isDefending", false);
        animator.SetBool("isCasting1", false);
        animator.SetBool("isCasting2", false);
        animator.SetBool("isCasting3", false);

        lerpSpeed = 0.1f * Time.deltaTime;
        currentStamina = Mathf.Lerp(currentStamina, maxStamina, lerpSpeed);

        if (currentPower > maxPower)
        {
            currentPower = maxPower;           
        }

    }

    private IEnumerator Dash()
    {
        canDash = false;
        isDashing = true;
        float originalGravity = rb.gravityScale;
        rb.gravityScale = 0f;

        if(isGrounded)
        {            
            if (!m_FacingRight)
            {
                rb.transform.position = new Vector2(rb.position.x, rb.position.y + 0.2f);
                rb.velocity = new Vector2(-1 * dashingPower, 0f);
            }
            else 
            {
                rb.transform.position = new Vector2(rb.position.x, rb.position.y + 0.2f);
                rb.velocity = new Vector2(dashingPower, 0f);
            }
        }
        else
        {
            if (!m_FacingRight)
            {
                rb.velocity = new Vector2(-1 * dashingPower * 0.4f, 0f);
            }
            else 
            {
                rb.velocity = new Vector2(dashingPower * 0.4f, 0f);
            }
        }

        currentStamina -= 50;
        tr.emitting = true;
        yield return new WaitForSeconds(dashingTime);
        rb.gravityScale = originalGravity;
        isDashing = false;
        yield return new WaitForSeconds(dashCooldown);
        tr.emitting = false;
        canDash = true; 
    }

    private IEnumerator Attack()
    {
        canAttack = false;
        animator.SetBool("isAttacking", true);
        Collider2D[] hit = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);
        foreach (Collider2D enemy in hit)
        {
            enemy.GetComponent<CharacterMovement>().TakeDamage(attackDmg);
            currentPower += 13;
        }
        yield return new WaitForSeconds(1/attackSpeed);
        canAttack = true;
    }

    public void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }

    public void TakeDamage(int dmg)
    {
        currentPower += 17;
        animator.SetTrigger("Hurt");
        currentHealth -= dmg - (dmg * armor/100);
        Show((dmg - (dmg * armor / 100)).ToString());

        if (currentHealth <= 0)
        {
            currentHealth = 0;
            Die();
        }
    }

    void Die()
    {
        animator.SetBool("isDead", true);

        this.GetComponent<Collider2D>().enabled = false;

        cpn = this.GetComponentsInChildren<PolygonCollider2D>();
        foreach (PolygonCollider2D c in cpn)
        {
            c.enabled = false;
        }
        rb.drag = 10f;
        rb.gravityScale = 0f;
        this.enabled = false;
    }

    void Show(string text)
    {
        if(textPopup)
        {
            GameObject pbObject = Instantiate(textPopup, transform.position, Quaternion.identity);
            pbObject.GetComponentInChildren<TMPro.TextMeshPro>().text = text;
        }
        
    }

}
