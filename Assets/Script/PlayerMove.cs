using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class script : MonoBehaviour
{
    private Rigidbody rigid;
    public float speed = 10f;
    public float jumpPower = 7f;
    public float turnSpeed = 3f;
    public Vector3 moveVec;
    private float xAxis;
    private float zAxis;
    private bool jDown;
    private bool fDown;
    private bool isJump = false;
    public Camera FollowCamera;
    void Start()
    {
        rigid = GetComponent<Rigidbody>();
    }

    void Update()
    {
        GetInput();
        Move();
        Turn();
        Jump();
        Attack();
    }
    void GetInput()
    {
        xAxis = Input.GetAxisRaw("Horizontal");
        zAxis = Input.GetAxisRaw("Vertical");
        moveVec = new Vector3(xAxis, 0, zAxis).normalized;
        jDown = Input.GetButtonDown("Jump");
        fDown = Input.GetButtonDown("Fire");
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
    void Jump()
    {
        if (jDown && !isJump)
        {
            isJump = true;
            rigid.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
        }
        
    }
    void OnCollisionEnter(Collision collision)
    {
        //바닥에 닿았을때 점프키 재활성화
        if (collision.gameObject.tag == "Floor")
        {
            isJump = false;
        }
    }
    void Attack()
    {
        if(fDown)
        {
            print("test");
        }
    }
}
