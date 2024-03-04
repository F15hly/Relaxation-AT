using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Binoculars : MonoBehaviour
{
    Inputs inputs;
    public CameraRotation camRot;

    public GameObject player;
    public GameObject playerCam;

    public GameObject BinocularsPrompt;

    public bool isLooking;

    private void Awake()
    {
        inputs = GameObject.FindGameObjectWithTag("Player").GetComponent<Inputs>();
        player = GameObject.FindGameObjectWithTag("Player");
        playerCam = GameObject.FindGameObjectWithTag("MainCamera");
        camRot = player.GetComponent<CameraRotation>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            BinocularsPrompt.SetActive(true);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            BinocularsPrompt.SetActive(false);
            playerCam.GetComponent<Camera>().fieldOfView = 60;
            playerCam.GetComponent<Camera>().nearClipPlane = 0.3f;
            isLooking = false;
            //player.GetComponent<Movement>().enabled = true;
            camRot.ZoomedIn = false;
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            if (inputs.Interacting)
            {
                isLooking = true;
                BinocularsPrompt.SetActive(false);
                playerCam.GetComponent<Camera>().fieldOfView = 20;
                playerCam.GetComponent<Camera>().nearClipPlane = 2f;
                //player.GetComponent<Movement>().enabled = false;
                ///Add a fade
            }
        }
    }

    private void Update()
    {
        if(isLooking)
        {
            camRot.ZoomedIn = true;
        }
    }
}
