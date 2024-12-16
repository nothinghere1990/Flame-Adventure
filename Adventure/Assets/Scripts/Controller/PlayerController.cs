using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed;
    public CharacterController charCon;
    private CameraController cam;
    private Vector3 moveAmount;
    public float sprintScale;

    public float jumpForce, gravityScale;
    private float yStore;

    public float rotateSpeed = 10;

    public Animator anim;

    public GameObject jumpParticle, landingParticle;
    private bool lastGrounded;
    
    void Start()
    {
        cam = FindObjectOfType<CameraController>();
        
        jumpParticle.SetActive(false);
        lastGrounded = true;
        charCon.Move(new Vector3(0, Physics.gravity.y * gravityScale * Time.deltaTime, 0));
    }
    
    void Update()
    {
        yStore = moveAmount.y;
        
        //Input
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveZ = Input.GetAxisRaw("Vertical");

        //Camera moving vector length changes to 1.
        moveAmount = cam.transform.forward * Input.GetAxisRaw("Vertical") +
                     cam.transform.right * Input.GetAxisRaw("Horizontal");
        moveAmount.y = 0;
        moveAmount = moveAmount.normalized;

        //Sprint
        if (Input.GetKey(KeyCode.LeftShift)) moveAmount *= sprintScale;
            
        //Rotate
        if (moveAmount.magnitude > .1f)
        {
            if (moveAmount != Vector3.zero)
            {
                Quaternion newRot = Quaternion.LookRotation(moveAmount);
                transform.rotation = Quaternion.Slerp(transform.rotation, newRot, rotateSpeed * Time.deltaTime);
            }
        }
        
        //Get yStore after normalized.
        moveAmount.y = yStore;
        
        if (charCon.isGrounded)
        {
            jumpParticle.SetActive(false);
            
            if (!lastGrounded)
                landingParticle.SetActive(true);
            
            //Jump
            if (Input.GetButtonDown("Jump"))
            {
                moveAmount.y = jumpForce;
                jumpParticle.SetActive(true);
            }
        }

        lastGrounded = charCon.isGrounded;
        
        charCon.Move(new Vector3(moveAmount.x * moveSpeed, moveAmount.y, moveAmount.z * moveSpeed) * Time.deltaTime);
        
        float moveVel = new Vector3(moveAmount.x, 0, moveAmount.z).magnitude * moveSpeed;
        
        //Animations
        anim.SetFloat("speed", moveVel);
        anim.SetBool("isGrounded", charCon.isGrounded);
        anim.SetFloat("yVel", moveAmount.y);
    }

    private void FixedUpdate()
    {
        //Gravity
        if (!charCon.isGrounded)
            moveAmount.y += Physics.gravity.y * gravityScale * Time.fixedDeltaTime;
        else
            moveAmount.y = Physics.gravity.y * gravityScale * Time.fixedDeltaTime;
    }
}
