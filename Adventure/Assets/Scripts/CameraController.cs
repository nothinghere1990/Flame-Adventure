using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Transform target;

    private Vector3 offset;

    public float moveSpeed = 15f;

    [HideInInspector]
    public Transform endCamPos;
    
    void Start()
    {
        AssignTarget();
        
        Cursor.lockState = CursorLockMode.Locked;

        LevelManager.instance.camSnap += SnapToTarget;
    }
    
    void Update()
    {
        // Alt and Cursor
        if (Input.GetKey(KeyCode.LeftAlt)) Cursor.lockState = CursorLockMode.None;
        else Cursor.lockState = CursorLockMode.Locked;
        
        //Camera Lerp Move
        if (endCamPos != null)
        {
            transform.position = Vector3.Lerp(transform.position, endCamPos.position, moveSpeed * Time.deltaTime);
            transform.rotation = Quaternion.Slerp(transform.rotation, endCamPos.rotation, moveSpeed * Time.deltaTime);
        }
        else
        {
            transform.position = Vector3.Lerp(transform.position, target.position + offset, moveSpeed * Time.deltaTime);

            //Camera won't fall when player is below the ground.
            if (transform.position.y < offset.y)
            {
                transform.position = new Vector3(transform.position.x, offset.y, transform.position.z);
            }
        }
    }

    private void AssignTarget()
    {
        if (target == null)
        {
            target = FindObjectOfType<PlayerController>().transform;

            offset = transform.position - target.position;
        }
    }

    public void SnapToTarget()
    {
        AssignTarget();

        transform.position = target.position + offset;
    }
}