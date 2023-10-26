using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MapEffect : MonoBehaviour
{
    public List<GameObject> players;

    public GameObject txtReady;
    public GameObject slashEffect;

    private GameObject text;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 1; i <= PlayerPrefs.GetInt("Player Count"); i++)
        {
            players.Add(GameObject.FindGameObjectWithTag("Player " + i.ToString()));
        }

        Effect();
        StartCoroutine(Fight());
    }

    public virtual void Effect()
    {
        
    }

    private IEnumerator Fight()
    {
        foreach (GameObject player in players)
        {
            player.GetComponent<CharacterController>().canNotAttack(3f);
            player.GetComponent<CharacterController>().canNotMove(3f);
            player.GetComponent<CharacterController>().dashTime = 3f;
            player.GetComponent<CharacterController>().canDefen = false;
            player.GetComponent<CharacterController>().canUseSkill = false;
        }

        text = Instantiate(txtReady);
        text.GetComponentInChildren<TMP_Text>().text = "Round " + (PlayerPrefs.GetInt("Score1") + PlayerPrefs.GetInt("Score2") + 1).ToString();

        yield return new WaitForSeconds(1f);

        text = Instantiate(txtReady);
        text.GetComponentInChildren<TMP_Text>().text = "Ready";

        yield return new WaitForSeconds(1f);

        text = Instantiate(txtReady);
        text.GetComponentInChildren<TMP_Text>().text = "Fitght!!!";
        text.GetComponentInChildren<TMP_Text>().fontSize = 60;
        GameObject effect = Instantiate(slashEffect);
        effect.GetComponent<Transform>().localScale = new Vector3(5f, 5f, 5f);
        Destroy(effect, 1f);

        yield return new WaitForSeconds(1f);

        foreach (GameObject player in players)
        {
            player.GetComponent<CharacterController>().canDefen = true;
            player.GetComponent<CharacterController>().canUseSkill = true;
        }
    }
}
