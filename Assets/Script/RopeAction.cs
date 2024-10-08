using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    // Start is called before the first frame update

    public Transform Player;
    Camera cam;
    RaycastHit hit;
    public LayerMask HitLayer;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            RopeShoot();
        }
    }
    void RopeShoot()
    {
        if(Physics.Raycast(cam.transform.position,cam.transform.forward,out hit, 100f, HitLayer))
        {
            print("RopeAction");
        }
    }
}
