using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class CharacterMovement : MonoBehaviour
{
    public CharacterController2D controller;
    public Animator animator;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private TrailRenderer tr;
    [SerializeField] private LayerMask m_WhatIsGround;
    [SerializeField] private Transform m_GroundCheck;
    public Transform attackPoint;
    public float attackRange = 0.5f;
    public LayerMask enemyLayers;

    private float walkSpeed = 8f;

    float horizontalMove = 0f;
    float horizontalRun = 0f;
    bool jump = false;

    const float k_GroundedRadius = .2f;
    private bool canDash = true;
    private bool isDashing;
    private float dashingPower = 25f;
    private float dashingTime = 0.1f;
    private float dashCooldown = 1f;
    private bool isGrounded = false;
    private bool m_FacingRight = true;
    int attackDmg = 20;

    private void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        horizontalMove = Input.GetAxisRaw("Horizontal") * walkSpeed;
        horizontalRun = 0;

        animator.SetFloat("walkSpeed", Mathf.Abs(horizontalMove));
        animator.SetFloat("runSpeed", Mathf.Abs(horizontalRun));

        if (Input.GetButtonDown("Jump"))
        {
            jump = true;
            animator.SetBool("isJumping", true);
        }

        if (Input.GetKeyDown(KeyCode.E) && canDash)
        {
            StartCoroutine(Dash());
        }

        if (isDashing)
        {
            animator.SetBool("isSliding", true);
            return;
        }

        if ((Input.GetKey(KeyCode.LeftArrow) && Input.GetKey(KeyCode.RightShift)))
        {
            horizontalRun = -25f;
            animator.SetFloat("runSpeed", Mathf.Abs(horizontalRun));
        }
        if ((Input.GetKey(KeyCode.RightArrow) && Input.GetKey(KeyCode.RightShift)))
        {
            horizontalRun = 25f;
            animator.SetFloat("runSpeed", Mathf.Abs(horizontalRun));
        }

        if ((Input.GetKeyDown(KeyCode.Keypad0)))
        {
            animator.SetBool("isAttacking", true);
            Attack();
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
                rb.transform.position = new Vector2(rb.position.x, rb.position.y + 0.7f);
                rb.velocity = new Vector2(-1 * dashingPower, 0f);
            }
            else 
            {
                rb.transform.position = new Vector2(rb.position.x, rb.position.y + 0.7f);
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

        tr.emitting = true;
        yield return new WaitForSeconds(dashingTime);
        rb.gravityScale = originalGravity;
        isDashing = false;
        yield return new WaitForSeconds(dashCooldown);
        tr.emitting = false;
        canDash = true; 
    }

    public void Attack()
    {
        Collider2D[] hit = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);
        foreach(Collider2D enemy in hit)
        {
            enemy.GetComponent<Enemy>().TakeDamage(attackDmg);
        }
    }
    public void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }

}
