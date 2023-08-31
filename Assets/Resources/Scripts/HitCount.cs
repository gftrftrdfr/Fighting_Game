using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class HitCount : MonoBehaviour
{
    public TMP_Text hitPopUp;
    public int count;

    Animator animator;
    float time = 0;
    // Start is called before the first frame update
    private void Start()
    {
        hitPopUp.gameObject.SetActive(false);
        animator = GetComponentInChildren<Animator>();
    }

    private void FixedUpdate()
    {
        Debug.Log(time);
        time -= Time.fixedDeltaTime;
        if(time < 0) 
        {
            time = 0;
            count = 0;
            hitPopUp.gameObject.SetActive(false);
        }
    }

    public void Show()
    {
        time = 2f;
        if (count <= 3)
        {
            hitPopUp.gameObject.SetActive(false);
        }
        else
        {
            hitPopUp.gameObject.SetActive(true);
            hitPopUp.text = count.ToString();
            animator.SetTrigger("Pop");           
        }
    }
}
