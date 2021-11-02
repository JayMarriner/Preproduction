using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorHandler : MonoBehaviour
{
    [SerializeField] bool doorClose;

    float yAmt;
    float scaleMultiplier;
    bool doorDown;

    public void OpenDoor()
    {
        //If door isn't already going down then start going down.
        if(!doorDown)
            StartCoroutine(DropOpen());
    }

    IEnumerator DropOpen()
    {
        //Adjust for the height of the object to allow for all door sizes to go down fully.
        scaleMultiplier = gameObject.transform.localScale.y;

        //Set to stop double button hits making object go down twice.
        doorDown = true;

        while(yAmt < 1)
        {
            //Door goes down on Y axis by set amount * the scale.
            gameObject.transform.position -= new Vector3(0, 0.01f * scaleMultiplier, 0);
            yAmt += 0.01f;
            yield return new WaitForSeconds(0.01f / scaleMultiplier);
        }

        yAmt = 0;

        //If this door is supposed to automatically close then it will wait 3 seconds, then begin closing.
        if (doorClose)
        {
            yield return new WaitForSeconds(3f);

            while (yAmt < 1)
            {
                //Goes up on Y axis by set amount * the scale.
                gameObject.transform.position += new Vector3(0, 0.01f * scaleMultiplier, 0);
                yAmt += 0.01f;
                yield return new WaitForSeconds(0.01f / scaleMultiplier);
            }

            yAmt = 0;

            //Door is no longer down.
            doorDown = false;
        }
    }
}
