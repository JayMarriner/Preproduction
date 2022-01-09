using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyHandler : MonoBehaviour
{
    NavMeshAgent agent;
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        if(agent.isOnNavMesh)
            RandomPath();
    }

    // Update is called once per frame
    void Update()
    {
        if (agent.remainingDistance <= 0.1f)
            RandomPath();
    }

    void RandomPath()
    {
        print("Run");
        agent.SetDestination(new Vector3(transform.position.x - 10, gameObject.transform.position.y, transform.position.z - 10));
    }
}
