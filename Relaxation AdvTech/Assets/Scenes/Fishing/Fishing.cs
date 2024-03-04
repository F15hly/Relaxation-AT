using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fishing : MonoBehaviour
{
    Inputs inputs;

    public GameObject player;
    public GameObject playerCam;
    public GameObject leftHand;
    public GameObject SittingSpot;

    public GameObject FishingPrompt;

    public bool isFishing;
    public int SittingState;

    public float Timer;
    public float FishTicTimer;
    public int fishTicInt, fishFished;

    public GameObject rod;
    public GameObject[] Fish;
    public Transform FishSpawn;

    private void Awake()
    {
        inputs = GameObject.FindGameObjectWithTag("Player").GetComponent<Inputs>();
        player = GameObject.FindGameObjectWithTag("Player");
        playerCam = GameObject.FindGameObjectWithTag("MainCamera");
        SittingState = 0;
        Timer = 0;
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            FishingPrompt.SetActive(true);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            FishingPrompt.SetActive(false);
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if(other.tag == "Player")
        {
            if (inputs.Interacting)
            {
                if(SittingState < 8)
                {
                    isFishing = true;
                    FishingPrompt.SetActive(false);
                }
            }
        }
    }

    private void Update()
    {
        if(isFishing)
        {
            //disable player scripts
            player.GetComponent<Movement>().enabled = false;
            player.GetComponent<CameraRotation>().enabled = false;
            ///walking to fishing spot
            Sitting();
        }
        else
        {
            player.GetComponent<Movement>().enabled = true;
            player.GetComponent<CameraRotation>().enabled = true;
        }
    }

    public void MiniGame()
    {
        FishTicTimer += Time.deltaTime * 60;
        if (FishTicTimer >= 5)
        {
            FishTicTimer = 0;
            fishTicInt = Mathf.RoundToInt(Random.Range(1, 24));
        }
        if(fishTicInt == 2)
        {
            fishFished = Mathf.RoundToInt(Random.Range(0, Fish.Length));
            SittingState = 101;//high to avoid interacting with other functions
        }
    }

    public void Sitting()
    {
        var step = 1 * Time.deltaTime;
        if (SittingState == 0)
        {
            player.transform.position = Vector3.MoveTowards(player.transform.position, SittingSpot.transform.position, step);
            player.transform.rotation = Quaternion.Slerp(player.transform.rotation, Quaternion.Euler(0, 0, 0), step);
            playerCam.transform.rotation = Quaternion.Slerp(playerCam.transform.rotation, Quaternion.Euler(0, 0, 0), step);
            if (Vector3.Distance(player.transform.position, SittingSpot.transform.position) < 0.01f)
            {
                SittingState = 1;
            }
        }

        if (SittingState == 1)
        {
            playerCam.transform.localPosition = Vector3.MoveTowards(playerCam.transform.localPosition, new Vector3(0, 0, 0), step);
            if (Vector3.Distance(playerCam.transform.localPosition, new Vector3(0, 0, 0)) < 0.01f)
            {
                Timer += Time.deltaTime;
                if (Timer >= 1)
                {
                    SittingState = 2;
                    Timer = 0;
                }
            }
        }

        if (SittingState == 2)
        {
            playerCam.transform.rotation = Quaternion.Lerp(playerCam.transform.rotation, Quaternion.Euler(9.329f, -76.793f, 0), step);
            Timer += Time.deltaTime;
            leftHand.transform.localPosition = Vector3.MoveTowards(leftHand.transform.localPosition, new Vector3(-2.3f, -1.092f, 0f), step);
            if (Timer >= 3)
            {
                SittingState = 3;
                Timer = 0;
            }
        }

        if (SittingState == 3)
        {
            playerCam.transform.rotation = Quaternion.Slerp(playerCam.transform.rotation, Quaternion.Euler(0, 0, 0), step);
            Timer += Time.deltaTime;
            rod.transform.parent = leftHand.transform;
            leftHand.transform.localPosition = Vector3.MoveTowards(leftHand.transform.localPosition, new Vector3(-0.55f, -0.5f, 0.8f), step);
            leftHand.transform.localRotation = Quaternion.Lerp(leftHand.transform.localRotation, Quaternion.Euler(0,0,135f), step);
            if (Timer >= 3)
            {
                SittingState = 4;
                Timer = 0;

            }
        }
        if(SittingState == 4)
        {
            MiniGame();
            if (inputs.Interacting)
            {
                SittingState = 5;
            }
        }
        if(SittingState == 5)
        {
            playerCam.transform.rotation = Quaternion.Lerp(playerCam.transform.rotation, Quaternion.Euler(9.329f, -76.793f, 0), step);
            Timer += Time.deltaTime;
            leftHand.transform.localPosition = Vector3.MoveTowards(leftHand.transform.localPosition, new Vector3(-2.3f, -1.092f, 0f), step);
            if (Timer >= 3)
            {
                SittingState = 6;
                Timer = 0;
            }
        }
        if(SittingState == 6)
        {
            rod.transform.parent = null;
            playerCam.transform.rotation = Quaternion.Slerp(playerCam.transform.rotation, Quaternion.Euler(0, 0, 0), step);
            Timer += Time.deltaTime;
            leftHand.transform.localPosition = Vector3.MoveTowards(leftHand.transform.localPosition, new Vector3(-0.5f, -0.323f, 0), step);
            leftHand.transform.localRotation = Quaternion.Lerp(leftHand.transform.localRotation, Quaternion.Euler(0, 0, 0), step);
            if (Timer >= 3)
            {
                SittingState = 7;
                Timer = 0;
            }
        }
        if(SittingState == 7)
        {
            playerCam.transform.localPosition = Vector3.MoveTowards(playerCam.transform.localPosition, new Vector3(0, 0.54f, 0), step);
            if (Vector3.Distance(playerCam.transform.localPosition, new Vector3(0, 0.54f, 0)) < 0.001f)
            {
                SittingState = 8;
            }
        }
        if(SittingState == 8)
        {
            isFishing = false;
            FishingPrompt.SetActive(true);
        }



        //miniGame Function
        if(SittingState == 101)
        {
            SittingState = 4; //make the animation + spawn the fish on ground
            ///put this in a future state
            Instantiate(Fish[fishFished], FishSpawn.position,transform.rotation);
        }
    }
}
