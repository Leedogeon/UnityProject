using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow : MonoBehaviour
{
    public Transform target;
    public Vector3 offset;
    public Vector2 turn;
    public float maxX = 45;
    public float MaxY = 30;

    void Start()
    {
       Cursor.lockState = CursorLockMode.Locked;   
    }
    void Update()
    {
        transform.position = target.position + offset;
        turn.x += Input.GetAxisRaw("Mouse X");
        turn.x = Mathf.Clamp(turn.x, -maxX, maxX);
        turn.y += Input.GetAxisRaw("Mouse Y");
        turn.y = Mathf.Clamp(turn.y, -60, MaxY);
        transform.localRotation = Quaternion.Euler(-turn.y,turn.x, 0);
    }
}
