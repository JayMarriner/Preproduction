using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    [SerializeField] private GameObject bullet;
    [SerializeField] private float speed = 100f;

    // Update is called once per frame
    void Update()
    {       
        if (Input.GetMouseButtonDown(0))
        {
            GameObject instBullet = Instantiate(bullet, transform.position, transform.rotation) as GameObject;
            Rigidbody instBulletRb = instBullet.GetComponent<Rigidbody>();
            instBulletRb.AddForce(transform.forward * speed);

            Debug.Log("Shoot");
        }        
    }    
}
