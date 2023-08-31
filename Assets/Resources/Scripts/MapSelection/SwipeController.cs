using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class SwipeController : MonoBehaviour, IEndDragHandler
{
    [SerializeField] int max;
    int currentPage;
    Vector3 targetPos;
    [SerializeField] Vector3 pageStep;
    [SerializeField] RectTransform lvPageRect;

    [SerializeField] float tweenTime;
    [SerializeField] LeanTweenType tweenType;

    public List<Button> maps = new List<Button>();

    float dragThreshould;

    float cooldown = 0;
    bool check = true;

    private void Awake()
    {
        currentPage = 1;
        targetPos = lvPageRect.localPosition; 
        foreach (var map in maps)
        {           
            Image[] images = map.GetComponentsInChildren<Image>();
            TMP_Text[] texts = map.GetComponentsInChildren<TMP_Text>();
            if (map == maps[currentPage - 1])
            {
                map.transform.localScale = new Vector3(1.2f, 1.2f, 1.2f);
                foreach (Image image in images)
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
    }

    private void Update()
    {
        if (check)
        {
            if (Input.GetAxisRaw("Horizontal") == 1)
            {
                Next();
                check = false;
            }
            if (Input.GetAxisRaw("Horizontal") == -1)
            {
                Previous();
                check = false;
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
            foreach (var map in maps)
            {
                Image[] images = map.GetComponentsInChildren<Image>();
                TMP_Text[] texts = map.GetComponentsInChildren<TMP_Text>();
                if (map == maps[currentPage - 1])
                {
                    map.transform.localScale = new Vector3(1.2f, 1.2f, 1.2f);
                    foreach (Image image in images)
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
        }
    }  

    public void Previous() 
    {
        if (currentPage > 1)
        {
            currentPage--;
            targetPos -= pageStep;
            MovePage();
            foreach (var map in maps)
            {
                Image[] images = map.GetComponentsInChildren<Image>();
                TMP_Text[] texts = map.GetComponentsInChildren<TMP_Text>();
                if (map == maps[currentPage - 1])
                {
                    map.transform.localScale = new Vector3(1.2f, 1.2f, 1.2f);
                    foreach (Image image in images)
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
        }
    }

    private void MovePage()
    {
        lvPageRect.LeanMoveLocal(targetPos,tweenTime).setEase(tweenType);
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
}
