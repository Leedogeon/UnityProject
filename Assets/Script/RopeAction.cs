using System.Collections;
using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class RopeAction: MonoBehaviour
{
    public Transform Player;
    public Transform RopeArm;
    public RaycastHit hit;
    public LayerMask HitLayer;

    public LineRenderer Lr; 
    public Transform RopePoint;
    public SpringJoint Sj;

    public float Length; //Rope길이
    private bool IsGrappling = false;
    private bool IsAttach = false;

    [SerializeField] private PlayerMove moveScript;
    void Start()
    {
        Lr = GetComponent<LineRenderer>();

        if (Player != null)
        {
            moveScript = Player.GetComponent<PlayerMove>();
        }

    }

    void Update()
    {
        if(Input.GetButtonDown("Fire"))
        {
            RopeShoot();
        }
        else if(Input.GetButtonUp("Fire"))
        {
            EndShoot();
        }

        if(IsGrappling)
        {
            DrawRope();
            if(Input.GetButton("Fire2") && !IsAttach)
            {
                Attach();
            }
        }
        
    }
    void RopeShoot()
    {
        if(Physics.Raycast(RopeArm.transform.position,RopeArm.transform.forward,out hit, Length, HitLayer))
        {
            IsGrappling = true;

            Lr.positionCount = 2;
            Lr.SetPosition(1, hit.point);
            Sj = Player.gameObject.AddComponent<SpringJoint>();
            //앵커의 위치 자동설정 false
            Sj.autoConfigureConnectedAnchor = false;
            Sj.connectedAnchor = hit.point;

            float distance = Vector3.Distance(transform.position,hit.point);
            Sj.maxDistance = distance;
            Sj.minDistance = distance * .5f;
            Sj.spring = 5f; //강도
            Sj.damper = 5f; //줄어드는 힘
            Sj.massScale = 5f;
        }
    }
    void EndShoot()
    {
        IsGrappling = false;
        Lr.positionCount = 0;
        Destroy(Sj);
        IsAttach = false;
    }
    void DrawRope()
    {
        if(IsGrappling)
        {
            Lr.SetPosition(0,RopePoint.position);
        }

    }
    void Attach()
    {
        IsAttach = true;
        Rigidbody PlayerRigid = Player.GetComponent<Rigidbody>();
        Vector3 ToTarget = (hit.point - Player.position).normalized;
        float RopeForce = 20f;
        PlayerRigid.AddForce(ToTarget*RopeForce, ForceMode.Impulse);
        if (moveScript.jumpCount == 0)
            moveScript.jumpCount++;

        EndShoot();
    }
}
