using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
using static UnityEditor.Experimental.GraphView.GraphView;


public class BGMotion : MonoBehaviour
{
    public float speed = 100f;
    public float positon;
    Vector3 startPosition;

    public List<Transform> players;

    // Start is called before the first frame update
    void Start()
    {
        if (SceneManager.GetActiveScene().name == "Menu" || SceneManager.GetActiveScene().name == "SelectMap" || SceneManager.GetActiveScene().name == "CharacterSelection")
        {
        }
        else
        {
            startPosition = transform.position;
            players.Add(GameObject.FindGameObjectWithTag("Player 1").transform);
            players.Add(GameObject.FindGameObjectWithTag("Player 2").transform);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        if(SceneManager.GetActiveScene().name == "Menu" || SceneManager.GetActiveScene().name == "SelectMap" || SceneManager.GetActiveScene().name == "CharacterSelection")
        {
            transform.Translate(Vector3.left * speed * Time.deltaTime / 100);
            if (transform.localPosition.x < -1920)
            {
                transform.position = startPosition;
            }
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(GetCenterPoint().x, 0, 0)/positon, speed * Time.deltaTime / 100);
        }
    }

    private Vector3 GetCenterPoint()
    {
        if (players.Count == 1)
        {
            return players[0].position;
        }
        else
        {
            Vector3 sum = players[0].position;
            for (int i = 1; i < players.Count; i++)
            {
                sum += players[i].position;
            }
            sum = sum / players.Count;
            return sum;
        }
    }
}
