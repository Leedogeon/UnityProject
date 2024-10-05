using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class script : MonoBehaviour
{
    private Rigidbody rigid;
    public float speed = 10f;
    public float jump = 5f;
    public float turnSpeed = 3f;
    public Vector3 moveVec;
    private float xAxis;
    private float zAxis;
    void Start()
    {
        rigid = GetComponent<Rigidbody>();
    }

    void Update()
    {
        GetInput();
        Move();
        Turn();
    }
    void GetInput()
    {
        xAxis = Input.GetAxisRaw("Horizontal");
        zAxis = Input.GetAxisRaw("Vertical");
        moveVec = new Vector3(xAxis, 0, zAxis).normalized;
    }
    void Move()
    {
        transform.position += speed * moveVec * Time.deltaTime;
    }
    void Turn()
    {
        transform.LookAt(transform.position + moveVec);
        //transform.forward = Vector3.Lerp(transform.forward, moveVec, turnSpeed * Time.deltaTime);
    }

}
