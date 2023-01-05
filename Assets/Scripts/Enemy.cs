using System.Collections;
using System.Collections.Generic;
using UnityEditor.AssetImporters;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Animator animator;
    public int maxHealth = 100;
    int currentHealth;
    private Component[] cpn;
    [SerializeField] private Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        animator.SetBool("isDead", false);
    }

    public void TakeDamage (int dmg)
    {
        animator.SetTrigger("Hurt");
        currentHealth -= dmg;

        if(currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        animator.SetBool("isDead", true);
        
        this.GetComponent<Collider2D>().enabled = false;

        cpn = this.GetComponentsInChildren<PolygonCollider2D>();
        foreach(PolygonCollider2D c in cpn)
        {
            c.enabled = false;
        }
        rb.gravityScale = 0.005f;
        this.enabled = false;
    }
}
