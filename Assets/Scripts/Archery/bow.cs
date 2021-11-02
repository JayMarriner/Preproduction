using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Security.Cryptography;
using UnityEngine;

public class bow : MonoBehaviour
{
    [SerializeField] LineRenderer line;
    [SerializeField] GameObject Arrow;
    [SerializeField] GameObject ArrowSpawnPoint;
    GameObject currentArrow;
    Vector3 linePos;
    Vector3 initialPos;
    float moveAmt;
    float powerAmt;
    bool pullingBow;
    bool arrowFired;
    bool relaxingBow;
    bool dontPull;
    Rigidbody arrowRb;

    // Start is called before the first frame update
    void Start()
    {
        line = GetComponentInChildren<LineRenderer>();
        linePos = initialPos = line.GetPosition(1);
    }

    // Update is called once per frame
    void Update()
    {
        //Store position of the mid-point of bowstring. (line renderer)
        linePos = line.GetPosition(1);

        //Stops multiple arrows from occuring from bugs.
        if (ArrowSpawnPoint.transform.childCount > 1)
        {
            //Loops through and destroys each arrow.
            while (ArrowSpawnPoint.transform.childCount > 0)
            {
                DestroyImmediate(ArrowSpawnPoint.transform.GetChild(0).gameObject);
            }
        }

        if (Input.GetKey(KeyCode.Mouse1))
        {
            //If there's no current arrow then create new arrow.
            if (currentArrow == null)
            {
                currentArrow = Instantiate(Arrow, ArrowSpawnPoint.transform);
                
                //Grab the rigidbody and stop it moving.
                arrowRb = currentArrow.GetComponent<Rigidbody>();
                arrowRb.AddForce(Vector3.zero);

                //Disable the box collider to stop collision with the bow, set bools to false.
                currentArrow.GetComponent<BoxCollider>().enabled = false;
                arrowFired = false;
                dontPull = false;
            }

            //Set the bows rotation equal to where the camera is facing.
            transform.rotation = Camera.main.transform.rotation;

            if (Input.GetKey(KeyCode.Mouse0))
            {
                //If the bow isn't currently being pulled then start pulling it.
                if (!pullingBow)
                {
                    //Dont pull if bowstring is still relaxing.
                    if (relaxingBow)
                        return;
                    //Dont pull if the arrow hasn't respawned.
                    else if (dontPull)
                        return;
                    else
                    {
                        StartCoroutine(PullBow());
                        pullingBow = true;
                    }
                }
            }

            //If the fire button has been released and the bow hasn't started being released yet.
            if (Input.GetKeyUp(KeyCode.Mouse0) && !relaxingBow)
            {
                //Add a trail to the arrow and set the parameters.
                TrailRenderer trail = currentArrow.AddComponent<TrailRenderer>();
                trail.endWidth = 0f; trail.startWidth = 0.2f; trail.startColor = Color.blue; trail.endColor = Color.red; trail.time = 0.25f;
                
                //Set the rigidbody parameters (force to go forward and gravity for drop off). Set the gravity to false if drop off not wanted.
                arrowRb.AddForce(transform.forward * 250 * powerAmt);
                arrowRb.useGravity = true;

                //Run the fired code on the arrow.
                currentArrow.GetComponent<Arrow>().Fired();

                //Detach the object from the player so it isn't attached and follows the player.
                currentArrow.transform.parent = null;

                //Start relaxing the bow.
                StartCoroutine(RelaxBow());
                arrowFired = true;
            }
        }
        else
        {
            //If aim has been released and bow is pulled; start relaxing the bow.
            if (pullingBow)
                StartCoroutine(RelaxBow());

            //If the arrow hasn't been fired then destroy the current arrow.
            if (!arrowFired)
                Destroy(currentArrow);
        }
    }

    IEnumerator PullBow()
    {
        //0.34 picked as looks the most realistic length for bowstring.
        while (moveAmt < 0.34f)
        {
            //If the bowstring is currently being relaxed then stop (failsafe, this code shouldn't be able to run if bow is being relaxed).
            if (relaxingBow == true)
                break;

            //If the player is still holding the fire button then continue to pull the bowstring back until max amount.
            if (Input.GetKey(KeyCode.Mouse1))
            {
                //Move back the midpoint of the bowstring line renderer, move arrow back equal amount.
                line.SetPosition(1, new Vector3(linePos.x, linePos.y, linePos.z - 0.01f));
                currentArrow.transform.position -= transform.forward * 0.01f;

                //Update moveamt to break the while loop.
                moveAmt += 0.01f;

                //Increase the powerAmt multiplier to increase power as the bow is pulled further back.
                powerAmt = moveAmt * 10;

                yield return new WaitForSeconds(0.01f);
            }
            else
            {
                break;
            }
        }
    }

    IEnumerator RelaxBow()
    {
        //Bow is currently being relaxed, so set to true. bow can't be pulled until cooldown ends.
        relaxingBow = true;
        dontPull = true;

        //Allow bow to return back to normal position over small amount of time .
        while (moveAmt > 0f)
        {
            line.SetPosition(1, new Vector3(linePos.x, linePos.y, linePos.z + 0.01f));
            moveAmt -= 0.02f;
            yield return new WaitForSeconds(0.0005f);
        }

        //Pulling bow set to false, bow is no longer being pulled at this point.
        pullingBow = false;

        //Set bow back to the initial position, without this the bow slowly goes further and further in with each use.
        line.SetPosition(1, initialPos);

        //Cooldown for the reload on arrow.
        yield return new WaitForSeconds(1f);

        //Currentarrow set to null to allow for new arrow generation, relaxingbow set to false to allow player to shoot new arrow.
        currentArrow = null;
        relaxingBow = false;
    }
}
