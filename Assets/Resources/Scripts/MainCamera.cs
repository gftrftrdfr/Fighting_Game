using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]  
public class MainCamera : MonoBehaviour
{
    public List<Transform> players;
    public Vector3 offset;
    public float smoothTime = 0.5f;
    private Vector3 velocity;
    public float minZoom = 18f;
    public float maxZoom = 4f;
    public float zoomLimiter = 60f;

    private void Start()
    {
        players.Add(GameObject.FindGameObjectWithTag("Player 1").transform);
        players.Add(GameObject.FindGameObjectWithTag("Player 2").transform);
    }
    private void LateUpdate()
    {
        if(players.Count == 0)
        {
            return;
        }
        Move();
        Zoom();        
    }

    private void Zoom()
    {
        float newZoom = Mathf.Lerp(maxZoom, minZoom, GetGreatestDistance() / zoomLimiter);
        Camera.main.orthographicSize = Mathf.Lerp(Camera.main.orthographicSize, newZoom, Time.deltaTime);

    }

    float GetGreatestDistance()
    {
        var bounds = new Bounds(players[0].position, Vector3.zero);
        for (int i = 0; i < players.Count; i++)
        {
            bounds.Encapsulate(players[i].position);
        }
        return bounds.size.x;
    }

    private void Move()
    {
        Vector3 center = GetCenterPoint();
        Vector3 newPos = center + offset;
        transform.position = Vector3.SmoothDamp(transform.position, newPos, ref velocity, smoothTime);
    }

    private Vector3 GetCenterPoint()
    {
        if(players.Count == 1)
        {
            return players[0].position;
        }

        var bounds = new Bounds(players[0].position, Vector3.zero);
        for(int i = 0; i < players.Count; i++)
        {
            bounds.Encapsulate(players[i].position);
        }
        return bounds.center;
    }
}
