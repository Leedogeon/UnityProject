using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using static Cinemachine.DocumentationSortingAttribute;

public class PlayerAction : MonoBehaviour
{
    private Rigidbody rigid;
    private Animator anim;
    public Transform RopeArm;

    //Move
    public Vector3 CurPos;
    public float speed = 10f;
    public float turnSpeed = 3f;
    public float xAxis;
    public float zAxis;
    private bool jDown;
    private bool fDown;
    //jump
    public float jumpPower = 7f;
    private bool isJump = false;
    public float jumpCount = 1;
    public float jumpCountBase = 1;
    public bool jumpSkill1 = false;
    public bool IsFall;

    [SerializeField] private RopeAction Rope;
    //Dash
    private bool bDash;

    //Camera
    public Camera FollowCamera;
    public Vector2 turn;
    public float maxX = 45;
    public float MaxY = 30;

    public PlayerInfo Info = new PlayerInfo();
    public void SavePlayer()
    {
        Save.SavePlayer(this, Info);
        print("Save Game");
    }

    public void LoadPlayer()
    {
        PlayerDataSave data = Save.LoadPlayer(this, Info);

        Info.Level = data.Level;
        Info.HP = data.HP;

        CurPos.x = data.position[0];
        CurPos.y = data.position[1];
        CurPos.z = data.position[2];
        transform.position = CurPos;
        print("Load Game");
    }
    void Start()
    {
        rigid = GetComponent<Rigidbody>();
        anim = GetComponentInChildren<Animator>();
        Rope = gameObject.GetComponentInChildren<RopeAction>();
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
        bDash = Input.GetButton("Dash");
        if (rigid.velocity.y > .05f || rigid.velocity.y < -.05f ) IsFall = true;
        else IsFall = false;

        if (jumpSkill1)
            jumpCountBase = 2;
        else
            jumpCountBase = 1;

        if (Input.GetButtonDown("Save"))
            SavePlayer();

        if (Input.GetButtonDown("Load"))
            LoadPlayer();
    }
    void Move()
    {
        if (!Rope.IsGrappling || !IsFall)
        {
            anim.SetBool("IsWalk", xAxis != 0 || zAxis != 0);
            anim.SetBool("IsRun", bDash);
            transform.Translate(Vector3.forward * zAxis * Time.deltaTime * speed * (bDash ? 2f : 1f));
            transform.Translate(Vector3.right * xAxis * Time.deltaTime * speed * (bDash ? 2f : 1f));
            CurPos = transform.position;
        }
    }
    void Turn()
    {
        turn.x += Input.GetAxisRaw("Mouse X");
        turn.y += Input.GetAxisRaw("Mouse Y");
        //turn.y = Mathf.Clamp(turn.y, -60, MaxY);
        
        transform.localRotation = Quaternion.Euler(0, turn.x, 0);
        FollowCamera.transform.localRotation = Quaternion.Euler(-turn.y, turn.x, 0);

        //RopeArm.transform.localRotation = Quaternion.Euler(-turn.y - 15, 0, 0);

    }
    void Jump()
    {
        if (jDown && !isJump && !IsFall)
        {
            isJump = true;
            rigid.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
        }
        else if(jDown && jumpCount >=1 && IsFall)
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
            IsFall = false;
            jumpCount = jumpCountBase;
        }
    }
    void Attack()
    {

    }
}