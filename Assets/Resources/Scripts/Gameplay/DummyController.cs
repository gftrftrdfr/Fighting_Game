using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DummyController : MonoBehaviour
{
    Animator animator;
    private Rigidbody2D rb;
    public LayerMask m_WhatIsGround;
    public Transform m_GroundCheck;
    public Transform m_CeilingCheck;

    [SerializeField] private GameObject hurtEffect1;
    [SerializeField] private GameObject hurtEffect2;
    [SerializeField] private GameObject hurtEffect3;
    [SerializeField] private GameObject stunEffect;
    [SerializeField] private GameObject stunHitEffect;

    public int maxHealth = 100;
    public int currentHealth;

    public bool isGrounded;

    GameObject enemy;
    // Start is called before the first frame update
    //void Start()
    //{
    //    enemy = GameObject.FindGameObjectWithTag("Player 1");
    //}

    //// Update is called once per frame
    //void Update()
    //{
    //    isGrounded = false;
    //    animator.SetBool("isGrounded", false);
    //    Collider2D[] colliders = Physics2D.OverlapCircleAll(m_GroundCheck.position, 0.2f, m_WhatIsGround);
    //    for (int i = 0; i < colliders.Length; i++)
    //    {
    //        if (colliders[i].gameObject != this.gameObject)
    //        {
    //            isGrounded = true;
    //            animator.SetBool("isGrounded", true);
    //        }
    //    }
    //}

    //public void TakeDamage(int dmg)
    //{
    //    Vector2 newVector = GetComponent<CharacterController>().m_GroundCheck.position - enemy.GetComponent<CharacterController>().m_GroundCheck.position;
    //    else
    //    {
    //        if (!isGrounded)
    //        {
    //            StartCoroutine(OnTheAir());
    //        }
    //        canNotAttack(0.5f);
    //        int temp = dmg - (dmg * armor / 100);
    //        if (temp <= 60)
    //        {
    //            LoadEffect(hurtEffect1, new Vector3(this.transform.position.x, m_CeilingCheck.position.y - 2, transform.position.z - 2), Quaternion.Euler(0, 0, 0), 3f, new Vector3(1, 1, 1));
    //        }
    //        else if ((60 < temp) && (temp <= 120))
    //        {
    //            LoadEffect(hurtEffect2, new Vector3(this.transform.position.x, m_CeilingCheck.position.y - 2, transform.position.z - 2), Quaternion.Euler(-90, 0, 0), 3f, new Vector3(1, 1, 1));
    //        }
    //        else
    //        {
    //            LoadEffect(hurtEffect3, new Vector3(this.transform.position.x, m_CeilingCheck.position.y - 2, transform.position.z - 2), Quaternion.Euler(-90, 0, 0), 3f, new Vector3(1, 1, 1));
    //        }

    //        currentPower += (int)(dmg * 0.12f);
    //        animator.SetTrigger("Hurt");
    //        currentHealth -= temp;
    //        Show((dmg - (dmg * armor / 100)).ToString(), Color.red);

    //        if (tag == "Player 2")
    //        {
    //            hitTextP1.GetComponent<HitCount>().count++;
    //            hitTextP1.GetComponent<HitCount>().Show();
    //            hitTextP2.GetComponent<HitCount>().count = 0;
    //            hitTextP2.GetComponent<HitCount>().Show();
    //        }
    //        else if (tag == "Player 1")
    //        {
    //            hitTextP2.GetComponent<HitCount>().count++;
    //            hitTextP2.GetComponent<HitCount>().Show();
    //            hitTextP1.GetComponent<HitCount>().count = 0;
    //            hitTextP1.GetComponent<HitCount>().Show();
    //        }
    //    }
    //}
    //public void TakeTrueDamage(int dmg)
    //{
    //    if (canTakeDame)
    //    {
    //        if (!isGrounded)
    //        {
    //            StartCoroutine(OnTheAir());
    //        }
    //        canNotAttack(0.7f);
    //        if (dmg <= 5)
    //        {
    //            LoadEffect(hurtEffect1, new Vector3(this.transform.position.x, m_CeilingCheck.position.y - 2, transform.position.z - 2), Quaternion.Euler(0, 0, 0), 3f, new Vector3(1, 1, 1));
    //        }
    //        else if ((5 < dmg) && (dmg <= 20))
    //        {
    //            LoadEffect(hurtEffect2, new Vector3(this.transform.position.x, m_CeilingCheck.position.y - 2, transform.position.z - 2), Quaternion.Euler(-90, 0, 0), 3f, new Vector3(1, 1, 1));
    //        }
    //        else
    //        {
    //            LoadEffect(hurtEffect3, new Vector3(this.transform.position.x, m_CeilingCheck.position.y - 2, transform.position.z - 2), Quaternion.Euler(-90, 0, 0), 3f, new Vector3(1, 1, 1));
    //        }

    //        currentPower += (int)(dmg * 0.15f);
    //        animator.SetTrigger("Hurt");
    //        currentHealth -= dmg;
    //        Show((dmg).ToString(), Color.white);

    //        if (tag == "Player 2")
    //        {
    //            hitTextP1.GetComponent<HitCount>().count++;
    //            hitTextP1.GetComponent<HitCount>().Show();
    //            hitTextP2.GetComponent<HitCount>().count = 0;
    //            hitTextP2.GetComponent<HitCount>().Show();
    //        }
    //        else if (tag == "Player 1")
    //        {
    //            hitTextP2.GetComponent<HitCount>().count++;
    //            hitTextP2.GetComponent<HitCount>().Show();
    //            hitTextP1.GetComponent<HitCount>().count = 0;
    //            hitTextP1.GetComponent<HitCount>().Show();
    //        }
    //    }
    //}

    //public void Show(string text, UnityEngine.Color color)
    //{
    //    if (textPopup)
    //    {
    //        if (txtPopupCheck)
    //        {
    //            txtPopup.transform.localPosition += new Vector3(0, .5f, 0);
    //        }
    //        txtPopup = Instantiate(textPopup, new Vector3(m_CeilingCheck.position.x + Random.Range(-0.2f, 0.2f), m_CeilingCheck.position.y, m_CeilingCheck.position.z), Quaternion.identity);
    //        txtPopup.GetComponentInChildren<TMPro.TextMeshPro>().text = text;
    //        txtPopup.GetComponentInChildren<TMPro.TextMeshPro>().color = color;
    //        txtPopupCheck = true;
    //        timePopup = 1;
    //    }
    //}
}
