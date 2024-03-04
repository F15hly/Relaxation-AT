using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inputs : MonoBehaviour
{
    InputSystems inputSystems;

    public Vector2 movementInput;
    public Vector2 rotationInput;
    public float xInput, yInput;
    public float horizontalInp, verticalInp;

    public bool Sprinting;
    public bool Jumping;
    public bool Firing;
    public bool Interacting;
    public bool Reloading;

    private void OnEnable()
    {
        if (inputSystems == null)
        {
            inputSystems = new InputSystems();
            inputSystems.PlayerInputs.Movement.performed += i => movementInput = i.ReadValue<Vector2>();
            inputSystems.PlayerInputs.Rotation.performed += i => rotationInput = i.ReadValue<Vector2>();
            inputSystems.PlayerInputs.Sprint.performed += i => Sprinting = i.ReadValueAsButton();
            inputSystems.PlayerInputs.Jump.performed += i => Jumping = i.ReadValueAsButton();
            inputSystems.PlayerInputs.Fire.performed += i => Firing = i.ReadValueAsButton();
            inputSystems.PlayerInputs.Interact.performed += i => Interacting = i.ReadValueAsButton();
            inputSystems.PlayerInputs.Reload.performed += i => Reloading = i.ReadValueAsButton();
        }
        inputSystems.Enable();
    }

    private void OnDisable()
    {
        inputSystems.Disable();
    }

    private void Update()
    {
        xInput = movementInput.x;
        yInput = movementInput.y;

        horizontalInp = rotationInput.x;
        verticalInp = rotationInput.y;
    }
}
