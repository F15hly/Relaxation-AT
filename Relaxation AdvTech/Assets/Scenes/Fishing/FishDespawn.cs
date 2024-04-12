using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishDespawn : MonoBehaviour
{
    private void OnEnable()
    {
        StartCoroutine(Despawn());
    }

    IEnumerator Despawn()
    {
        yield return new WaitForSeconds(2);
        Destroy(gameObject);
    }
}
