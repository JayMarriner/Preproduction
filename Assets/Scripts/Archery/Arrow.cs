using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    BoxCollider collider;
    LineRenderer line;
    TrailRenderer trail;
    Rigidbody rb;

    private void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        collider = GetComponent<BoxCollider>();
    }

    public void Fired()
    {
        //Box collider is enabled now after collison with bow is not possible.
        collider.enabled = true;

        //Starts the lifespan of the arrow, stops memory leak from old arrows.
        StartCoroutine(Lifespan());
    }

    IEnumerator Lifespan()
    {
        //Destroy the arrow after 10 seconds.
        yield return new WaitForSeconds(10);
        Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        trail = GetComponent<TrailRenderer>();

        //If the arrow collides with a "target" then allow it to stick into the object.
        if (collision.gameObject.tag == "Target")
        {
            //Grab the point of contact with collider object.
            ContactPoint contact = collision.contacts[0];

            //Set the arrows position to the point of contact.
            transform.position = contact.point;

            //Freeze the arrow in place.
            rb.isKinematic = true;

            //Freeze the position and rotation.
            rb.constraints = RigidbodyConstraints.FreezePosition | RigidbodyConstraints.FreezeRotation;
        }

        //If it hits an object that opens the door.
        else if(collision.gameObject.tag == "openDoor")
        {
            //Grab the first point of contact.
            ContactPoint contact = collision.contacts[0];

            //Set the arrow to the point of contact.
            transform.position = contact.point;

            //Freeze the arrow in place.
            rb.isKinematic = true;

            //Freeze the position and rotation.
            rb.constraints = RigidbodyConstraints.FreezePosition | RigidbodyConstraints.FreezeRotation;

            //Run the code attached to the button that opens the door.
            collision.gameObject.GetComponent<DoorTarget>().TargetHit();
        }

        //Get rid of the trail.
        trail.Clear();
    }
}
