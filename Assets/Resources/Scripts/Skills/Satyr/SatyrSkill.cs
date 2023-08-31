using System;
using System.Collections;
using System.Collections.Generic;
using System.Resources;
using Unity.Burst.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;
using Random = UnityEngine.Random;

public class SatyrSkill : MonoBehaviour
{
    private bool canUseSkill1;
    private bool canUseSkill2;
    private bool canUseUlti;

    private Animator animator;
    private Rigidbody2D rb;
    public GameObject treePrefab;

    public Transform leftHand;
    public Transform rightHand;

    [SerializeField] private GameObject musicEffect1;
    [SerializeField] private GameObject musicEffect2;
    [SerializeField] private GameObject handEffect;
    [SerializeField] private GameObject powEffect;
    [SerializeField] private GameObject powTauntEffect;

    Collider2D[] temps;

    public GameObject skill1;
    public GameObject skill2;

    int[] temp1 = new int [5];
    int[] temp2 = new int[5];

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
        GetComponent<CharacterController>().canMove = false;
        GetComponent<CharacterController>().LoadEffect(musicEffect1, new Vector3(GetComponent<CharacterController>().m_CeilingCheck.position.x, GetComponent<CharacterController>().m_CeilingCheck.position.y, -3), Quaternion.Euler(-90, 0, 0), 0.5f, new Vector3(4f, 4f, 4f));

        yield return new WaitForSeconds(1f);

        int atkTemp = 0;
        int armorTemp = 0;       
        int count = 0;

        GetComponent<CharacterController>().canMove = true;
        Collider2D[] hits = Physics2D.OverlapCircleAll(new Vector3(GetComponent<CharacterController>().m_GroundCheck.position.x + 2, GetComponent<CharacterController>().m_GroundCheck.position.y, GetComponent<CharacterController>().m_GroundCheck.position.z), 6f, GetComponent<CharacterController>().enemyLayers);
        foreach (Collider2D hit in hits)
        {
            temp1[count] = (int)(hit.GetComponent<CharacterController>().attackDmg * 0.2);
            temp2[count] = (int)(hit.GetComponent<CharacterController>().armor * 0.2);
            hit.GetComponent<CharacterController>().DecreaseATK(temp1[count]);
            hit.GetComponent<CharacterController>().DecreaseArmor(temp2[count]);
            atkTemp += temp1[count];
            armorTemp += temp2[count];
            count++;
        }

        if (atkTemp != 0 || armorTemp !=0)
        {
            GameObject leftEffect = Instantiate(handEffect, new Vector3(leftHand.position.x, leftHand.position.y, 2), Quaternion.Euler(0, 0, 0));
            leftEffect.transform.localScale = new Vector3(0.7f, 0.7f, 0.7f);
            leftEffect.transform.parent = leftHand;
            Destroy(leftEffect, 5f);
            GameObject rightEffect = Instantiate(handEffect, new Vector3(rightHand.position.x, rightHand.position.y, -2), Quaternion.Euler(0, 0, 0));
            rightEffect.transform.localScale = new Vector3(0.7f, 0.7f, 0.7f);
            rightEffect.transform.parent = rightHand;
            Destroy(rightEffect, 5f);
        }
        GetComponent<CharacterController>().IncreaseATK(atkTemp);       
        yield return new WaitForSeconds(.5f);
        GetComponent<CharacterController>().IncreaseArmor(armorTemp);

        yield return new WaitForSeconds(5f);
        count = 0;
        foreach (Collider2D hit in hits)
        {
            hit.GetComponent<CharacterController>().IncreaseATK(temp1[count]);
            hit.GetComponent<CharacterController>().IncreaseArmor(temp2[count]);
            count++;
        }

        GetComponent<CharacterController>().DecreaseATK(atkTemp);
        GetComponent<CharacterController>().DecreaseArmor(armorTemp);

        skill1.GetComponent<SkillCooldown>().UseSkill(cooldown);
        yield return new WaitForSeconds(cooldown);
        canUseSkill1 = true;
    }


    private IEnumerator Skill2(float cooldown)
    {
        canUseSkill2 = false;
        GetComponent<CharacterController>().canMove = false;
        GetComponent<CharacterController>().LoadEffect(musicEffect2, new Vector3(GetComponent<CharacterController>().m_CeilingCheck.position.x, GetComponent<CharacterController>().m_CeilingCheck.position.y, -3), Quaternion.Euler(-90, 0, 0), 0.5f, new Vector3(2f, 2f, 2f));

        yield return new WaitForSeconds(1f);
        GetComponent<CharacterController>().canMove = true;
        
        temps = Physics2D.OverlapCircleAll(new Vector3(GetComponent<CharacterController>().m_GroundCheck.position.x + 2, GetComponent<CharacterController>().m_GroundCheck.position.y, GetComponent<CharacterController>().m_GroundCheck.position.z), 6f, GetComponent<CharacterController>().enemyLayers);
        foreach (Collider2D temp in temps)
        {
            temp.GetComponent<CharacterController>().Intoxicated(1.5f, transform);
        }

        skill2.GetComponent<SkillCooldown>().UseSkill(cooldown);
        yield return new WaitForSeconds(cooldown);
        canUseSkill2 = true;
    }

    private IEnumerator Ultimate()
    {
        canUseUlti = false;
        GetComponent<CharacterController>().canNotMove(.7f);
        GetComponent<CharacterController>().LoadEffect(powEffect, new Vector3(transform.position.x, GetComponent<CharacterController>().m_CeilingCheck.position.y, -2), Quaternion.Euler(-90, 0, 0), 1f, new Vector3(4, 4, 4));
        GetComponent<CharacterController>().LoadEffect(powTauntEffect, new Vector3(GetComponent<CharacterController>().m_GroundCheck.position.x, GetComponent<CharacterController>().m_GroundCheck.position.y, -2), Quaternion.Euler(0, 0, 0), 1f, new Vector3(2, 2, 2));

        yield return new WaitForSeconds(0.7f);

        treePrefab.tag = tag + " Skill";
        GameObject gObject = Instantiate(treePrefab, new Vector3(GetComponent<CharacterController>().m_GroundCheck.position.x, GetComponent<CharacterController>().m_GroundCheck.position.y, 0), transform.rotation);
        Destroy(gObject, 5f);

        yield return new WaitForSeconds(5f);
        GetComponent<CharacterController>().currentPower = 0;
        canUseUlti = true;
    }

}
