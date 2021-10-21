using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrentItem : MonoBehaviour
{

    [SerializeField] Sprite[] weaponImages;
    public int arrayPos;
    int maxPos;

    // Start is called before the first frame update
    void Start()
    {
        arrayPos = 0;
        maxPos = weaponImages.Length;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxis("Mouse ScrollWheel") > 0f)
        {
            if (arrayPos == 0)
                arrayPos = maxPos-1;
            else
                arrayPos--;
        }

        if (Input.GetAxis("Mouse ScrollWheel") < 0f)
        {
            if (arrayPos == maxPos-1)
                arrayPos = 0;
            else
                arrayPos++;
        }
    }

    public Sprite ReturnCurrentImg()
    {
        return weaponImages[arrayPos];
    }
}
