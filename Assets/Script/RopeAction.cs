using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    public Transform Player;
    public Transform Head;
    public Transform RopeArm;
    public RaycastHit hit;
    public LayerMask HitLayer;
    public float Length;
    public LineRenderer Lr;
    private bool IsGrappling = false;
    public Transform RopePoint;
    public SpringJoint Sj;
    void Start()
    {
        Lr = GetComponent<LineRenderer>();
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
    }
    void DrawRope()
    {
        if(IsGrappling)
        {
            Lr.SetPosition(0,RopePoint.position);
        }

    }
}
