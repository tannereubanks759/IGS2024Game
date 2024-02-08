using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CharacterControllerScript : MonoBehaviour
{
    //public variables
    public float xSens;
    public float ySens;
   private float scopeSenseX;
    private float scopeSenseY;   
    public float moveSpeed;
    public float jumpForce;
    public bool isGrounded;
    public bool isCrouched;

    //private variables
    private float mouseX;
    private float mouseY;
    private Vector3 Velocity;
    private Vector3 moveDirection;
    private GameObject mainCam;
    private CharacterController controller;
    private float horizontal;
    private float vertical;
    private bool isSprinting;
    private float originalSpeed;
    private float sprintSpeed;
    private float airSpeed;
    private bool isScoped;
    // Start is called before the first frame update
    void Start()
    {
        originalSpeed = moveSpeed;
        sprintSpeed = moveSpeed * 2;
        airSpeed = moveSpeed / 4;
        scopeSenseX = xSens / 3;
        scopeSenseY = ySens / 3;
        controller = this.GetComponent<CharacterController>();
        mainCam = Camera.main.gameObject;
        isScoped = false;
        cursorDisable();
    }

    // Update is called once per frame
    void Update()
    {
        //movement
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");
        moveDirection = transform.forward * vertical + transform.right * horizontal;
        //Debug.Log("Move Direction: " + moveDirection);
        if (Input.GetKeyDown(KeyCode.C))
        {
            //float temp = originalSpeed;
            if (isCrouched == false)
            {
               controller.transform.localScale = new Vector3(.5f, .5f, .5f);
                //controller.height = controller.height / 2;
                originalSpeed = originalSpeed / 2;
                isCrouched = true;
            }
            else
            {
                controller.transform.localScale = new Vector3(1f, 1f, 1f);
                //controller.height = controller.height * 2;
                originalSpeed = originalSpeed *2;
                isCrouched = false;
            }
            //Debug.Log("Crouched");
        }
        if (Input.GetMouseButtonDown(1))
        {
            //Debug.Log("Scoped");
            isScoped = !isScoped;
        }
        float currentX = isScoped ? scopeSenseX : xSens;
        float currentY = isScoped ? scopeSenseY : ySens;
        if (controller.isGrounded)
        {   
            if (Velocity.y < 0)
            {
                Velocity.y = -25f;
            }

            if (Input.GetKey(KeyCode.Space))
            {
                Velocity.y = jumpForce;
            }
            
            if (Input.GetKey(KeyCode.LeftShift))
            {
                moveSpeed = sprintSpeed;
            }
            else
            {
                moveSpeed = originalSpeed;
            }
            
        }
        else 
        {
            if (Velocity.y > -9.81f)
            {
                Velocity.y -= 9.81f * Time.deltaTime;
            }
        }
        controller.SimpleMove((moveDirection).normalized * moveSpeed);
        controller.Move(Velocity * Time.deltaTime);
        //mouse Look
        mouseX -= Input.GetAxisRaw("Mouse Y") * currentY;
        mouseY += Input.GetAxisRaw("Mouse X") * currentX;
        mouseX = Mathf.Clamp(mouseX, -90f, 90f);
        this.transform.rotation = Quaternion.Euler(0, mouseY, 0);
        mainCam.transform.rotation = Quaternion.Euler(mouseX, mouseY, 0);

        
    }
    public void cursorDisable()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    public void cursorEnable()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}
