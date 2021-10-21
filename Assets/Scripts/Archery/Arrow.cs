using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    LineRenderer line;
    Rigidbody rb;

    public void Fired()
    {
        StartCoroutine(Lifespan());
    }

    IEnumerator Lifespan()
    {
        yield return new WaitForSeconds(10);
        Destroy(gameObject);
    }
}
