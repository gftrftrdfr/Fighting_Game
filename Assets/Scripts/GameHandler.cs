
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameHandler : MonoBehaviour
{

    [SerializeField] private Transform dmgPopup;
    // Start is called before the first frame update
    void Start()
    {
        Transform transform = Instantiate(dmgPopup, Vector3.zero, Quaternion.identity);
        Popup popup = transform.GetComponent<Popup>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
