using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuCamera : MonoBehaviour
{
    public Transform PlayerCam;
    public GameObject Player;

    private void Update()
    {
        transform.LookAt(PlayerCam);
        if(Vector3.Distance(transform.position, PlayerCam.position) < .5f)
        {
            Destroy(gameObject);
        }
    }
    private void OnDestroy()
    {
        Player.GetComponent<Movement>().enabled = true;
        Player.GetComponent<CameraRotation>().enabled = true;
        Player.GetComponent<Inputs>().enabled = true;
        Cursor.visible = false;
    }
}
