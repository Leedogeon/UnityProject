using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamRotate : MonoBehaviour
{
    public Vector3 offset;
    public Vector2 turn;
    public float maxX = 45;
    public float MaxY = 30;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        turn.x += Input.GetAxisRaw("Mouse X");
        //turn.x = Mathf.Clamp(turn.x, -maxX, maxX);
        turn.y += Input.GetAxisRaw("Mouse Y");
        turn.y = Mathf.Clamp(turn.y, -60, MaxY);
        transform.localRotation = Quaternion.Euler(-turn.y, turn.x, 0);
    }
}
