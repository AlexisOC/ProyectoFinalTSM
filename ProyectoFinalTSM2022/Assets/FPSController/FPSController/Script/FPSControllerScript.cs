using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSControllerScript : MonoBehaviour
{
    public bool jump;
    public bool isGrounded;
    public float x, z;
    public float speed;
    public float mouseSensitivity;
    public float gravity = 9.81f;
    public float jumpSpeed = 2;
    public Camera camera1;
    public CharacterController character;

    public Vector3 velocity;
    private float rightLeftRotation;
    private float upDownRotation;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked; //Bloque el cursor al centro
        Cursor.visible = false; //Hace invisible el cursor 
    }

    void Update()
    {
        /////////////////////////////Rotation Inputs///////////////////////////////
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime * 100;
        rightLeftRotation += mouseX;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime * 100;
        upDownRotation -= mouseY;
        upDownRotation = Mathf.Clamp(upDownRotation,-70,70);
        /////////////////////////////Movement Inputs///////////////////////////////
        x = Input.GetAxis("Horizontal");
        z = Input.GetAxis("Vertical");
        jump = Input.GetButton("Jump");
        isGrounded = character.isGrounded;
    }

    private void FixedUpdate()
    {
        RotateLeftRight();
        RotateUpDown();
        Move();
    }

    private void RotateLeftRight()
    {
        transform.rotation = Quaternion.Euler(0, rightLeftRotation,0);
    }

    private void RotateUpDown()
    {
        camera1.transform.localRotation = Quaternion.Euler(upDownRotation, 0,0);
    }

    private void Move()
    {
        Vector3 moveVector = transform.right * x + transform.forward * z;
        character.Move(moveVector.normalized * speed * Time.deltaTime * 10);

        if(jump && isGrounded) //Presionó el botón de brincar
        {
            velocity.y = jumpSpeed / 20;
        }

        if (!isGrounded)
        {
            velocity.y -= gravity * Time.deltaTime * Time.deltaTime;
        }       
        
        character.Move(velocity);
    }
}
