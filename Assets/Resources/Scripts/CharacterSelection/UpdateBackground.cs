using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UpdateBackground : MonoBehaviour
{
    private string bgName = "Desert";
    public  BackgroundDatabase backgroundDB;
    public CharacterDatabase characterDB;
    BackgroundObject bgObject;
    int tmp = 0;
    float cooldown = 10f;
    bool check = false;
    void Start()
    {
        if (SceneManager.GetActiveScene().name == "Menu")
        {
            int tmp = Random.Range(0, backgroundDB.CharacterCount);
            bgObject = backgroundDB.GetBackgroundByIndex(tmp);
            PlayerPrefs.SetString("sceneName", bgObject.backgroundName);
            GameObject bg = Instantiate(bgObject.backgroundObject, transform);
        }
        else if (SceneManager.GetActiveScene().name != "SelectMap")
        {
            if (PlayerPrefs.HasKey("sceneName"))
            {
                bgName = PlayerPrefs.GetString("sceneName");
                bgObject = backgroundDB.GetBackgroundByName(bgName);
            }
            GameObject bg = Instantiate(bgObject.backgroundObject, transform);
        }               
    }

    private void Update()
    {
        if (SceneManager.GetActiveScene().name == "Menu")
        {
            while (check)
            {
                check = false;
                tmp = (int)Random.Range(0, characterDB.CharacterCount - 1);
                Character character = characterDB.GetCharacter(tmp);
                GameObject artwork = Instantiate(character.characterSprite[(int)Random.Range(0, 2)], transform);
                artwork.GetComponent<Animator>().SetBool("run", true);
                artwork.transform.localPosition = new Vector3(-1320, -460, 0);
                artwork.GetComponent<Rigidbody2D>().velocity = transform.right * Random.Range(2, 4);
                Destroy(artwork, 15f);
                switch (tmp)
                {
                    case 1:
                    case 2:
                    case 3:
                    case 4:
                        artwork.transform.localScale = Vector3.one * 100;
                        break;
                    default:
                        artwork.transform.localScale = new Vector3(50f, 50f, 50f);
                        break;
                }              
            }
        }
    }
    private void FixedUpdate()
    {
        if (!check)
        {
            cooldown -= Time.fixedDeltaTime;
            if (cooldown < 0)
            {
                check = true;
                cooldown = Random.Range(40,60);
            }
        }
    }
}
