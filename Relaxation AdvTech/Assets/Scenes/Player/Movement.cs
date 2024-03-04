using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    Inputs inputs;
    CharacterController controller;

    public float yInp, xInp;
    public Vector3 moveDirection;

    public float walkSpeed = 1f;
    public float sprintSpeed = 2f;
    private float speed;

    //public AudioSource runAudio;

    private void Awake()
    {
        speed = walkSpeed;
        inputs = gameObject.GetComponent<Inputs>();
        controller = gameObject.GetComponent<CharacterController>();
    }

    private void Update()
    {
        //Audio
        bool isMoving;
        if(xInp != 0 || yInp != 0)
        {
            isMoving = true;
        }
        else
        {
            isMoving = false;
        }

        //WASD
        yInp = inputs.yInput;
        xInp = inputs.xInput;

        moveDirection = transform.forward * yInp;
        moveDirection = moveDirection + transform.right * xInp;

        moveDirection.Normalize();

        moveDirection = moveDirection * speed;
        moveDirection.y = -2f;

        Vector3 move = moveDirection;
        controller.Move(move * Time.deltaTime * speed);

        

        //Sprinting
        if (inputs.Sprinting)
        {
            speed = sprintSpeed;
        }
        else
        {
            speed = walkSpeed;
        }
    }
}
