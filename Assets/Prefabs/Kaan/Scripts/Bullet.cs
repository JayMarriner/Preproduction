using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Enemy enemy;
    private float destroyDelay = 2f;
    
    // Update is called once per frame
    void Update()
    {
        StartCoroutine(DestroyAfterSeconds());
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            enemy = other.gameObject.GetComponent<Enemy>();
            Debug.Log("Hit!");
            enemy.DealDamage();
            Destroy(gameObject);
        }
    }

    IEnumerator DestroyAfterSeconds()
    {
        yield return new WaitForSeconds(destroyDelay);
        Destroy(gameObject);
    }
}
