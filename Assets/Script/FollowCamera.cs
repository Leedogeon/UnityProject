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


    public float rayLength = 100f;
    public LineRenderer lineRenderer;
    public Color rayColor = Color.blue;
    void Start()
    {
       Cursor.lockState = CursorLockMode.Locked;

        //Forward 확인용
        /*        lineRenderer = GetComponent<LineRenderer>();
                lineRenderer.startWidth = 0.5f;  // 시작 지점의 두께
                lineRenderer.endWidth = 0.5f;  // 끝 지점의 두께
                lineRenderer.startColor = rayColor;
                lineRenderer.endColor = rayColor;*/
    }
    void Update()
    {

        //Forward 확인용
        /*        Vector3 forward = transform.forward;

                // Ray의 시작점과 끝점을 설정하여 LineRenderer로 그리기
                lineRenderer.SetPosition(0, target.transform.position);  // 시작점
                lineRenderer.SetPosition(1, target.transform.position + forward * rayLength);*/

        offset.x = -target.forward.x* offsetRadius;
        offset.z = -target.forward.z* offsetRadius;
        transform.position = Vector3.Lerp(transform.position, target.position, pLerp);
        transform.rotation = Quaternion.Lerp(transform.rotation, target.rotation, rLerp);
        transform.position = target.position + offset;

    }
}
