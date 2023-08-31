using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{
    GameObject player1;
    GameObject player2;
    public GameObject gameOver;
    public TMPro.TextMeshProUGUI tmp;

    // Start is called before the first frame update
    void Start()
    {
        player1 = GameObject.FindGameObjectWithTag("Player 1");
        player2 = GameObject.FindGameObjectWithTag("Player 2");
    }

    // Update is called once per frame
    void Update()
    {
        if (player1.GetComponent<CharacterController>().currentHealth == 0)
        {
            StartCoroutine(ShowOver("Player 1 Win!!!"));
        }
        if (player2.GetComponent<CharacterController>().currentHealth == 0)
        {
            StartCoroutine(ShowOver("Player 2 Win!!!"));
        }
    }

    public IEnumerator ShowOver(string txt)
    {
        Time.timeScale = 0.5f;
        yield return new WaitForSeconds(1f);
        Time.timeScale = 1f;
        yield return new WaitForSeconds(2f);
        gameOver.SetActive(true);
        tmp.text = txt;
    }

}
