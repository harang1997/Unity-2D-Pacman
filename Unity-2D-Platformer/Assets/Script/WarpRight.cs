using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarpRight : MonoBehaviour
{
    public Transform leftWarp;
    public GameObject target;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            target = other.gameObject;
            StartCoroutine(Warp());
        }
    }
    
    IEnumerator Warp()
    {
        yield return null;
        target.transform.position = leftWarp.position + new Vector3(3f, 0, 0);
    }
}
