using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerMove : MonoBehaviour
{
    private Rigidbody rigid;
    public Transform Head;
    public Transform RopeArm;
    public float speed = 10f;
    public float turnSpeed = 3f;
    public Vector3 moveVec;
    private float xAxis;
    private float zAxis;
    private bool jDown;
    private bool fDown;
    //jump
    public float jumpPower = 7f;
    private bool isJump = false;
    public float jumpCount;
    public float jumpCountBase = 1;
    public bool jumpSkill1 = false;

    public Camera FollowCamera;
    public Vector2 turn;
    public float maxX = 45;
    public float MaxY = 30;
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
        jDown = Input.GetButtonDown("Jump");
        fDown = Input.GetButtonDown("Fire");
        
        if (jumpSkill1)
            jumpCountBase = 2;
        else
            jumpCountBase = 1;
    }
    void Move()
    {
        transform.Translate(Vector3.forward * zAxis * Time.deltaTime * speed);
        transform.Translate(Vector3.right * xAxis * Time.deltaTime * speed);
    }
    void Turn()
    {
        turn.x += Input.GetAxisRaw("Mouse X");
        turn.y += Input.GetAxisRaw("Mouse Y");
        //turn.y = Mathf.Clamp(turn.y, -60, MaxY);
        
        transform.localRotation = Quaternion.Euler(0, turn.x, 0);
        FollowCamera.transform.localRotation = Quaternion.Euler(-turn.y, turn.x, 0);

        //Head.transform.localRotation = Quaternion.Euler(-turn.y,turn.x, 0);
        RopeArm.transform.localRotation = Quaternion.Euler(-turn.y - 15, 0, 0);

    }
    void Jump()
    {
        if (jDown && !isJump)
        {
            isJump = true;
            rigid.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
        }
        else if(jDown && jumpCount >=1)
        {
            isJump = true;
            rigid.velocity = Vector3.zero;
            rigid.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
            jumpCount--;
        }
        
    }
    void OnCollisionEnter(Collision collision)
    {
        //바닥에 닿았을때 점프키 재활성화
        if (collision.gameObject.tag == "Floor")
        {
            isJump = false;
            jumpCount = jumpCountBase;
        }
    }
    void Attack()
    {

    }
}