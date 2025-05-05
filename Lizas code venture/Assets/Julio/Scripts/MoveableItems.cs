using UnityEngine;

public class MoveableItems : MonoBehaviour
{    
    Transform cameraTransform;

    [Header("Movimentação")]
    public float moveSpeed = 5f;

    [Header("Rotação com Mouse")]
    public float mouseSensitivity = 100f;
    private float rotationY = 0f;


    private float pitch = 0f;
    private float yaw = 0f;  

    private void Start()
    {
        cameraTransform = GameManager.Instance.GetItemCamera().transform;
    }

    void Update()
    {
        HandleMouseRotation();
        HandleMovement();
    }

    void HandleMouseRotation()
    {
        if (Input.GetButton("Fire1")) 
        {
            float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
            float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

            yaw += mouseX;
            pitch -= mouseY;
            pitch = Mathf.Clamp(pitch, -89f, 89f);

            transform.rotation = Quaternion.Euler(pitch, yaw, 0f);
        }
    }

    void HandleMovement()
    {
        Vector3 forward = cameraTransform.forward;
        Vector3 right = cameraTransform.right;

        forward.y = 0f;  
        right.y = 0f;

        forward.Normalize();
        right.Normalize();

        float horizontal = Input.GetAxis("Horizontal"); 
        float vertical = Input.GetAxis("Vertical");     

        Vector3 moveDirection = (forward * vertical + right * horizontal).normalized;
        transform.position += moveDirection * moveSpeed * Time.deltaTime;
    }
}
