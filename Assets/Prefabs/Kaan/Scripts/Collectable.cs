using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    [SerializeField] private GameObject pickUp;
    public float xAngle, yAngle, zAngle;
        
    void Update()
    {
        pickUp.transform.Rotate(xAngle, yAngle, zAngle, Space.World); //This updates the rotation of the collectable object every frame. 
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) //Check collision with the player.
        {
            other.GetComponent<PlayerMovement>().Jetpack.HasJetPack = true;
            other.GetComponent<PlayerMovement>().Jetpack.jetpackEquipped = true;
            Debug.Log("Trigger!");
            Destroy(gameObject);            
        }
    }
}
