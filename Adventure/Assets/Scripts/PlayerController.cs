using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed;
    public CharacterController charCon;

    private CameraController cam;
    private Vector3 moveAmount;
    
    void Start()
    {
        cam = FindObjectOfType<CameraController>();
    }
    
    void Update()
    {
        float yStore = moveAmount.y;
        
        //Input
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveZ = Input.GetAxisRaw("Vertical");

        //Camera moving vector length changes to 1.
        moveAmount = cam.transform.forward * Input.GetAxisRaw("Vertical") +
                     cam.transform.right * Input.GetAxisRaw("Horizontal");
        moveAmount.y = 0;
        moveAmount = moveAmount.normalized;
        moveAmount.y = yStore;
        
        charCon.Move(new Vector3(moveAmount.x * moveSpeed, moveAmount.y, moveAmount.z * moveSpeed) * Time.deltaTime);
    }

    private void FixedUpdate()
    {
        //Gravity
        if (!charCon.isGrounded)
        {
            moveAmount.y += Physics.gravity.y * Time.fixedDeltaTime;
        }
        else
        {
            moveAmount.y = Physics.gravity.y * Time.fixedDeltaTime;
        }
    }
}
