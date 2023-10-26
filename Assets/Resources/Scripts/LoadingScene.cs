using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingScene : MonoBehaviour
{
    public GameObject LoadingScreen;
    public GameObject SelectScreen;
    public Image loadingBarFill;
    public GameObject P1;
    public TMP_Text nameP1;
    public GameObject P2;
    public TMP_Text nameP2;
    public CharacterDatabase characterDB;

    public void LoadScene()
    {
        string scenceName = "Desert";
        if (PlayerPrefs.HasKey("sceneName"))
        {
            scenceName = PlayerPrefs.GetString("sceneName");
        }
        
        Character character1 = characterDB.GetCharacter(PlayerPrefs.GetInt("selectedCharacterOption1"));
        GameObject image1 = Instantiate(character1.characterSprite[PlayerPrefs.GetInt("selectedSkinOption1")], P1.transform);
        image1.transform.localScale *= 10f;
        nameP1.text = character1.characterName;

        Character character2 = characterDB.GetCharacter(PlayerPrefs.GetInt("selectedCharacterOption2"));
        GameObject image2 = Instantiate(character2.characterSprite[PlayerPrefs.GetInt("selectedSkinOption2")], P2.transform);
        image2.transform.localScale *= 10f;
        nameP2.text = character2.characterName;

        PlayerPrefs.SetInt("Score1", 0);
        PlayerPrefs.SetInt("Score2", 0);

        StartCoroutine(LoadSceneAsync(scenceName));
    }

    IEnumerator LoadSceneAsync(string scene)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(scene);
        LoadingScreen.SetActive(true);
        SelectScreen.SetActive(false);
        while(!operation.isDone)
        {
            float progressValue = Mathf.Clamp01(operation.progress/0.9f);
            loadingBarFill.fillAmount = progressValue;
            yield return null;
        }
    }
}
