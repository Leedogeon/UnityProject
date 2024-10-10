using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Follow : MonoBehaviour
{
    public Transform target;
    public float pLerp = .02f;
    public float rLerp = .01f;
    public Vector3 offset;
    public float offsetRadius;
    void Start()
    {
       Cursor.lockState = CursorLockMode.Locked;   
    }
    void Update()
    {
        offset.x = -target.forward.x* offsetRadius;
        offset.z = -target.forward.z* offsetRadius;
        transform.position = Vector3.Lerp(transform.position, target.position, pLerp);
        transform.rotation = Quaternion.Lerp(transform.rotation, target.rotation, rLerp);
        transform.position = target.position + offset;

    }
}
