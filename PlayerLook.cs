using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLook : MonoBehaviour
{
    public float walkSpeed = 3f;
    public float sprintSpeed = 5f;
    public float mouseSensitivity = 100f;
    public float crouchDepth = -0.2f;
    public float playerHeight = 1.0f;
    public float jumpHeight = 5.0f;

    private float currentSpeed = 3f;
    private float xRotation = 0f; 
    private Rigidbody rb;
    private Camera playerCamera;


    void Start()
    {
        rb = GetComponent<Rigidbody>();
        playerCamera = Camera.main;  
        Cursor.lockState = CursorLockMode.Locked;  
    }

    void Update()
    {
        
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

       
        transform.Rotate(Vector3.up * mouseX);

       
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f); 
        playerCamera.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        if(Input.GetKey(KeyCode.LeftShift))
            {
                currentSpeed = sprintSpeed;
            }

            else
            {
                currentSpeed = walkSpeed;
            }       
        
        if(Input.GetKeyDown(KeyCode.Space))
            {
               rb.velocity = new Vector3(rb.velocity.x, jumpHeight, rb.velocity.z);
            }
        
        if (Input.GetKeyDown(KeyCode.LeftControl))
            {
               transform.localScale = new Vector3(transform.localScale.x, crouchDepth, transform.localScale.z);
            }

        if (Input.GetKeyUp(KeyCode.LeftControl))
            {
               transform.localScale = new Vector3(transform.localScale.x, playerHeight, transform.localScale.z);
            }

    }

    void FixedUpdate()
    {
      
        float moveX = Input.GetAxis("Horizontal");  
        float moveZ = Input.GetAxis("Vertical");   

        Vector3 move = transform.right * moveX + transform.forward * moveZ;
        rb.MovePosition(rb.position + move * currentSpeed * Time.fixedDeltaTime);
    }
}