using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Popup : MonoBehaviour
{
    private TextMeshPro textMesh;
    // Start is called before the first frame update
    private void Awake()
    {
        textMesh = transform.GetComponent<TextMeshPro>();
    }

    public void Setup(string text)
    {
        textMesh.text = text;
    }
}
