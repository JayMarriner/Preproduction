using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolTest : MonoBehaviour
{
    [SerializeField] private GameObject pointA;
    [SerializeField] private GameObject pointB;

    private float patrolTime = 3f;

    // Start is called before the first frame update
    IEnumerator Start()
    {
        
        while (true)
        {
            yield return StartCoroutine(PatrolEnemy(transform, pointA.transform.position, pointB.transform.position, patrolTime));
            yield return StartCoroutine(PatrolEnemy(transform, pointB.transform.position, pointA.transform.position, patrolTime));
        }
    }

    // Update is called once per frame
    IEnumerator PatrolEnemy(Transform thisTransform, Vector3 startPos, Vector3 endPos, float time)
    {
        var i = 0.0f;
        var rate = 1.0f/time;

        while (i < 1.0f)
        {
            i += Time.deltaTime * rate;
            thisTransform.position = Vector3.Lerp(startPos, endPos, i);
            yield return null;
        }
    }
}
