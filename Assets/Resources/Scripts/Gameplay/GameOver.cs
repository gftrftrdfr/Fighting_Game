using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{
    GameObject player1;
    GameObject player2;
    public GameObject gameOver;
    public GameObject timeShow;
    public TMPro.TextMeshProUGUI tmp;

    bool timeMode;
    float time;

    public TMP_Text scoreP1;
    public TMP_Text scoreP2;

    int scoreTempP1;
    int scoreTempP2;
    bool check;

    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.GetString("PlayMode") != "practice")
        {
            player1 = GameObject.FindGameObjectWithTag("Player 1");
            player2 = GameObject.FindGameObjectWithTag("Player 2");
            if (PlayerPrefs.GetFloat("time") != 0)
            {
                timeMode = true;
            }
            else
            {
                timeMode = false;
            }
            scoreP1.text = PlayerPrefs.GetInt("Score1").ToString();
            scoreP2.text = PlayerPrefs.GetInt("Score2").ToString();
            scoreTempP1 = PlayerPrefs.GetInt("Score1");
            scoreTempP2 = PlayerPrefs.GetInt("Score2");
            check = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerPrefs.GetString("PlayMode") != "practice")
        {
            if (check)
            {
                if (!timeMode)
                {
                    if (player1.GetComponent<CharacterController>().currentHealth == 0)
                    {
                        player2.GetComponent<CharacterController>().canTakeDame = false;
                        StartCoroutine(ShowOver("Player 2 Win!!!"));
                        PlayerPrefs.SetInt("Score2", scoreTempP2 + 1);
                        check = false;
                    }
                    else if (player2.GetComponent<CharacterController>().currentHealth == 0)
                    {
                        player1.GetComponent<CharacterController>().canTakeDame = false;
                        StartCoroutine(ShowOver("Player 1 Win!!!"));
                        PlayerPrefs.SetInt("Score1", scoreTempP1 + 1);
                        check = false;
                    }
                }
                else if (timeMode)
                {
                    time = timeShow.GetComponent<TimeController>().timeInGame;
                    if (time <= 0)
                    {
                        if (player1.GetComponent<CharacterController>().currentHealth > player2.GetComponent<CharacterController>().currentHealth)
                        {
                            player1.GetComponent<CharacterController>().canTakeDame = false;
                            player2.GetComponent<CharacterController>().canTakeDame = false;
                            StartCoroutine(ShowOver("Player 1 Win!!!"));
                            PlayerPrefs.SetInt("Score1", scoreTempP1 + 1);
                            check = false;
                        }
                        else if (player1.GetComponent<CharacterController>().currentHealth < player2.GetComponent<CharacterController>().currentHealth)
                        {
                            player1.GetComponent<CharacterController>().canTakeDame = false;
                            player2.GetComponent<CharacterController>().canTakeDame = false;
                            StartCoroutine(ShowOver("Player 2 Win!!!"));
                            PlayerPrefs.SetInt("Score2", scoreTempP2 + 1);
                            check = false;
                        }
                        else
                        {
                            player1.GetComponent<CharacterController>().canTakeDame = false;
                            player2.GetComponent<CharacterController>().canTakeDame = false;
                            StartCoroutine(ShowOver("Tieeeee!!!"));
                            check = false;
                        }
                    }
                }
            }
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
