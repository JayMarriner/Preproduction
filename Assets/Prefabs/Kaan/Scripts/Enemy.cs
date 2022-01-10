using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] public int Health = 100;
    [SerializeField] private GameObject enemyDeathEffect;   

    private void Start()
    {
        if (enemyDeathEffect.GetComponentInChildren<ParticleSystem>().isPlaying)
        {
            enemyDeathEffect.GetComponentInChildren<ParticleSystem>().Stop();
        }
        else
            enemyDeathEffect.GetComponentInChildren<ParticleSystem>().Stop();

        
    }

    private void Update()
    {
        Dead();
    }

    public void DealDamage()
    {
         Health -= 20;
    }

    void Dead()
    {
        if (Health <= 0)
        {         
            Destroy(gameObject);

            Instantiate(enemyDeathEffect, transform.position, Quaternion.identity);
        }
    }
}
