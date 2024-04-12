using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeaMovement : MonoBehaviour
{
    Vector3 target;
    private void Awake()
    {
        StartCoroutine(Move());
    }

    IEnumerator Move()
    {
        target = new Vector3(Random.Range(-2, 2), Random.Range(-1,1), Random.Range(-2, 2));
        yield return new WaitForSeconds(1.3f);
        StartCoroutine(Move());
    }
    void LateUpdate()
    {
        transform.position = Vector3.MoveTowards(transform.position, target, 0.2f * Time.deltaTime);
    }
}
