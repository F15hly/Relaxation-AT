using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarreledFish : MonoBehaviour
{
    public GameObject[] CollectedFish;
    public GameObject Hand;
    public GameObject Prompt;

    public bool CanDrop;

    Inputs inputs;

    private void Awake()
    {
        inputs = GameObject.FindGameObjectWithTag("Player").GetComponent<Inputs>();
        CanDrop = false;
}
    void Update()
    {
        CollectedFish = GameObject.FindGameObjectsWithTag("Fish");
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            Prompt.SetActive(true);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            Prompt.SetActive(false);
            if (Hand.transform.childCount == 0)
            {
                Hand.transform.localPosition = new Vector3(0.505996704f, -0.322999954f, 0);
            }
            else
            {
                CanDrop = true;
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            if (inputs.Interacting)
            {
                Prompt.SetActive(false);
                if (CollectedFish != null)
                {
                    //pick up fish
                    CollectedFish[0].GetComponent<Rigidbody>().useGravity = false;
                    CollectedFish[0].GetComponent<Collider>().enabled = false;
                    CollectedFish[0].transform.position = Hand.transform.position;
                    CollectedFish[0].transform.parent = Hand.transform;
                    Hand.transform.localPosition = new Vector3(0.505996704f, 0f, 0.788999975f);
                }
            }
        }
    }
}
