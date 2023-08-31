using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMotion : MonoBehaviour
{
    public float speed = 100f;
    Vector3 startPosition;

    // Start is called before the first frame update
    void Start()
    {
       startPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.left*speed*Time.deltaTime/100);
        if(transform.localPosition.x < - 1920)
        {
            transform.position = startPosition;
        }
    }
}
