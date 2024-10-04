using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class script : MonoBehaviour
{
    private Rigidbody rigidbody;
    public float speed = 10f;
    public float jump = 5f;
    public float TurnSpeed = 3f;

    private Vector3 dir = Vector3.zero;
    void Start()
    {
        rigidbody = this.GetComponent<Rigidbody>();
    }

    void Update()
    {
        GetInput();
        Move();

    }
    void GetInput()
    {
        dir.x = Input.GetAxis("Horizontal");
        dir.z = Input.GetAxis("Vertical");
    }
    void Move()
    {
        if (dir != Vector3.zero)
        {
            transform.forward = dir;
            transform.position += dir * speed * 1f * Time.deltaTime;
        }

    }

}
