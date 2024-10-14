using Cinemachine.Utility;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
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

    Vector3 newForward;
    public float Length; //Rope����
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
        newForward = Quaternion.LookRotation(Player.transform.forward) * Quaternion.Euler((-ActionScript.turn.y) -30, 0, 0) * Vector3.forward;
        if (ActionScript.IsFall && IsGrappling)
        {
            RopeSwing();
        }
    }
    void RopeShoot()
    {
/*        float dis = Length + 1;
        if (Physics.Raycast(RopeArm.transform.position, newForward, out hit, Length, HitLayer))
        {
            dis = Vector3.Distance(Player.transform.position, hit.point);
        }
        if (dis <= Length / 4)
            newForward = Quaternion.LookRotation(Player.transform.forward) * Quaternion.Euler(-7.5f + (-ActionScript.turn.y), 0, 0) * Vector3.forward;
        else if (dis <= Length / 2)
            newForward = Quaternion.LookRotation(Player.transform.forward) * Quaternion.Euler(-15f + (-ActionScript.turn.y), 0, 0) * Vector3.forward;
        else if (dis <= (Length * 3) / 4)
            newForward = Quaternion.LookRotation(Player.transform.forward) * Quaternion.Euler(-22.5f + (-ActionScript.turn.y), 0, 0) * Vector3.forward;
        else
            newForward = Quaternion.LookRotation(Player.transform.forward) * Quaternion.Euler(-30 + (-ActionScript.turn.y), 0, 0) * Vector3.forward;*/


        if (Physics.Raycast(RopeArm.transform.position, newForward, out hit, Length, HitLayer))
        {
            IsGrappling = true;
            Lr.positionCount = 2;
            Lr.SetPosition(1, hit.point);
            Sj = Player.gameObject.AddComponent<SpringJoint>();
            //��Ŀ�� ��ġ �ڵ����� false
            Sj.autoConfigureConnectedAnchor = false;
            Sj.connectedAnchor = hit.point;

            float distance = Vector3.Distance(transform.position, hit.point);
            Sj.maxDistance = distance;
            Sj.minDistance = distance * .5f;
            Sj.spring = 5f; //����
            Sj.damper = 5f; //�پ��� ��
            Sj.massScale = 5f;

        }

    }
    void RopeSwing()
    {

        Rigidbody PlayerRigid = Player.GetComponent<Rigidbody>();

        Vector3 ToTarget = (hit.point - Player.position).normalized;
        float RopeForce = .05f;
        PlayerRigid.AddForce(ToTarget * RopeForce, ForceMode.Impulse);

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
        Destroy(Sj);
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

    void SetVec()
    { 
    }
}
