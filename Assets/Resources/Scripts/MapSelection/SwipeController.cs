using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class SwipeController : MonoBehaviour, IEndDragHandler
{
    [SerializeField] int max;
    int currentPage;
    Vector3 targetPos;
    [SerializeField] Vector3 pageStep;
    [SerializeField] RectTransform lvPageRect;

    [SerializeField] float tweenTime;
    [SerializeField] LeanTweenType tweenType;

    public List<UnityEngine.UI.Button> maps = new List<UnityEngine.UI.Button>();

    public BackgroundDatabase backgroundDB;
    BackgroundObject bgObject;
    public GameObject bgParent;
    GameObject tmpBG;

    float dragThreshould;

    float cooldown = 0;
    bool check = true;

    public List<string> decriptions = new List<string>();
    public TMP_Text txtDecripton;

    public Button confirmButton;
    public Button backButton;

    private void Awake()
    {
        currentPage = 1;
        targetPos = lvPageRect.localPosition; 
        foreach (var map in maps)
        {
            UnityEngine.UI.Image[] images = map.GetComponentsInChildren<UnityEngine.UI.Image>();
            TMP_Text[] texts = map.GetComponentsInChildren<TMP_Text>();
            if (map == maps[currentPage - 1])
            {
                map.transform.localScale = new Vector3(1.2f, 1.2f, 1.2f);
                foreach (UnityEngine.UI.Image image in images)
                {
                    image.color = new Color32(255, 255, 255, 255);
                }
                foreach (TMP_Text text in texts)
                {
                    text.color = new Color32(255, 255, 255, 255);
                }
            }           
            else
            {
                foreach (var image in images)
                {
                    image.color = new Color32(150, 150, 150, 255);
                    image.transform.localScale = Vector3.one;
                }
                foreach (TMP_Text text in texts)
                {
                    text.color = new Color32(150, 150, 150, 255);
                }
            }
        }
        dragThreshould = Screen.width / 15;

        txtDecripton.SetText(decriptions[0]);

        bgObject = backgroundDB.GetBackgroundByIndex(0);
        PlayerPrefs.SetString("sceneName", bgObject.backgroundName);
        GameObject bg = Instantiate(bgObject.backgroundObject, bgParent.transform);
        tmpBG = bg;
    }

    private void Update()
    {
        if (check)
        {
            if (Input.GetAxisRaw("Horizontal") == 1)
            {
                Next();
                check = false;
                if (AudioManager.Instance)
                    AudioManager.Instance.PlaySFX("Move");
            }
            if (Input.GetAxisRaw("Horizontal") == -1)
            {
                Previous();
                check = false;
                if (AudioManager.Instance)
                    AudioManager.Instance.PlaySFX("Move");
            }
            if(Input.GetButtonDown("Submit"))
            {
                confirmButton.onClick.Invoke();
                if (AudioManager.Instance)
                    AudioManager.Instance.PlaySFX("Confirm");
            }
            if (Input.GetButtonDown("Cancel"))
            {
                backButton.onClick.Invoke();
                if (AudioManager.Instance)
                    AudioManager.Instance.PlaySFX("Back");
            }
        }
    }

    private void FixedUpdate()
    {
        if (!check)
        {
            cooldown += Time.fixedDeltaTime;
            if (cooldown > .2f)
            {
                check = true;
                cooldown = 0;
            }
        }
    }

    public void Next()
    {
        if(currentPage < max)
        {
            currentPage++;
            targetPos += pageStep;
            MovePage();          
        }
    }  

    public void Previous() 
    {
        if (currentPage > 1)
        {
            currentPage--;
            targetPos -= pageStep;
            MovePage();          
        }
    }

    private void MovePage()
    {
        Destroy(tmpBG);
        lvPageRect.LeanMoveLocal(targetPos,tweenTime).setEase(tweenType);
        foreach (var map in maps)
        {
            UnityEngine.UI.Image[] images = map.GetComponentsInChildren<UnityEngine.UI.Image>();
            TMP_Text[] texts = map.GetComponentsInChildren<TMP_Text>();
            if (map == maps[currentPage - 1])
            {
                map.transform.localScale = new Vector3(1.2f, 1.2f, 1.2f);
                foreach (UnityEngine.UI.Image image in images)
                {
                    image.color = new Color32(255, 255, 255, 255);
                }
                foreach (TMP_Text text in texts)
                {
                    text.color = new Color32(255, 255, 255, 255);
                }
            }
            else
            {
                foreach (var image in images)
                {
                    image.color = new Color32(150, 150, 150, 255);
                    image.transform.localScale = Vector3.one;
                }
                foreach (TMP_Text text in texts)
                {
                    text.color = new Color32(150, 150, 150, 255);
                }
            }
        }
        txtDecripton.SetText(decriptions[currentPage - 1]);
        if (AudioManager.Instance)
            AudioManager.Instance.PlaySFX("Message");

        bgObject = backgroundDB.GetBackgroundByIndex(currentPage - 1);
        PlayerPrefs.SetString("sceneName", bgObject.backgroundName);
        GameObject bg = Instantiate(bgObject.backgroundObject, bgParent.transform);       
        tmpBG = bg;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if(Mathf.Abs(eventData.position.x - eventData.pressPosition.x)>dragThreshould)
        {
            if (eventData.position.x > eventData.pressPosition.x) { Previous(); }
            else Next();
        }
        else
        {
            MovePage();
        }
    }

    public void ChangeScene()
    {
        SceneManager.LoadScene("CharacterSelection");
    }
    public void BackScene()
    {
        SceneManager.LoadScene("Menu");
    }
}
