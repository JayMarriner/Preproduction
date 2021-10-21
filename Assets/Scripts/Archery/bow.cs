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
    bool newArrowNeeded;
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
        linePos = line.GetPosition(1);

        if (Input.GetKey(KeyCode.Mouse1))
        {
            if (currentArrow == null || newArrowNeeded/*&& !relaxingBow || arrowFired && !relaxingBow*/)
            {
                currentArrow = Instantiate(Arrow, ArrowSpawnPoint.transform);
                arrowRb = currentArrow.GetComponent<Rigidbody>();
                arrowRb.AddForce(Vector3.zero);
                currentArrow.GetComponent<BoxCollider>().enabled = false;
                arrowFired = false;
                newArrowNeeded = false;
            }

            transform.rotation = Camera.main.transform.rotation;

            if (Input.GetKey(KeyCode.Mouse0))
            {
                if (!pullingBow)
                {
                    StartCoroutine(PullBow());
                    pullingBow = true;
                }
            }

            if (Input.GetKeyUp(KeyCode.Mouse0) && !relaxingBow)
            {
                TrailRenderer trail = currentArrow.AddComponent<TrailRenderer>();
                trail.endWidth = 0f; trail.startWidth = 0.2f; trail.startColor = Color.blue; trail.endColor = Color.red; trail.time = 0.25f;

                arrowRb.AddForce(transform.forward * 250 * powerAmt);
                arrowRb.useGravity = true;
                currentArrow.GetComponent<Arrow>().Fired();
                currentArrow.transform.parent = null;
                StartCoroutine(RelaxBow());
                arrowFired = true;
            }
        }
        else
        {
            if (pullingBow)
                StartCoroutine(RelaxBow());

            if (!arrowFired)
                Destroy(currentArrow);
        }
    }

    IEnumerator PullBow()
    {
        while (moveAmt < 0.34f)
        {
            if (relaxingBow == true)
                break;
            if (Input.GetKey(KeyCode.Mouse1))
            {
                line.SetPosition(1, new Vector3(linePos.x, linePos.y, linePos.z - 0.01f));
                //currentArrow.transform.position = linePos;
                //currentArrow.transform.localPosition = new Vector3(currentArrow.transform.position.x, currentArrow.transform.position.y, currentArrow.transform.position.z - 0.01f);

                currentArrow.transform.position -= transform.forward * 0.01f;

                moveAmt += 0.01f;
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
        relaxingBow = true;
        while (moveAmt > 0f)
        {
            line.SetPosition(1, new Vector3(linePos.x, linePos.y, linePos.z + 0.01f));
            moveAmt -= 0.02f;
            yield return new WaitForSeconds(0.0005f);
        }



        if (Input.GetKey(KeyCode.Mouse1))
        {
            newArrowNeeded = true;
        }


        relaxingBow = false;
        pullingBow = false;
        line.SetPosition(1, initialPos);
    }
}
