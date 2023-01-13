using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class FASkill : MonoBehaviour
{
    public Transform bulletSpawnPoint;
    public GameObject bulletPf;
    public float speed;

    GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        player = this.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (player.tag == "Player 1")
        {
            if (Input.GetButtonDown("Cast 1") )
            {
                GameObject enemy = GameObject.FindGameObjectWithTag("Player 2");
                Skill(enemy);
            }
        }else if (player.tag == "Player 2")
        {
            if (Input.GetButtonDown("Cast 2"))
            {
                GameObject enemy = GameObject.FindGameObjectWithTag("Player 1");
                Skill(enemy);
            }
        }
    }

    private void Skill(GameObject enemy)
    {
        var bullet = Instantiate(bulletPf, new Vector3(Random.Range(enemy.transform.position.x - 20, enemy.transform.position.x + 20), enemy.transform.position.y + 100), bulletSpawnPoint.rotation);
        bullet.GetComponent<Rigidbody2D>().velocity = bulletSpawnPoint.up * speed;
    }
}
