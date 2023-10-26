using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private GameObject textPlayer;

    private GameObject p1;
    private GameObject p2;
    GameObject pbObject;
    GameObject pbObject2;
    // Start is called before the first frame update
    void Start()
    {
        if(PlayerPrefs.GetString("PlayMode") == "practice")
        {
            p1 = GameObject.FindGameObjectWithTag("Player 1");
            p2 = GameObject.FindGameObjectWithTag("Dummy");
            pbObject = Instantiate(textPlayer, new Vector3(p1.GetComponent<CharacterController>().m_CeilingCheck.position.x, p1.GetComponent<CharacterController>().m_CeilingCheck.position.y + 1.5f, p1.GetComponent<CharacterController>().m_CeilingCheck.position.z), Quaternion.identity);
            pbObject.GetComponentInChildren<TMPro.TextMeshPro>().text = "P1";
            pbObject.GetComponentInChildren<TMPro.TextMeshPro>().color = UnityEngine.Color.red;
            pbObject.GetComponentInChildren<SpriteRenderer>().color = Color.red;

            pbObject2 = Instantiate(textPlayer, new Vector3(p2.GetComponent<CharacterController>().m_CeilingCheck.position.x, p2.GetComponent<CharacterController>().m_CeilingCheck.position.y + 1.5f, p2.GetComponent<CharacterController>().m_CeilingCheck.position.z), Quaternion.identity);
            pbObject2.GetComponentInChildren<TMPro.TextMeshPro>().text = "Dummy";
            pbObject2.GetComponentInChildren<TMPro.TextMeshPro>().color = UnityEngine.Color.white;
            pbObject2.GetComponentInChildren<SpriteRenderer>().color = Color.white;
        }
        else
        {
            p1 = GameObject.FindGameObjectWithTag("Player 1");
            p2 = GameObject.FindGameObjectWithTag("Player 2");
            pbObject = Instantiate(textPlayer, new Vector3(p1.GetComponent<CharacterController>().m_CeilingCheck.position.x, p1.GetComponent<CharacterController>().m_CeilingCheck.position.y + 1.5f, p1.GetComponent<CharacterController>().m_CeilingCheck.position.z), Quaternion.identity);
            pbObject.GetComponentInChildren<TMPro.TextMeshPro>().text = "P1";
            pbObject.GetComponentInChildren<TMPro.TextMeshPro>().color = UnityEngine.Color.red;
            pbObject.GetComponentInChildren<SpriteRenderer>().color = Color.red;

            pbObject2 = Instantiate(textPlayer, new Vector3(p2.GetComponent<CharacterController>().m_CeilingCheck.position.x, p2.GetComponent<CharacterController>().m_CeilingCheck.position.y + 1.5f, p2.GetComponent<CharacterController>().m_CeilingCheck.position.z), Quaternion.identity);
            pbObject2.GetComponentInChildren<TMPro.TextMeshPro>().text = "P2";
            pbObject2.GetComponentInChildren<TMPro.TextMeshPro>().color = UnityEngine.Color.cyan;
            pbObject2.GetComponentInChildren<SpriteRenderer>().color = Color.cyan;
        }
    }

    // Update is called once per frame
    void Update()
    {
        pbObject.transform.position = new Vector3(p1.GetComponent<CharacterController>().m_CeilingCheck.position.x, p1.GetComponent<CharacterController>().m_CeilingCheck.position.y + 1.5f, p1.GetComponent<CharacterController>().m_CeilingCheck.position.z);
        pbObject2.transform.position = new Vector3(p2.GetComponent<CharacterController>().m_CeilingCheck.position.x, p2.GetComponent<CharacterController>().m_CeilingCheck.position.y + 1.5f, p2.GetComponent<CharacterController>().m_CeilingCheck.position.z);
    }
}
