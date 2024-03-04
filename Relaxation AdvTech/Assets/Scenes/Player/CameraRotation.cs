using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotation : MonoBehaviour
{
    Inputs inputs;

    public Transform Cam;

    public float yRot, xRot;
    public float sense = 1f;

    public bool ZoomedIn;

    private void Awake()
    {
        inputs = gameObject.GetComponent<Inputs>();
        Cam = transform.GetChild(0); // ensure camera is first child
    }

    private void Update()
    {
        yRot += inputs.horizontalInp * Time.deltaTime * 60 * sense;
        xRot -= inputs.verticalInp * Time.deltaTime * 60 * sense;
        
        transform.localRotation = Quaternion.Euler(0, yRot, 0);
        Cam.localRotation = Quaternion.Euler(xRot, 0, 0);

        if(!ZoomedIn)
        {
            if (xRot > 60) xRot = 60; if (xRot < -60) xRot = -60;//clamping
            sense = 1f;
        }
        if(ZoomedIn)
        {
            if (xRot > 10) xRot = 10; if (xRot < -15) xRot = -15;
            if (yRot > 40) yRot = 40; if (yRot < -40) yRot = -40;
            sense = 0.15f;
        }
    }
}
