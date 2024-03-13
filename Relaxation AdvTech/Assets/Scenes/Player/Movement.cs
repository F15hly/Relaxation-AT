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

    public AudioSource PlayerAudio;
    public LayerMask woodLayer,snowLayer;
    public AudioClip[] FootStepArray;
    public AudioClip CurrentStepSound;
    private float footstepCycleTimer;

    private void Awake()
    {
        Cursor.visible = false;
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

        if(isMoving)
        {
            //change to every 0.5 seconds
            footstepCycleTimer += Time.deltaTime;
            if(footstepCycleTimer >= 0.5f)
            {
                footstepCycleTimer = 0;
                FootStepFunction();
                PlayerAudio.PlayOneShot(CurrentStepSound);
            }
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

    private void FootStepFunction()
    {
        RaycastHit hit;
        if(Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), out hit, 10f, snowLayer))
        {
            CurrentStepSound = FootStepArray[Random.Range(0, 4)];
            Debug.Log("Snow");
        }
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), out hit, 10f, woodLayer))
        {
            CurrentStepSound = FootStepArray[Random.Range(4, 8)];
            Debug.Log("Wood");
        }
    }
}
