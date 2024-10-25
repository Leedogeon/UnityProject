using Cinemachine.Utility;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
using Unity.VisualScripting;
using UnityEngine;

public class RopeAction : MonoBehaviour
{
    public Transform Player;
    public Transform RopeArm;
    public Camera FollowCamera;
    public RaycastHit hit;
    public LayerMask HitLayer;

    public LineRenderer Lr;
    public Transform RopePoint;
    public SpringJoint Sj;

    public float Length; //Rope길이
    public bool IsGrappling = false;
    private bool IsAttach = false;


    [SerializeField] private PlayerAction ActionScript;
    void Start()
    {
        Lr = GetComponent<LineRenderer>();

        if (Player != null)
        {
            ActionScript = Player.GetComponent<PlayerAction>();

        }

    }

    void Update()
    {
        /*Lr.SetPosition(0, Player.transform.position);  // 시작점
        Lr.SetPosition(1, Player.transform.position + newForward * 50);*/
        if (Input.GetButtonDown("Fire"))
        {
            RopeShoot();
        }
        else if (Input.GetButtonUp("Fire"))
        {
            EndShoot();
        }

        if (IsGrappling)
        {
            DrawRope();
            if (Input.GetButton("Fire2") && !IsAttach)
            {
                Attach();
            }
        }

        if (ActionScript.IsFall && IsGrappling)
        {
            RopeSwing();
        }


        
    }
    void RopeShoot()
    {
        if(Physics.Raycast(FollowCamera.transform.position, FollowCamera.transform.forward, out hit, Length, HitLayer))
        {
            float distance = Vector3.Distance(transform.position, hit.point);
            IsGrappling = true;
            Lr.positionCount = 2;
            Lr.SetPosition(1, hit.point);
            Sj = Player.gameObject.AddComponent<SpringJoint>();
            //앵커의 위치 자동설정 false
            Sj.autoConfigureConnectedAnchor = false;
            Sj.connectedAnchor = hit.point;

            Sj.maxDistance = distance;
            Sj.minDistance = distance * .5f;
            Sj.spring = 2f; //강도
            Sj.damper = 3f; //줄어드는 힘
            Sj.massScale = 1f;
        }

    }

    void RopeSwing()
    {

        Rigidbody PlayerRigid = Player.GetComponent<Rigidbody>();

        Vector3 ToTarget = (hit.point - Player.position).normalized;
        float RopeForce = .05f;
        PlayerRigid.AddForce(ToTarget * RopeForce, ForceMode.Force);
        PlayerRigid.AddForce(Vector3.down * 2.5f, ForceMode.Force);
        if (ActionScript.xAxis != 0 || ActionScript.zAxis != 0)
        {
            PlayerRigid.AddForce(Player.forward * ActionScript.zAxis * .1f);
            PlayerRigid.AddForce(Player.right * ActionScript.xAxis * .1f);
        }

    }
    void EndShoot()
    {
        IsGrappling = false;
        Lr.positionCount = 0;
        if(Sj != null)
        {
            Destroy(Sj);
            Sj = null;
        }
        IsAttach = false;
    }
    void DrawRope()
    {
        if (IsGrappling)
        {
            Lr.SetPosition(0, RopePoint.position);
        }

    }
    void Attach()
    {
        IsAttach = true;
        Rigidbody PlayerRigid = Player.GetComponent<Rigidbody>();
        Vector3 ToTarget = (hit.point - Player.position).normalized;
        float RopeForce = 20f;
        PlayerRigid.AddForce(ToTarget * RopeForce, ForceMode.Impulse);
        if (ActionScript.jumpCount == 0)
            ActionScript.jumpCount++;

        EndShoot();
    }

}
