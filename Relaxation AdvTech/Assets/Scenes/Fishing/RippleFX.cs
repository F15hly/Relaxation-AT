using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RippleFX : MonoBehaviour
{
    private float Timer;
    public float ScaleFactor;
    void Update()
    {
        Timer += Time.deltaTime;
        if(Timer >= 2)
        {
            Destroy(gameObject);
        }
        transform.localScale = new Vector3(transform.localScale.x + Time.deltaTime * ScaleFactor, transform.localScale.y, transform.localScale.z + Time.deltaTime * ScaleFactor);
    }
}
