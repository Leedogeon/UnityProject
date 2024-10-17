using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCam : MonoBehaviour
{
    [Header("References")]
    public Transform orientation;
    public Transform Player;
    public Transform PlayerObj;
    public Rigidbody rigid;
    public float rotationSpeed;


    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    // Update is called once per frame
    void Update()
    {
/*        Vector3 veiwDir = Player.position - new Vector3(transform.position.x, Player.position.y, transform.position.z);
        orientation.forward = veiwDir.normalized;
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");
        Vector3 inputDir = orientation.forward * verticalInput + orientation.right * horizontalInput;*/

        /*if(inputDir != Vector3.zero)
        {
            PlayerObj.forward = Vector3.Slerp(PlayerObj.forward, inputDir.normalized, Time.deltaTime*rotationSpeed);
        }*/
    }
}
