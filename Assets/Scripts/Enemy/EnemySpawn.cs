using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemySpawn : MonoBehaviour
{
    [SerializeField] int spawnAmt;
    [SerializeField] GameObject obj;
    GameObject[] currentEnemies;
    BoxCollider spawnArea;
    int currentAmt;
    bool currentlySpawning;

    // Start is called before the first frame update
    void Start()
    {
        spawnArea = GetComponent<BoxCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        if(currentAmt < spawnAmt && !currentlySpawning)
        {
            currentlySpawning = true;
            StartCoroutine(SpawnNew());
        }
    }

    IEnumerator SpawnNew()
    { 
        float waitTime = Random.Range(0, 4);
        yield return new WaitForSeconds(waitTime);
        float pointX = Random.Range(spawnArea.bounds.min.x, spawnArea.bounds.max.x);
        float pointZ = Random.Range(spawnArea.bounds.min.z, spawnArea.bounds.max.z);
        GameObject newObj = Instantiate(obj);
        newObj.GetComponent<NavMeshAgent>().Warp(new Vector3(pointX, gameObject.transform.position.y, pointZ));
        currentAmt++;
        currentlySpawning = false;
    }
}
